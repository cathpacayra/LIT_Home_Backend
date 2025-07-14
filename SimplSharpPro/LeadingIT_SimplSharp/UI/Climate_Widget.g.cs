using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    /// <summary>
    /// High when AC is On
    /// </summary>
    /// <summary>
    /// Receive 'AC is ON' or 'AC is OFF' depending on AC status
    /// </summary>
    /// <summary>
    /// Room Temperature
    /// </summary>
    /// <summary>
    /// Room Humidity
    /// </summary>
    public interface IClimate_Widget
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Power_Toggle;
        event EventHandler<UIEventArgs> IncrementSetpoint;
        event EventHandler<UIEventArgs> DecrementSetpoint;
        event EventHandler<UIEventArgs> FanSpeed_1_Select;
        event EventHandler<UIEventArgs> FanSpeed_2_Select;
        event EventHandler<UIEventArgs> FanSpeed_3_Select;
        event EventHandler<UIEventArgs> FanSpeed_4_Select;
        event EventHandler<UIEventArgs> FanSpeed_5_Select;
        event EventHandler<UIEventArgs> Setpoint_Set;

        void PowerToggle_FB(Climate_WidgetBoolInputSigDelegate callback);
        void Show_FanControlsButton(Climate_WidgetBoolInputSigDelegate callback);
        void FanSpeed_1_IsSelected(Climate_WidgetBoolInputSigDelegate callback);
        void FanSpeed_2_IsSelected(Climate_WidgetBoolInputSigDelegate callback);
        void FanSpeed_3_IsSelected(Climate_WidgetBoolInputSigDelegate callback);
        void FanSpeed_4_IsSelected(Climate_WidgetBoolInputSigDelegate callback);
        void FanSpeed_5_IsSelected(Climate_WidgetBoolInputSigDelegate callback);
        void Show_FanSpeed_1(Climate_WidgetBoolInputSigDelegate callback);
        void Show_FanSpeed_2(Climate_WidgetBoolInputSigDelegate callback);
        void Show_FanSpeed_3(Climate_WidgetBoolInputSigDelegate callback);
        void Show_FanSpeed_4(Climate_WidgetBoolInputSigDelegate callback);
        void Show_FanSpeed_5(Climate_WidgetBoolInputSigDelegate callback);
        void Show_RoomTemp(Climate_WidgetBoolInputSigDelegate callback);
        void Show_RoomHumidity(Climate_WidgetBoolInputSigDelegate callback);
        void Setpoint_FB(Climate_WidgetUShortInputSigDelegate callback);
        void Thermostat_Step(Climate_WidgetUShortInputSigDelegate callback);
        void AC_Status(Climate_WidgetStringInputSigDelegate callback);
        void Setpoint_Value(Climate_WidgetStringInputSigDelegate callback);
        void RoomTemp_Value(Climate_WidgetStringInputSigDelegate callback);
        void RoomHum_Value(Climate_WidgetStringInputSigDelegate callback);
        void FanSpeed_Selected_Name(Climate_WidgetStringInputSigDelegate callback);
        void FanSpeed_1_Name(Climate_WidgetStringInputSigDelegate callback);
        void FanSpeed_2_Name(Climate_WidgetStringInputSigDelegate callback);
        void FanSpeed_3_Name(Climate_WidgetStringInputSigDelegate callback);
        void FanSpeed_4_Name(Climate_WidgetStringInputSigDelegate callback);
        void FanSpeed_5_Name(Climate_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out1(Climate_WidgetStringInputSigDelegate callback);

    }

    public delegate void Climate_WidgetBoolInputSigDelegate(BoolInputSig boolInputSig, IClimate_Widget climate_Widget);
    public delegate void Climate_WidgetUShortInputSigDelegate(UShortInputSig uShortInputSig, IClimate_Widget climate_Widget);
    public delegate void Climate_WidgetStringInputSigDelegate(StringInputSig stringInputSig, IClimate_Widget climate_Widget);

    internal class Climate_Widget : IClimate_Widget, IDisposable
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
                public const uint FanSpeed_1_Select = 5;
                public const uint FanSpeed_2_Select = 6;
                public const uint FanSpeed_3_Select = 7;
                public const uint FanSpeed_4_Select = 8;
                public const uint FanSpeed_5_Select = 9;

                public const uint PowerToggle_FB = 1;
                public const uint Show_FanControlsButton = 4;
                public const uint FanSpeed_1_IsSelected = 5;
                public const uint FanSpeed_2_IsSelected = 6;
                public const uint FanSpeed_3_IsSelected = 7;
                public const uint FanSpeed_4_IsSelected = 8;
                public const uint FanSpeed_5_IsSelected = 9;
                public const uint Show_FanSpeed_1 = 10;
                public const uint Show_FanSpeed_2 = 11;
                public const uint Show_FanSpeed_3 = 12;
                public const uint Show_FanSpeed_4 = 13;
                public const uint Show_FanSpeed_5 = 14;
                public const uint Show_RoomTemp = 16;
                public const uint Show_RoomHumidity = 17;
            }
            internal static class Numerics
            {
                public const uint Setpoint_Set = 1;

                public const uint Setpoint_FB = 1;
                public const uint Thermostat_Step = 2;
            }
            internal static class Strings
            {

                public const uint AC_Status = 1;
                public const uint Setpoint_Value = 2;
                public const uint RoomTemp_Value = 3;
                public const uint RoomHum_Value = 4;
                public const uint FanSpeed_Selected_Name = 5;
                public const uint FanSpeed_1_Name = 6;
                public const uint FanSpeed_2_Name = 7;
                public const uint FanSpeed_3_Name = 8;
                public const uint FanSpeed_4_Name = 9;
                public const uint FanSpeed_5_Name = 10;
                public const uint Ser_Reserved_Out1 = 11;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Climate_Widget(ComponentMediator componentMediator, uint controlJoinId)
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
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.FanSpeed_1_Select, onFanSpeed_1_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.FanSpeed_2_Select, onFanSpeed_2_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.FanSpeed_3_Select, onFanSpeed_3_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.FanSpeed_4_Select, onFanSpeed_4_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.FanSpeed_5_Select, onFanSpeed_5_Select);
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

        public event EventHandler<UIEventArgs> FanSpeed_1_Select;
        private void onFanSpeed_1_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = FanSpeed_1_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> FanSpeed_2_Select;
        private void onFanSpeed_2_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = FanSpeed_2_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> FanSpeed_3_Select;
        private void onFanSpeed_3_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = FanSpeed_3_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> FanSpeed_4_Select;
        private void onFanSpeed_4_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = FanSpeed_4_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> FanSpeed_5_Select;
        private void onFanSpeed_5_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = FanSpeed_5_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void PowerToggle_FB(Climate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.PowerToggle_FB], this);
            }
        }

        public void Show_FanControlsButton(Climate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_FanControlsButton], this);
            }
        }

        public void FanSpeed_1_IsSelected(Climate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.FanSpeed_1_IsSelected], this);
            }
        }

        public void FanSpeed_2_IsSelected(Climate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.FanSpeed_2_IsSelected], this);
            }
        }

        public void FanSpeed_3_IsSelected(Climate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.FanSpeed_3_IsSelected], this);
            }
        }

        public void FanSpeed_4_IsSelected(Climate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.FanSpeed_4_IsSelected], this);
            }
        }

        public void FanSpeed_5_IsSelected(Climate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.FanSpeed_5_IsSelected], this);
            }
        }

        public void Show_FanSpeed_1(Climate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_FanSpeed_1], this);
            }
        }

        public void Show_FanSpeed_2(Climate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_FanSpeed_2], this);
            }
        }

        public void Show_FanSpeed_3(Climate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_FanSpeed_3], this);
            }
        }

        public void Show_FanSpeed_4(Climate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_FanSpeed_4], this);
            }
        }

        public void Show_FanSpeed_5(Climate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_FanSpeed_5], this);
            }
        }

        public void Show_RoomTemp(Climate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_RoomTemp], this);
            }
        }

        public void Show_RoomHumidity(Climate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_RoomHumidity], this);
            }
        }

        public event EventHandler<UIEventArgs> Setpoint_Set;
        private void onSetpoint_Set(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Setpoint_Set;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Setpoint_FB(Climate_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Setpoint_FB], this);
            }
        }

        public void Thermostat_Step(Climate_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Thermostat_Step], this);
            }
        }


        public void AC_Status(Climate_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.AC_Status], this);
            }
        }

        public void Setpoint_Value(Climate_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Setpoint_Value], this);
            }
        }

        public void RoomTemp_Value(Climate_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.RoomTemp_Value], this);
            }
        }

        public void RoomHum_Value(Climate_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.RoomHum_Value], this);
            }
        }

        public void FanSpeed_Selected_Name(Climate_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.FanSpeed_Selected_Name], this);
            }
        }

        public void FanSpeed_1_Name(Climate_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.FanSpeed_1_Name], this);
            }
        }

        public void FanSpeed_2_Name(Climate_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.FanSpeed_2_Name], this);
            }
        }

        public void FanSpeed_3_Name(Climate_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.FanSpeed_3_Name], this);
            }
        }

        public void FanSpeed_4_Name(Climate_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.FanSpeed_4_Name], this);
            }
        }

        public void FanSpeed_5_Name(Climate_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.FanSpeed_5_Name], this);
            }
        }

        public void Ser_Reserved_Out1(Climate_WidgetStringInputSigDelegate callback)
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Climate_Widget", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
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
            FanSpeed_1_Select = null;
            FanSpeed_2_Select = null;
            FanSpeed_3_Select = null;
            FanSpeed_4_Select = null;
            FanSpeed_5_Select = null;
            Setpoint_Set = null;
        }

        #endregion

    }
}
