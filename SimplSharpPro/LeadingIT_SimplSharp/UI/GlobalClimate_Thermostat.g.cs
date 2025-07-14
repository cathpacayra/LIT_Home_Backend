using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IGlobalClimate_Thermostat
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Power_Toggle;
        event EventHandler<UIEventArgs> IncrementSetpoint;
        event EventHandler<UIEventArgs> DecrementSetpoint;
        event EventHandler<UIEventArgs> Enable_Toggle;
        event EventHandler<UIEventArgs> Dig_Reserved_In1;
        event EventHandler<UIEventArgs> Dig_Reserved_In2;
        event EventHandler<UIEventArgs> Dig_Reserved_In3;
        event EventHandler<UIEventArgs> Setpoint_Set;

        void PowerToggle_FB(GlobalClimate_ThermostatBoolInputSigDelegate callback);
        void EnableToggle_FB(GlobalClimate_ThermostatBoolInputSigDelegate callback);
        void Dig_Reserved_Out1(GlobalClimate_ThermostatBoolInputSigDelegate callback);
        void Dig_Reserved_Out2(GlobalClimate_ThermostatBoolInputSigDelegate callback);
        void Dig_Reserved_Out3(GlobalClimate_ThermostatBoolInputSigDelegate callback);
        void Setpoint_FB(GlobalClimate_ThermostatUShortInputSigDelegate callback);
        void Thermostat_Step(GlobalClimate_ThermostatUShortInputSigDelegate callback);
        void Enable_Status(GlobalClimate_ThermostatStringInputSigDelegate callback);
        void Power_Status(GlobalClimate_ThermostatStringInputSigDelegate callback);
        void Setpoint_Value(GlobalClimate_ThermostatStringInputSigDelegate callback);

    }

    public delegate void GlobalClimate_ThermostatBoolInputSigDelegate(BoolInputSig boolInputSig, IGlobalClimate_Thermostat globalClimate_Thermostat);
    public delegate void GlobalClimate_ThermostatUShortInputSigDelegate(UShortInputSig uShortInputSig, IGlobalClimate_Thermostat globalClimate_Thermostat);
    public delegate void GlobalClimate_ThermostatStringInputSigDelegate(StringInputSig stringInputSig, IGlobalClimate_Thermostat globalClimate_Thermostat);

    internal class GlobalClimate_Thermostat : IGlobalClimate_Thermostat, IDisposable
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
                public const uint Power_Toggle = 1;
                public const uint IncrementSetpoint = 2;
                public const uint DecrementSetpoint = 3;
                public const uint Enable_Toggle = 4;
                public const uint Dig_Reserved_In1 = 5;
                public const uint Dig_Reserved_In2 = 6;
                public const uint Dig_Reserved_In3 = 7;

                public const uint PowerToggle_FB = 1;
                public const uint EnableToggle_FB = 4;
                public const uint Dig_Reserved_Out1 = 5;
                public const uint Dig_Reserved_Out2 = 6;
                public const uint Dig_Reserved_Out3 = 7;
            }
            internal static class Numerics
            {
                public const uint Setpoint_Set = 1;

                public const uint Setpoint_FB = 1;
                public const uint Thermostat_Step = 2;
            }
            internal static class Strings
            {

                public const uint Enable_Status = 1;
                public const uint Power_Status = 2;
                public const uint Setpoint_Value = 3;
            }
        }

        #endregion

        #region Construction and Initialization

        internal GlobalClimate_Thermostat(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Power_Toggle, onPower_Toggle);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.IncrementSetpoint, onIncrementSetpoint);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.DecrementSetpoint, onDecrementSetpoint);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Enable_Toggle, onEnable_Toggle);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In2, onDig_Reserved_In2);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In3, onDig_Reserved_In3);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.Setpoint_Set, onSetpoint_Set);

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

        public event EventHandler<UIEventArgs> Power_Toggle;
        private void onPower_Toggle(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Power_Toggle;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> IncrementSetpoint;
        private void onIncrementSetpoint(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = IncrementSetpoint;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> DecrementSetpoint;
        private void onDecrementSetpoint(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = DecrementSetpoint;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Enable_Toggle;
        private void onEnable_Toggle(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Enable_Toggle;
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

        public event EventHandler<UIEventArgs> Dig_Reserved_In3;
        private void onDig_Reserved_In3(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In3;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void PowerToggle_FB(GlobalClimate_ThermostatBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.PowerToggle_FB], this);
            }
        }

        public void EnableToggle_FB(GlobalClimate_ThermostatBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.EnableToggle_FB], this);
            }
        }

        public void Dig_Reserved_Out1(GlobalClimate_ThermostatBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }

        public void Dig_Reserved_Out2(GlobalClimate_ThermostatBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out2], this);
            }
        }

        public void Dig_Reserved_Out3(GlobalClimate_ThermostatBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out3], this);
            }
        }

        public event EventHandler<UIEventArgs> Setpoint_Set;
        private void onSetpoint_Set(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Setpoint_Set;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Setpoint_FB(GlobalClimate_ThermostatUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Setpoint_FB], this);
            }
        }

        public void Thermostat_Step(GlobalClimate_ThermostatUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Thermostat_Step], this);
            }
        }


        public void Enable_Status(GlobalClimate_ThermostatStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Enable_Status], this);
            }
        }

        public void Power_Status(GlobalClimate_ThermostatStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Power_Status], this);
            }
        }

        public void Setpoint_Value(GlobalClimate_ThermostatStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Setpoint_Value], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "GlobalClimate_Thermostat", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            Power_Toggle = null;
            IncrementSetpoint = null;
            DecrementSetpoint = null;
            Enable_Toggle = null;
            Dig_Reserved_In1 = null;
            Dig_Reserved_In2 = null;
            Dig_Reserved_In3 = null;
            Setpoint_Set = null;
        }

        #endregion

    }
}
