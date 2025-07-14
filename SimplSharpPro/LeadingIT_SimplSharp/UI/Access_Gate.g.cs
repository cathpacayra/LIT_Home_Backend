 using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    /// <summary>
    /// Options: 'is open', 'is closed', 'is closing...', 'is opening...'
    /// </summary>
    public interface IAccess_Gate
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Open;
        event EventHandler<UIEventArgs> Close;
        event EventHandler<UIEventArgs> Stop;
        event EventHandler<UIEventArgs> Ser_Reserved_In1;

        void Name(Access_GateStringInputSigDelegate callback);
        void Status(Access_GateStringInputSigDelegate callback);
        void Ser_Reserved_Out1(Access_GateStringInputSigDelegate callback);

    }

    public delegate void Access_GateBoolInputSigDelegate(BoolInputSig boolInputSig, IAccess_Gate access_Gate);
    public delegate void Access_GateStringInputSigDelegate(StringInputSig stringInputSig, IAccess_Gate access_Gate);

    internal class Access_Gate : IAccess_Gate, IDisposable
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
                public const uint Open = 1;
                public const uint Close = 2;
                public const uint Stop = 3;

            }
            internal static class Strings
            {
                public const uint Ser_Reserved_In1 = 3;

                public const uint Name = 1;
                public const uint Status = 2;
                public const uint Ser_Reserved_Out1 = 3;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Access_Gate(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Open, onOpen);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Close, onClose);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Stop, onStop);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.Ser_Reserved_In1, onSer_Reserved_In1);

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

        public event EventHandler<UIEventArgs> Open;
        private void onOpen(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Open;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Close;
        private void onClose(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Close;
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


        public event EventHandler<UIEventArgs> Ser_Reserved_In1;
        private void onSer_Reserved_In1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Ser_Reserved_In1;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Name(Access_GateStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Name], this);
            }
        }

        public void Status(Access_GateStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Status], this);
            }
        }

        public void Ser_Reserved_Out1(Access_GateStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out1], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Access_Gate", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            Open = null;
            Close = null;
            Stop = null;
            Ser_Reserved_In1 = null;
        }

        #endregion

    }
}
