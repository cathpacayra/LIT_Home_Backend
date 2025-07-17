using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Crestron.SimplSharp;

namespace LeadingIT_SimplSharp
{
    public class WebSocketServer
    {
        private TcpListener listener;
        private Thread listenerThread;
        private bool running = false;
        private int port;

        public event Action<string> OnMessageReceived;

        public WebSocketServer(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            running = true;
            listener = new TcpListener(System.Net.IPAddress.Any, port);
            listener.Start();
            listenerThread = new Thread(ListenLoop) { Name = "WebSocketListener" };
            listenerThread.Start();
            CrestronConsole.PrintLine("[WebSocketServer] Started on port " + port);
        }

        public void Stop()
        {
            running = false;
            try
            {
                listener.Stop();
            }
            catch { }
            listenerThread = null;
        }

        private void ListenLoop(object userObj)
        {
            while (running)
            {
                try
                {
                    if (!listener.Pending())
                    {
                        Thread.Sleep(50);
                        continue;
                    }
                    TcpClient client = listener.AcceptTcpClient();
                    Thread clientThread = new Thread(ClientLoop) { Name = "WebSocketClient" };
                    clientThread.Start();
                }
                catch (Exception ex)
                {
                    CrestronConsole.PrintLine("[WebSocketServer] Listener error: " + ex.Message);
                }
            }
        }

        private void ClientLoop(object clientObj)
        {
            TcpClient client = (TcpClient)clientObj;
            NetworkStream stream = client.GetStream();

            try
            {
                byte[] buffer = new byte[4096];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string handshake = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                if (handshake.IndexOf("Sec-WebSocket-Key:", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    string key = GetWebSocketKey(handshake);
                    string accept = ComputeWebSocketAccept(key);

                    string response = "HTTP/1.1 101 Switching Protocols\r\n"
                        + "Upgrade: websocket\r\n"
                        + "Connection: Upgrade\r\n"
                        + "Sec-WebSocket-Accept: " + accept + "\r\n\r\n";
                    byte[] respBytes = Encoding.UTF8.GetBytes(response);
                    stream.Write(respBytes, 0, respBytes.Length);
                    CrestronConsole.PrintLine("[WebSocketServer] Handshake complete.");

                    while (running && client.Connected)
                    {
                        if (!stream.DataAvailable)
                        {
                            Thread.Sleep(10);
                            continue;
                        }
                        int len = stream.Read(buffer, 0, buffer.Length);
                        if (len == 0) break;

                        string msg = DecodeWebSocketFrame(buffer, len);
                        CrestronConsole.PrintLine("[WebSocketServer] Received: " + msg);

                        // Raise event
                        OnMessageReceived?.Invoke(msg);

                        // Echo the message back
                        byte[] frame = EncodeWebSocketFrame(msg);
                        stream.Write(frame, 0, frame.Length);
                    }
                }
                else
                {
                    CrestronConsole.PrintLine("[WebSocketServer] Invalid WebSocket handshake.");
                }
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("[WebSocketServer] Client error: " + ex.Message);
            }
            finally
            {
                try { stream.Close(); } catch { }
                try { client.Close(); } catch { }
                CrestronConsole.PrintLine("[WebSocketServer] Client disconnected.");
            }
        }

        private string GetWebSocketKey(string handshake)
        {
            foreach (string line in handshake.Split('\n'))
                if (line.Trim().StartsWith("Sec-WebSocket-Key:"))
                    return line.Split(':')[1].Trim();
            return "";
        }

        private string ComputeWebSocketAccept(string key)
        {
            string magic = key + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
            System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(magic));
            return Convert.ToBase64String(hash);
        }

        private string DecodeWebSocketFrame(byte[] buffer, int length)
        {
            if (length < 6) return "";
            byte secondByte = buffer[1];
            int payloadLen = secondByte & 0x7F;
            int maskIndex = 2;
            if (payloadLen == 126) maskIndex = 4;
            else if (payloadLen == 127) maskIndex = 10;
            byte[] masks = new byte[4];
            Array.Copy(buffer, maskIndex, masks, 0, 4);
            int dataIndex = maskIndex + 4;
            byte[] data = new byte[payloadLen];
            for (int i = 0; i < payloadLen; i++)
                data[i] = (byte)(buffer[dataIndex + i] ^ masks[i % 4]);
            return Encoding.UTF8.GetString(data, 0, data.Length);
        }

        private byte[] EncodeWebSocketFrame(string message)
        {
            byte[] msgBytes = Encoding.UTF8.GetBytes(message);
            byte[] frame = new byte[2 + msgBytes.Length];
            frame[0] = 0x81; // FIN + text frame
            frame[1] = (byte)msgBytes.Length;
            Array.Copy(msgBytes, 0, frame, 2, msgBytes.Length);
            return frame;
        }
    }
}
