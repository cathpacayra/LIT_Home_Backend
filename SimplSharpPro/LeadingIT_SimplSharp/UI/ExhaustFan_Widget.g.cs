using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    /// <summary>
    /// Delayed Off button label
    /// </summary>
    /// <summary>
    /// "FAN is ON" or "FAN is OFF"
    /// </summary>
    public interface IExhaustFan_Widget
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> On;
        event EventHandler<UIEventArgs> Off;
        event EventHandler<UIEventArgs> Delayed_Off;
        event EventHandler<UIEventArgs> Dig_Reserved_In1;
        event EventHandler<UIEventArgs> Dig_Reserved_In2;

        void On_FB(ExhaustFan_WidgetBoolInputSigDelegate callback);
        void Off_FB(ExhaustFan_WidgetBoolInputSigDelegate callback);
        void DelayedOff_FB(ExhaustFan_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out1(ExhaustFan_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out2(ExhaustFan_WidgetBoolInputSigDelegate callback);
        void Delay_Name(ExhaustFan_WidgetStringInputSigDelegate callback);
        void Status(ExhaustFan_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out1(ExhaustFan_WidgetStringInputSigDelegate callback);

    }

    public delegate void ExhaustFan_WidgetBoolInputSigDelegate(BoolInputSig boolInputSig, IExhaustFan_Widget exhaustFan_Widget);
    public delegate void ExhaustFan_WidgetStringInputSigDelegate(StringInputSig stringInputSig, IExhaustFan_Widget exhaustFan_Widget);

    internal class ExhaustFan_Widget : IExhaustFan_Widget, IDisposable
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
                public const uint On = 1;
                public const uint Off = 2;
                public const uint Delayed_Off = 3;
                public const uint Dig_Reserved_In1 = 4;
                public const uint Dig_Reserved_In2 = 5;

                public const uint On_FB = 1;
                public const uint Off_FB = 2;
                public const uint DelayedOff_FB = 3;
                public const uint Dig_Reserved_Out1 = 4;
                public const uint Dig_Reserved_Out2 = 5;
            }
            internal static class Strings
            {

                public const uint Delay_Name = 1;
                public const uint Status = 2;
                public const uint Ser_Reserved_Out1 = 3;
            }
        }

        #endregion

        #region Construction and Initialization

        internal ExhaustFan_Widget(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.On, onOn);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Off, onOff);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Delayed_Off, onDelayed_Off);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In2, onDig_Reserved_In2);

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

        public event EventHandler<UIEventArgs> On;
        private void onOn(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = On;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Off;
        private void onOff(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Off;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Delayed_Off;
        private void onDelayed_Off(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Delayed_Off;
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


        public void On_FB(ExhaustFan_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.On_FB], this);
            }
        }

        public void Off_FB(ExhaustFan_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Off_FB], this);
            }
        }

        public void DelayedOff_FB(ExhaustFan_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.DelayedOff_FB], this);
            }
        }

        public void Dig_Reserved_Out1(ExhaustFan_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }

        public void Dig_Reserved_Out2(ExhaustFan_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out2], this);
            }
        }


        public void Delay_Name(ExhaustFan_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Delay_Name], this);
            }
        }

        public void Status(ExhaustFan_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Status], this);
            }
        }

        public void Ser_Reserved_Out1(ExhaustFan_WidgetStringInputSigDelegate callback)
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "ExhaustFan_Widget", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            On = null;
            Off = null;
            Delayed_Off = null;
            Dig_Reserved_In1 = null;
            Dig_Reserved_In2 = null;
        }

        #endregion

    }
}
