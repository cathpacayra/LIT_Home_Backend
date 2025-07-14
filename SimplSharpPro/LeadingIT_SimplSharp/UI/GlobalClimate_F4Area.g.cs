using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IGlobalClimate_F4Area
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Select;
        event EventHandler<UIEventArgs> IncrementSetpoint;
        event EventHandler<UIEventArgs> DecrementSetpoint;
        event EventHandler<UIEventArgs> Dig_Reserved_In1;
        event EventHandler<UIEventArgs> Dig_Reserved_In2;
        event EventHandler<UIEventArgs> Power_Toggle;

        void IsSelected(GlobalClimate_F4AreaBoolInputSigDelegate callback);
        void PowerToggle_FB(GlobalClimate_F4AreaBoolInputSigDelegate callback);
        void Dig_Reserved_Out1(GlobalClimate_F4AreaBoolInputSigDelegate callback);
        void Dig_Reserved_Out2(GlobalClimate_F4AreaBoolInputSigDelegate callback);
        void Name(GlobalClimate_F4AreaStringInputSigDelegate callback);
        void AC_Status(GlobalClimate_F4AreaStringInputSigDelegate callback);
        void Setpoint_Value(GlobalClimate_F4AreaStringInputSigDelegate callback);
        void RoomTemp_Value(GlobalClimate_F4AreaStringInputSigDelegate callback);
        void RoomHum_Value(GlobalClimate_F4AreaStringInputSigDelegate callback);
        void Ser_Reserved_Out1(GlobalClimate_F4AreaStringInputSigDelegate callback);

    }

    public delegate void GlobalClimate_F4AreaBoolInputSigDelegate(BoolInputSig boolInputSig, IGlobalClimate_F4Area globalClimate_F4Area);
    public delegate void GlobalClimate_F4AreaStringInputSigDelegate(StringInputSig stringInputSig, IGlobalClimate_F4Area globalClimate_F4Area);

    internal class GlobalClimate_F4Area : IGlobalClimate_F4Area, IDisposable
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
                public const uint Select = 1;
                public const uint IncrementSetpoint = 2;
                public const uint DecrementSetpoint = 3;
                public const uint Dig_Reserved_In1 = 4;
                public const uint Dig_Reserved_In2 = 5;
                public const uint Power_Toggle = 6;

                public const uint IsSelected = 1;
                public const uint PowerToggle_FB = 4;
                public const uint Dig_Reserved_Out1 = 5;
                public const uint Dig_Reserved_Out2 = 6;
            }
            internal static class Strings
            {

                public const uint Name = 1;
                public const uint AC_Status = 2;
                public const uint Setpoint_Value = 3;
                public const uint RoomTemp_Value = 4;
                public const uint RoomHum_Value = 5;
                public const uint Ser_Reserved_Out1 = 6;
            }
        }

        #endregion

        #region Construction and Initialization

        internal GlobalClimate_F4Area(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Select, onSelect);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.IncrementSetpoint, onIncrementSetpoint);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.DecrementSetpoint, onDecrementSetpoint);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In2, onDig_Reserved_In2);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Power_Toggle, onPower_Toggle);

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

        public event EventHandler<UIEventArgs> Select;
        private void onSelect(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Select;
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

        public event EventHandler<UIEventArgs> Power_Toggle;
        private void onPower_Toggle(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Power_Toggle;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void IsSelected(GlobalClimate_F4AreaBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.IsSelected], this);
            }
        }

        public void PowerToggle_FB(GlobalClimate_F4AreaBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.PowerToggle_FB], this);
            }
        }

        public void Dig_Reserved_Out1(GlobalClimate_F4AreaBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }

        public void Dig_Reserved_Out2(GlobalClimate_F4AreaBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out2], this);
            }
        }


        public void Name(GlobalClimate_F4AreaStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Name], this);
            }
        }

        public void AC_Status(GlobalClimate_F4AreaStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.AC_Status], this);
            }
        }

        public void Setpoint_Value(GlobalClimate_F4AreaStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Setpoint_Value], this);
            }
        }

        public void RoomTemp_Value(GlobalClimate_F4AreaStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.RoomTemp_Value], this);
            }
        }

        public void RoomHum_Value(GlobalClimate_F4AreaStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.RoomHum_Value], this);
            }
        }

        public void Ser_Reserved_Out1(GlobalClimate_F4AreaStringInputSigDelegate callback)
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "GlobalClimate_F4Area", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            Select = null;
            IncrementSetpoint = null;
            DecrementSetpoint = null;
            Dig_Reserved_In1 = null;
            Dig_Reserved_In2 = null;
            Power_Toggle = null;
        }

        #endregion

    }
}
