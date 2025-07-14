using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IVideo_Remote
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Button1_Press;
        event EventHandler<UIEventArgs> Button2_Press;
        event EventHandler<UIEventArgs> Button3_Press;
        event EventHandler<UIEventArgs> Button4_Press;
        event EventHandler<UIEventArgs> Button5_Press;
        event EventHandler<UIEventArgs> Button6_Press;
        event EventHandler<UIEventArgs> Button7_Press;
        event EventHandler<UIEventArgs> Button8_Press;
        event EventHandler<UIEventArgs> Button9_Press;
        event EventHandler<UIEventArgs> DPad_Up;
        event EventHandler<UIEventArgs> DPad_Down;
        event EventHandler<UIEventArgs> DPad_Right;
        event EventHandler<UIEventArgs> DPad_Left;
        event EventHandler<UIEventArgs> DPad_Enter;
        event EventHandler<UIEventArgs> Green;
        event EventHandler<UIEventArgs> Red;
        event EventHandler<UIEventArgs> Blue;
        event EventHandler<UIEventArgs> Yellow;
        event EventHandler<UIEventArgs> Play;
        event EventHandler<UIEventArgs> Pause;
        event EventHandler<UIEventArgs> Stop;
        event EventHandler<UIEventArgs> Forward;
        event EventHandler<UIEventArgs> Rewind;
        event EventHandler<UIEventArgs> Next;
        event EventHandler<UIEventArgs> Previous;
        event EventHandler<UIEventArgs> ProjectorPower_Toggle;
        event EventHandler<UIEventArgs> ProjectorLift_Up;
        event EventHandler<UIEventArgs> ProjectorLift_Down;
        event EventHandler<UIEventArgs> ProjectorLift_Stop;
        event EventHandler<UIEventArgs> ScreenLift_Up;
        event EventHandler<UIEventArgs> ScreenLift_Down;
        event EventHandler<UIEventArgs> ScreenLift_Stop;

        void Show_RegularRemote(Video_RemoteBoolInputSigDelegate callback);
        void Show_AppleTV_Remote(Video_RemoteBoolInputSigDelegate callback);
        void Show_Button1(Video_RemoteBoolInputSigDelegate callback);
        void Show_Button2(Video_RemoteBoolInputSigDelegate callback);
        void Show_Button3(Video_RemoteBoolInputSigDelegate callback);
        void Show_Button4(Video_RemoteBoolInputSigDelegate callback);
        void Show_Button5(Video_RemoteBoolInputSigDelegate callback);
        void Show_Button6(Video_RemoteBoolInputSigDelegate callback);
        void Show_Button7(Video_RemoteBoolInputSigDelegate callback);
        void Show_Button8(Video_RemoteBoolInputSigDelegate callback);
        void Show_Button9(Video_RemoteBoolInputSigDelegate callback);
        void Show_Color_Buttons(Video_RemoteBoolInputSigDelegate callback);
        void Show_Lift_Controls(Video_RemoteBoolInputSigDelegate callback);
        void Show_ProjectorData_List(Video_RemoteBoolInputSigDelegate callback);
        void ProjectorPower_FB(Video_RemoteBoolInputSigDelegate callback);
        void Show_ProjectorLift_Controls(Video_RemoteBoolInputSigDelegate callback);
        void Show_ScreenLift_Controls(Video_RemoteBoolInputSigDelegate callback);
        void Show_ProjectorLift_Stop(Video_RemoteBoolInputSigDelegate callback);
        void Show_ScreenLift_Stop(Video_RemoteBoolInputSigDelegate callback);
        void NumberOf_ProjectorData_Outputs(Video_RemoteUShortInputSigDelegate callback);
        void An_Reserved_Out1(Video_RemoteUShortInputSigDelegate callback);
        void Button1_Name(Video_RemoteStringInputSigDelegate callback);
        void Button2_Name(Video_RemoteStringInputSigDelegate callback);
        void Button3_Name(Video_RemoteStringInputSigDelegate callback);
        void Button4_Name(Video_RemoteStringInputSigDelegate callback);
        void Button5_Name(Video_RemoteStringInputSigDelegate callback);
        void Button6_Name(Video_RemoteStringInputSigDelegate callback);
        void Button7_Name(Video_RemoteStringInputSigDelegate callback);
        void Button8_Name(Video_RemoteStringInputSigDelegate callback);
        void Button9_Name(Video_RemoteStringInputSigDelegate callback);
        void ProjectorPower_Status(Video_RemoteStringInputSigDelegate callback);
        void ProjectorData_Output0(Video_RemoteStringInputSigDelegate callback);
        void ProjectorData_Output1(Video_RemoteStringInputSigDelegate callback);
        void ProjectorData_Output2(Video_RemoteStringInputSigDelegate callback);
        void ProjectorData_Output3(Video_RemoteStringInputSigDelegate callback);
        void ProjectorData_Output4(Video_RemoteStringInputSigDelegate callback);
        void Ser_Reserved_Out1(Video_RemoteStringInputSigDelegate callback);
        void Ser_Reserved_Out2(Video_RemoteStringInputSigDelegate callback);

    }

    public delegate void Video_RemoteBoolInputSigDelegate(BoolInputSig boolInputSig, IVideo_Remote video_Remote);
    public delegate void Video_RemoteUShortInputSigDelegate(UShortInputSig uShortInputSig, IVideo_Remote video_Remote);
    public delegate void Video_RemoteStringInputSigDelegate(StringInputSig stringInputSig, IVideo_Remote video_Remote);

    internal class Video_Remote : IVideo_Remote, IDisposable
    {
        #region Standard CH5 Component members

        private ComponentMediator ComponentMediator { get; set; }

        public object UserObject { get; set; }

        public uint ControlJoinId { get; private set; }

        private IList<BasicTriListWithSmartObject> _devices;
        public IList<BasicTriListWithSmartObject> Devices { get { return _devices; } }

        #endregion

        #region Joins

        private static class Joins
        {
            internal static class Booleans
            {
                public const uint Button1_Press = 13;
                public const uint Button2_Press = 14;
                public const uint Button3_Press = 15;
                public const uint Button4_Press = 16;
                public const uint Button5_Press = 17;
                public const uint Button6_Press = 18;
                public const uint Button7_Press = 19;
                public const uint Button8_Press = 20;
                public const uint Button9_Press = 21;
                public const uint DPad_Up = 23;
                public const uint DPad_Down = 24;
                public const uint DPad_Right = 25;
                public const uint DPad_Left = 26;
                public const uint DPad_Enter = 27;
                public const uint Green = 30;
                public const uint Red = 31;
                public const uint Blue = 32;
                public const uint Yellow = 33;
                public const uint Play = 35;
                public const uint Pause = 36;
                public const uint Stop = 37;
                public const uint Forward = 38;
                public const uint Rewind = 39;
                public const uint Next = 40;
                public const uint Previous = 41;
                public const uint ProjectorPower_Toggle = 45;
                public const uint ProjectorLift_Up = 46;
                public const uint ProjectorLift_Down = 47;
                public const uint ProjectorLift_Stop = 48;
                public const uint ScreenLift_Up = 49;
                public const uint ScreenLift_Down = 50;
                public const uint ScreenLift_Stop = 51;

                public const uint Show_RegularRemote = 1;
                public const uint Show_AppleTV_Remote = 2;
                public const uint Show_Button1 = 3;
                public const uint Show_Button2 = 4;
                public const uint Show_Button3 = 5;
                public const uint Show_Button4 = 6;
                public const uint Show_Button5 = 7;
                public const uint Show_Button6 = 8;
                public const uint Show_Button7 = 9;
                public const uint Show_Button8 = 10;
                public const uint Show_Button9 = 11;
                public const uint Show_Color_Buttons = 29;
                public const uint Show_Lift_Controls = 43;
                public const uint Show_ProjectorData_List = 44;
                public const uint ProjectorPower_FB = 45;
                public const uint Show_ProjectorLift_Controls = 52;
                public const uint Show_ScreenLift_Controls = 53;
                public const uint Show_ProjectorLift_Stop = 54;
                public const uint Show_ScreenLift_Stop = 55;
            }
            internal static class Numerics
            {

                public const uint NumberOf_ProjectorData_Outputs = 1;
                public const uint An_Reserved_Out1 = 2;
            }
            internal static class Strings
            {

                public const uint Button1_Name = 1;
                public const uint Button2_Name = 2;
                public const uint Button3_Name = 3;
                public const uint Button4_Name = 4;
                public const uint Button5_Name = 5;
                public const uint Button6_Name = 6;
                public const uint Button7_Name = 7;
                public const uint Button8_Name = 8;
                public const uint Button9_Name = 9;
                public const uint ProjectorPower_Status = 11;
                public const uint ProjectorData_Output0 = 12;
                public const uint ProjectorData_Output1 = 13;
                public const uint ProjectorData_Output2 = 14;
                public const uint ProjectorData_Output3 = 15;
                public const uint ProjectorData_Output4 = 16;
                public const uint Ser_Reserved_Out1 = 18;
                public const uint Ser_Reserved_Out2 = 19;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Video_Remote(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Button1_Press, onButton1_Press);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Button2_Press, onButton2_Press);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Button3_Press, onButton3_Press);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Button4_Press, onButton4_Press);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Button5_Press, onButton5_Press);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Button6_Press, onButton6_Press);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Button7_Press, onButton7_Press);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Button8_Press, onButton8_Press);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Button9_Press, onButton9_Press);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.DPad_Up, onDPad_Up);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.DPad_Down, onDPad_Down);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.DPad_Right, onDPad_Right);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.DPad_Left, onDPad_Left);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.DPad_Enter, onDPad_Enter);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Green, onGreen);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Red, onRed);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Blue, onBlue);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Yellow, onYellow);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Play, onPlay);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Pause, onPause);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Stop, onStop);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Forward, onForward);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Rewind, onRewind);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Next, onNext);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Previous, onPrevious);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ProjectorPower_Toggle, onProjectorPower_Toggle);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ProjectorLift_Up, onProjectorLift_Up);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ProjectorLift_Down, onProjectorLift_Down);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ProjectorLift_Stop, onProjectorLift_Stop);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ScreenLift_Up, onScreenLift_Up);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ScreenLift_Down, onScreenLift_Down);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ScreenLift_Stop, onScreenLift_Stop);

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
        }

        #endregion

        #region CH5 Contract

        public event EventHandler<UIEventArgs> Button1_Press;
        private void onButton1_Press(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Button1_Press;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Button2_Press;
        private void onButton2_Press(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Button2_Press;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Button3_Press;
        private void onButton3_Press(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Button3_Press;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Button4_Press;
        private void onButton4_Press(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Button4_Press;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Button5_Press;
        private void onButton5_Press(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Button5_Press;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Button6_Press;
        private void onButton6_Press(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Button6_Press;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Button7_Press;
        private void onButton7_Press(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Button7_Press;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Button8_Press;
        private void onButton8_Press(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Button8_Press;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Button9_Press;
        private void onButton9_Press(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Button9_Press;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> DPad_Up;
        private void onDPad_Up(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = DPad_Up;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> DPad_Down;
        private void onDPad_Down(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = DPad_Down;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> DPad_Right;
        private void onDPad_Right(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = DPad_Right;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> DPad_Left;
        private void onDPad_Left(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = DPad_Left;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> DPad_Enter;
        private void onDPad_Enter(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = DPad_Enter;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Green;
        private void onGreen(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Green;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Red;
        private void onRed(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Red;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Blue;
        private void onBlue(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Blue;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Yellow;
        private void onYellow(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Yellow;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Play;
        private void onPlay(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Play;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Pause;
        private void onPause(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Pause;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Stop;
        private void onStop(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Stop;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Forward;
        private void onForward(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Forward;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Rewind;
        private void onRewind(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Rewind;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Next;
        private void onNext(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Next;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Previous;
        private void onPrevious(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Previous;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ProjectorPower_Toggle;
        private void onProjectorPower_Toggle(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ProjectorPower_Toggle;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ProjectorLift_Up;
        private void onProjectorLift_Up(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ProjectorLift_Up;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ProjectorLift_Down;
        private void onProjectorLift_Down(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ProjectorLift_Down;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ProjectorLift_Stop;
        private void onProjectorLift_Stop(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ProjectorLift_Stop;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ScreenLift_Up;
        private void onScreenLift_Up(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ScreenLift_Up;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ScreenLift_Down;
        private void onScreenLift_Down(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ScreenLift_Down;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ScreenLift_Stop;
        private void onScreenLift_Stop(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ScreenLift_Stop;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Show_RegularRemote(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_RegularRemote], this);
            }
        }

        public void Show_AppleTV_Remote(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_AppleTV_Remote], this);
            }
        }

        public void Show_Button1(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Button1], this);
            }
        }

        public void Show_Button2(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Button2], this);
            }
        }

        public void Show_Button3(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Button3], this);
            }
        }

        public void Show_Button4(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Button4], this);
            }
        }

        public void Show_Button5(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Button5], this);
            }
        }

        public void Show_Button6(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Button6], this);
            }
        }

        public void Show_Button7(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Button7], this);
            }
        }

        public void Show_Button8(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Button8], this);
            }
        }

        public void Show_Button9(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Button9], this);
            }
        }

        public void Show_Color_Buttons(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Color_Buttons], this);
            }
        }

        public void Show_Lift_Controls(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Lift_Controls], this);
            }
        }

        public void Show_ProjectorData_List(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_ProjectorData_List], this);
            }
        }

        public void ProjectorPower_FB(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.ProjectorPower_FB], this);
            }
        }

        public void Show_ProjectorLift_Controls(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_ProjectorLift_Controls], this);
            }
        }

        public void Show_ScreenLift_Controls(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_ScreenLift_Controls], this);
            }
        }

        public void Show_ProjectorLift_Stop(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_ProjectorLift_Stop], this);
            }
        }

        public void Show_ScreenLift_Stop(Video_RemoteBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_ScreenLift_Stop], this);
            }
        }


        public void NumberOf_ProjectorData_Outputs(Video_RemoteUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_ProjectorData_Outputs], this);
            }
        }

        public void An_Reserved_Out1(Video_RemoteUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.An_Reserved_Out1], this);
            }
        }


        public void Button1_Name(Video_RemoteStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Button1_Name], this);
            }
        }

        public void Button2_Name(Video_RemoteStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Button2_Name], this);
            }
        }

        public void Button3_Name(Video_RemoteStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Button3_Name], this);
            }
        }

        public void Button4_Name(Video_RemoteStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Button4_Name], this);
            }
        }

        public void Button5_Name(Video_RemoteStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Button5_Name], this);
            }
        }

        public void Button6_Name(Video_RemoteStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Button6_Name], this);
            }
        }

        public void Button7_Name(Video_RemoteStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Button7_Name], this);
            }
        }

        public void Button8_Name(Video_RemoteStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Button8_Name], this);
            }
        }

        public void Button9_Name(Video_RemoteStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Button9_Name], this);
            }
        }

        public void ProjectorPower_Status(Video_RemoteStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.ProjectorPower_Status], this);
            }
        }

        public void ProjectorData_Output0(Video_RemoteStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.ProjectorData_Output0], this);
            }
        }

        public void ProjectorData_Output1(Video_RemoteStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.ProjectorData_Output1], this);
            }
        }

        public void ProjectorData_Output2(Video_RemoteStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.ProjectorData_Output2], this);
            }
        }

        public void ProjectorData_Output3(Video_RemoteStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.ProjectorData_Output3], this);
            }
        }

        public void ProjectorData_Output4(Video_RemoteStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.ProjectorData_Output4], this);
            }
        }

        public void Ser_Reserved_Out1(Video_RemoteStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out1], this);
            }
        }

        public void Ser_Reserved_Out2(Video_RemoteStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out2], this);
            }
        }

        #endregion

        #region Overrides

        public override int GetHashCode()
        {
            return (int)ControlJoinId;
        }

        public override string ToString()
        {
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Video_Remote", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            Button1_Press = null;
            Button2_Press = null;
            Button3_Press = null;
            Button4_Press = null;
            Button5_Press = null;
            Button6_Press = null;
            Button7_Press = null;
            Button8_Press = null;
            Button9_Press = null;
            DPad_Up = null;
            DPad_Down = null;
            DPad_Right = null;
            DPad_Left = null;
            DPad_Enter = null;
            Green = null;
            Red = null;
            Blue = null;
            Yellow = null;
            Play = null;
            Pause = null;
            Stop = null;
            Forward = null;
            Rewind = null;
            Next = null;
            Previous = null;
            ProjectorPower_Toggle = null;
            ProjectorLift_Up = null;
            ProjectorLift_Down = null;
            ProjectorLift_Stop = null;
            ScreenLift_Up = null;
            ScreenLift_Down = null;
            ScreenLift_Stop = null;
        }

        #endregion

    }
}
