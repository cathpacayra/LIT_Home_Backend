using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface ICurtain
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Open;
        event EventHandler<UIEventArgs> Stop;
        event EventHandler<UIEventArgs> Close;
        event EventHandler<UIEventArgs> Level_Set;

        void Show_Buttons(CurtainBoolInputSigDelegate callback);
        void Show_Slider(CurtainBoolInputSigDelegate callback);
        void Level_FB(CurtainUShortInputSigDelegate callback);
        void Name(CurtainStringInputSigDelegate callback);
        void Ser_Reserved_Out1(CurtainStringInputSigDelegate callback);

    }

    public delegate void CurtainBoolInputSigDelegate(BoolInputSig boolInputSig, ICurtain curtain);
    public delegate void CurtainUShortInputSigDelegate(UShortInputSig uShortInputSig, ICurtain curtain);
    public delegate void CurtainStringInputSigDelegate(StringInputSig stringInputSig, ICurtain curtain);

    internal class Curtain : ICurtain, IDisposable
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
                public const uint Open = 3;
                public const uint Stop = 4;
                public const uint Close = 5;

                public const uint Show_Buttons = 1;
                public const uint Show_Slider = 2;
            }
            internal static class Numerics
            {
                public const uint Level_Set = 1;

                public const uint Level_FB = 1;
            }
            internal static class Strings
            {

                public const uint Name = 1;
                public const uint Ser_Reserved_Out1 = 2;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Curtain(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Open, onOpen);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Stop, onStop);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Close, onClose);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.Level_Set, onLevel_Set);

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

        public event EventHandler<UIEventArgs> Stop;
        private void onStop(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Stop;
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


        public void Show_Buttons(CurtainBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Buttons], this);
            }
        }

        public void Show_Slider(CurtainBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Slider], this);
            }
        }

        public event EventHandler<UIEventArgs> Level_Set;
        private void onLevel_Set(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Level_Set;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Level_FB(CurtainUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Level_FB], this);
            }
        }


        public void Name(CurtainStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Name], this);
            }
        }

        public void Ser_Reserved_Out1(CurtainStringInputSigDelegate callback)
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Curtain", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
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
            Stop = null;
            Close = null;
            Level_Set = null;
        }

        #endregion

    }
}
