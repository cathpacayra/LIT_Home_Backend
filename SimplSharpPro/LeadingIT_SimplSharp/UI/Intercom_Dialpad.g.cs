using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IIntercom_Dialpad
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Dial_1;
        event EventHandler<UIEventArgs> Dial_2;
        event EventHandler<UIEventArgs> Dial_3;
        event EventHandler<UIEventArgs> Dial_4;
        event EventHandler<UIEventArgs> Dial_5;
        event EventHandler<UIEventArgs> Dial_6;
        event EventHandler<UIEventArgs> Dial_7;
        event EventHandler<UIEventArgs> Dial_8;
        event EventHandler<UIEventArgs> Dial_9;
        event EventHandler<UIEventArgs> Dial_0;
        event EventHandler<UIEventArgs> Call_Select;
        event EventHandler<UIEventArgs> HangUp_Select;
        event EventHandler<UIEventArgs> Dig_Reserved_In1;
        event EventHandler<UIEventArgs> Dig_Reserved_In2;
        event EventHandler<UIEventArgs> Delete_Input;
        event EventHandler<UIEventArgs> Dialed_Number;

        void Dig_Reserved_Out1(Intercom_DialpadBoolInputSigDelegate callback);
        void Dig_Reserved_Out2(Intercom_DialpadBoolInputSigDelegate callback);
        void Dialed_Number_FB(Intercom_DialpadStringInputSigDelegate callback);

    }

    public delegate void Intercom_DialpadBoolInputSigDelegate(BoolInputSig boolInputSig, IIntercom_Dialpad intercom_Dialpad);
    public delegate void Intercom_DialpadStringInputSigDelegate(StringInputSig stringInputSig, IIntercom_Dialpad intercom_Dialpad);

    internal class Intercom_Dialpad : IIntercom_Dialpad, IDisposable
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
                public const uint Dial_1 = 1;
                public const uint Dial_2 = 2;
                public const uint Dial_3 = 3;
                public const uint Dial_4 = 4;
                public const uint Dial_5 = 5;
                public const uint Dial_6 = 6;
                public const uint Dial_7 = 7;
                public const uint Dial_8 = 8;
                public const uint Dial_9 = 9;
                public const uint Dial_0 = 10;
                public const uint Call_Select = 11;
                public const uint HangUp_Select = 12;
                public const uint Dig_Reserved_In1 = 13;
                public const uint Dig_Reserved_In2 = 14;
                public const uint Delete_Input = 15;

                public const uint Dig_Reserved_Out1 = 14;
                public const uint Dig_Reserved_Out2 = 15;
            }
            internal static class Strings
            {
                public const uint Dialed_Number = 1;

                public const uint Dialed_Number_FB = 1;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Intercom_Dialpad(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dial_1, onDial_1);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dial_2, onDial_2);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dial_3, onDial_3);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dial_4, onDial_4);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dial_5, onDial_5);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dial_6, onDial_6);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dial_7, onDial_7);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dial_8, onDial_8);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dial_9, onDial_9);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dial_0, onDial_0);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Call_Select, onCall_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.HangUp_Select, onHangUp_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In2, onDig_Reserved_In2);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Delete_Input, onDelete_Input);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.Dialed_Number, onDialed_Number);

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

        public event EventHandler<UIEventArgs> Dial_1;
        private void onDial_1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dial_1;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dial_2;
        private void onDial_2(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dial_2;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dial_3;
        private void onDial_3(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dial_3;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dial_4;
        private void onDial_4(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dial_4;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dial_5;
        private void onDial_5(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dial_5;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dial_6;
        private void onDial_6(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dial_6;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dial_7;
        private void onDial_7(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dial_7;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dial_8;
        private void onDial_8(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dial_8;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dial_9;
        private void onDial_9(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dial_9;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dial_0;
        private void onDial_0(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dial_0;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Call_Select;
        private void onCall_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Call_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> HangUp_Select;
        private void onHangUp_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = HangUp_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dig_Reserved_In1;
        private void onDig_Reserved_In1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In1;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dig_Reserved_In2;
        private void onDig_Reserved_In2(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In2;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Delete_Input;
        private void onDelete_Input(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Delete_Input;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Dig_Reserved_Out1(Intercom_DialpadBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }

        public void Dig_Reserved_Out2(Intercom_DialpadBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out2], this);
            }
        }

        public event EventHandler<UIEventArgs> Dialed_Number;
        private void onDialed_Number(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dialed_Number;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Dialed_Number_FB(Intercom_DialpadStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Dialed_Number_FB], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Intercom_Dialpad", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            Dial_1 = null;
            Dial_2 = null;
            Dial_3 = null;
            Dial_4 = null;
            Dial_5 = null;
            Dial_6 = null;
            Dial_7 = null;
            Dial_8 = null;
            Dial_9 = null;
            Dial_0 = null;
            Call_Select = null;
            HangUp_Select = null;
            Dig_Reserved_In1 = null;
            Dig_Reserved_In2 = null;
            Delete_Input = null;
            Dialed_Number = null;
        }

        #endregion

    }
}
