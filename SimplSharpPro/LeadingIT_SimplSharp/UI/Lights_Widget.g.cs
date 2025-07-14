using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    /// <summary>
    /// //
    /// </summary>
    /// <summary>
    /// //
    /// </summary>
    /// <summary>
    /// //
    /// </summary>
    /// <summary>
    /// High when any light level is higher than 0%
    /// </summary>
    public interface ILights_Widget
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> AllOnScene_Select;
        event EventHandler<UIEventArgs> AllOffScene_Select;
        event EventHandler<UIEventArgs> All_Increment;
        event EventHandler<UIEventArgs> All_Decrement;
        event EventHandler<UIEventArgs> Close_ColorPicker;
        event EventHandler<UIEventArgs> ColorPicker_Red;
        event EventHandler<UIEventArgs> ColorPicker_Green;
        event EventHandler<UIEventArgs> ColorPicker_Blue;

        void LightsNotOff(Lights_WidgetBoolInputSigDelegate callback);
        void AllOnScene_IsSelected(Lights_WidgetBoolInputSigDelegate callback);
        void AllOffScene_IsSelected(Lights_WidgetBoolInputSigDelegate callback);
        void Show_Sensors(Lights_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out1(Lights_WidgetBoolInputSigDelegate callback);
        void NumberOf_Lights(Lights_WidgetUShortInputSigDelegate callback);
        void NumberOf_Sensors(Lights_WidgetUShortInputSigDelegate callback);
        void ColorPicker_Red_FB(Lights_WidgetUShortInputSigDelegate callback);
        void ColorPicker_Green_FB(Lights_WidgetUShortInputSigDelegate callback);
        void ColorPicker_Blue_FB(Lights_WidgetUShortInputSigDelegate callback);

        App_Contract.ILight[] Light { get; }
        App_Contract.ISensor[] Sensor { get; }
        App_Contract.ILights_Scenes Scenes { get; }
    }

    public delegate void Lights_WidgetBoolInputSigDelegate(BoolInputSig boolInputSig, ILights_Widget lights_Widget);
    public delegate void Lights_WidgetUShortInputSigDelegate(UShortInputSig uShortInputSig, ILights_Widget lights_Widget);

    internal class Lights_Widget : ILights_Widget, IDisposable
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
                public const uint AllOnScene_Select = 2;
                public const uint AllOffScene_Select = 3;
                public const uint All_Increment = 4;
                public const uint All_Decrement = 5;
                public const uint Close_ColorPicker = 6;

                public const uint LightsNotOff = 1;
                public const uint AllOnScene_IsSelected = 2;
                public const uint AllOffScene_IsSelected = 3;
                public const uint Show_Sensors = 6;
                public const uint Dig_Reserved_Out1 = 7;
            }
            internal static class Numerics
            {
                public const uint ColorPicker_Red = 3;
                public const uint ColorPicker_Green = 4;
                public const uint ColorPicker_Blue = 5;

                public const uint NumberOf_Lights = 1;
                public const uint NumberOf_Sensors = 2;
                public const uint ColorPicker_Red_FB = 3;
                public const uint ColorPicker_Green_FB = 4;
                public const uint ColorPicker_Blue_FB = 5;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Lights_Widget(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private static readonly IDictionary<uint, List<uint>> LightSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 96, new List<uint> { 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108 } }};
        private static readonly IDictionary<uint, List<uint>> SensorSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 96, new List<uint> { 109, 110, 111, 112, 113, 114, 115, 116, 117, 118 } }};

        internal static void ClearDictionaries()
        {
            LightSmartObjectIdMappings.Clear();
            SensorSmartObjectIdMappings.Clear();
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.AllOnScene_Select, onAllOnScene_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.AllOffScene_Select, onAllOffScene_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.All_Increment, onAll_Increment);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.All_Decrement, onAll_Decrement);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Close_ColorPicker, onClose_ColorPicker);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.ColorPicker_Red, onColorPicker_Red);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.ColorPicker_Green, onColorPicker_Green);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.ColorPicker_Blue, onColorPicker_Blue);

            List<uint> lightList = LightSmartObjectIdMappings[controlJoinId];
            Light = new App_Contract.ILight[lightList.Count];
            for (int index = 0; index < lightList.Count; index++)
            {
                Light[index] = new App_Contract.Light(ComponentMediator, lightList[index]); 
            }

            List<uint> sensorList = SensorSmartObjectIdMappings[controlJoinId];
            Sensor = new App_Contract.ISensor[sensorList.Count];
            for (int index = 0; index < sensorList.Count; index++)
            {
                Sensor[index] = new App_Contract.Sensor(ComponentMediator, sensorList[index]); 
            }

            Scenes = new App_Contract.Lights_Scenes(ComponentMediator, 119);

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Light.Length; index++)
            {
                ((App_Contract.Light)Light[index]).AddDevice(device);
            }
            for (int index = 0; index < Sensor.Length; index++)
            {
                ((App_Contract.Sensor)Sensor[index]).AddDevice(device);
            }
            ((App_Contract.Lights_Scenes)Scenes).AddDevice(device);
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Light.Length; index++)
            {
                ((App_Contract.Light)Light[index]).RemoveDevice(device);
            }
            for (int index = 0; index < Sensor.Length; index++)
            {
                ((App_Contract.Sensor)Sensor[index]).RemoveDevice(device);
            }
            ((App_Contract.Lights_Scenes)Scenes).RemoveDevice(device);
        }

        #endregion

        #region CH5 Contract

        public App_Contract.ILight[] Light { get; private set; }

        public App_Contract.ISensor[] Sensor { get; private set; }

        public App_Contract.ILights_Scenes Scenes { get; private set; }

        public event EventHandler<UIEventArgs> AllOnScene_Select;
        private void onAllOnScene_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AllOnScene_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> AllOffScene_Select;
        private void onAllOffScene_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AllOffScene_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> All_Increment;
        private void onAll_Increment(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = All_Increment;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> All_Decrement;
        private void onAll_Decrement(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = All_Decrement;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Close_ColorPicker;
        private void onClose_ColorPicker(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Close_ColorPicker;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void LightsNotOff(Lights_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.LightsNotOff], this);
            }
        }

        public void AllOnScene_IsSelected(Lights_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.AllOnScene_IsSelected], this);
            }
        }

        public void AllOffScene_IsSelected(Lights_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.AllOffScene_IsSelected], this);
            }
        }

        public void Show_Sensors(Lights_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Sensors], this);
            }
        }

        public void Dig_Reserved_Out1(Lights_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }

        public event EventHandler<UIEventArgs> ColorPicker_Red;
        private void onColorPicker_Red(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ColorPicker_Red;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ColorPicker_Green;
        private void onColorPicker_Green(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ColorPicker_Green;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ColorPicker_Blue;
        private void onColorPicker_Blue(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ColorPicker_Blue;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void NumberOf_Lights(Lights_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Lights], this);
            }
        }

        public void NumberOf_Sensors(Lights_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Sensors], this);
            }
        }

        public void ColorPicker_Red_FB(Lights_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.ColorPicker_Red_FB], this);
            }
        }

        public void ColorPicker_Green_FB(Lights_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.ColorPicker_Green_FB], this);
            }
        }

        public void ColorPicker_Blue_FB(Lights_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.ColorPicker_Blue_FB], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Lights_Widget", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            for (int index = 0; index < Light.Length; index++)
            {
                ((App_Contract.Light)Light[index]).Dispose();
            }
            for (int index = 0; index < Sensor.Length; index++)
            {
                ((App_Contract.Sensor)Sensor[index]).Dispose();
            }
            ((App_Contract.Lights_Scenes)Scenes).Dispose();

            AllOnScene_Select = null;
            AllOffScene_Select = null;
            All_Increment = null;
            All_Decrement = null;
            Close_ColorPicker = null;
            ColorPicker_Red = null;
            ColorPicker_Green = null;
            ColorPicker_Blue = null;
        }

        #endregion

    }
}
