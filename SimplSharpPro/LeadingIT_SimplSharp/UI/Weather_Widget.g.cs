using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IWeather_Widget
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Dig_Reserved_In1;
        event EventHandler<UIEventArgs> Dig_Reserved_In2;
        event EventHandler<UIEventArgs> Dig_Reserved_In3;
        event EventHandler<UIEventArgs> Dig_Reserved_In4;
        event EventHandler<UIEventArgs> Dig_Reserved_In5;

        void Dig_Reserved_Out1(Weather_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out2(Weather_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out3(Weather_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out4(Weather_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out5(Weather_WidgetBoolInputSigDelegate callback);
        void Wind_Direction(Weather_WidgetUShortInputSigDelegate callback);
        void An_Reserved_Out1(Weather_WidgetUShortInputSigDelegate callback);
        void An_Reserved_Out2(Weather_WidgetUShortInputSigDelegate callback);
        void Temp(Weather_WidgetStringInputSigDelegate callback);
        void Humidity(Weather_WidgetStringInputSigDelegate callback);
        void Pressure(Weather_WidgetStringInputSigDelegate callback);
        void UV(Weather_WidgetStringInputSigDelegate callback);
        void Wind_Chill(Weather_WidgetStringInputSigDelegate callback);
        void Heat_Index(Weather_WidgetStringInputSigDelegate callback);
        void In_Sun(Weather_WidgetStringInputSigDelegate callback);
        void In_Shade(Weather_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out1(Weather_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out2(Weather_WidgetStringInputSigDelegate callback);
        void Wind_Dir(Weather_WidgetStringInputSigDelegate callback);
        void Wind_Speed(Weather_WidgetStringInputSigDelegate callback);
        void Wind_10min_avg(Weather_WidgetStringInputSigDelegate callback);
        void Wind_Gust(Weather_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out3(Weather_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out4(Weather_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out5(Weather_WidgetStringInputSigDelegate callback);
        void Dew_Point(Weather_WidgetStringInputSigDelegate callback);
        void Wet_Bulb(Weather_WidgetStringInputSigDelegate callback);
        void Rain_CurrentDay(Weather_WidgetStringInputSigDelegate callback);
        void Rain_24hr(Weather_WidgetStringInputSigDelegate callback);
        void Rain_Month(Weather_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out6(Weather_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out7(Weather_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out8(Weather_WidgetStringInputSigDelegate callback);
        void AQI_Indoor(Weather_WidgetStringInputSigDelegate callback);
        void AQI_Outdoor(Weather_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out9(Weather_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out10(Weather_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out11(Weather_WidgetStringInputSigDelegate callback);

    }

    public delegate void Weather_WidgetBoolInputSigDelegate(BoolInputSig boolInputSig, IWeather_Widget weather_Widget);
    public delegate void Weather_WidgetUShortInputSigDelegate(UShortInputSig uShortInputSig, IWeather_Widget weather_Widget);
    public delegate void Weather_WidgetStringInputSigDelegate(StringInputSig stringInputSig, IWeather_Widget weather_Widget);

    internal class Weather_Widget : IWeather_Widget, IDisposable
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
                public const uint Dig_Reserved_In1 = 1;
                public const uint Dig_Reserved_In2 = 2;
                public const uint Dig_Reserved_In3 = 3;
                public const uint Dig_Reserved_In4 = 4;
                public const uint Dig_Reserved_In5 = 5;

                public const uint Dig_Reserved_Out1 = 1;
                public const uint Dig_Reserved_Out2 = 2;
                public const uint Dig_Reserved_Out3 = 3;
                public const uint Dig_Reserved_Out4 = 4;
                public const uint Dig_Reserved_Out5 = 5;
            }
            internal static class Numerics
            {
                public const uint Wind_Direction = 1;
                public const uint An_Reserved_Out1 = 2;
                public const uint An_Reserved_Out2 = 3;
            }
            internal static class Strings
            {
                public const uint Temp = 1;
                public const uint Humidity = 2;
                public const uint Pressure = 3;
                public const uint UV = 4;
                public const uint Wind_Chill = 5;
                public const uint Heat_Index = 6;
                public const uint In_Sun = 7;
                public const uint In_Shade = 8;
                public const uint Ser_Reserved_Out1 = 9;
                public const uint Ser_Reserved_Out2 = 10;
                public const uint Wind_Dir = 11;
                public const uint Wind_Speed = 12;
                public const uint Wind_10min_avg = 13;
                public const uint Wind_Gust = 14;
                public const uint Ser_Reserved_Out3 = 15;
                public const uint Ser_Reserved_Out4 = 16;
                public const uint Ser_Reserved_Out5 = 17;
                public const uint Dew_Point = 18;
                public const uint Wet_Bulb = 19;
                public const uint Rain_CurrentDay = 20;
                public const uint Rain_24hr = 21;
                public const uint Rain_Month = 22;
                public const uint Ser_Reserved_Out6 = 23;
                public const uint Ser_Reserved_Out7 = 24;
                public const uint Ser_Reserved_Out8 = 25;
                public const uint AQI_Indoor = 26;
                public const uint AQI_Outdoor = 27;
                public const uint Ser_Reserved_Out9 = 28;
                public const uint Ser_Reserved_Out10 = 29;
                public const uint Ser_Reserved_Out11 = 30;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Weather_Widget(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In2, onDig_Reserved_In2);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In3, onDig_Reserved_In3);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In4, onDig_Reserved_In4);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In5, onDig_Reserved_In5);

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

        public event EventHandler<UIEventArgs> Dig_Reserved_In4;
        private void onDig_Reserved_In4(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In4;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dig_Reserved_In5;
        private void onDig_Reserved_In5(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In5;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Dig_Reserved_Out1(Weather_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }

        public void Dig_Reserved_Out2(Weather_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out2], this);
            }
        }

        public void Dig_Reserved_Out3(Weather_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out3], this);
            }
        }

        public void Dig_Reserved_Out4(Weather_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out4], this);
            }
        }

        public void Dig_Reserved_Out5(Weather_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out5], this);
            }
        }

        public void Wind_Direction(Weather_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Wind_Direction], this);
            }
        }

        public void An_Reserved_Out1(Weather_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.An_Reserved_Out1], this);
            }
        }

        public void An_Reserved_Out2(Weather_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.An_Reserved_Out2], this);
            }
        }

        public void Temp(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Temp], this);
            }
        }

        public void Humidity(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Humidity], this);
            }
        }

        public void Pressure(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Pressure], this);
            }
        }

        public void UV(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.UV], this);
            }
        }

        public void Wind_Chill(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Wind_Chill], this);
            }
        }

        public void Heat_Index(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Heat_Index], this);
            }
        }

        public void In_Sun(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.In_Sun], this);
            }
        }

        public void In_Shade(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.In_Shade], this);
            }
        }

        public void Ser_Reserved_Out1(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out1], this);
            }
        }

        public void Ser_Reserved_Out2(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out2], this);
            }
        }

        public void Wind_Dir(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Wind_Dir], this);
            }
        }

        public void Wind_Speed(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Wind_Speed], this);
            }
        }

        public void Wind_10min_avg(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Wind_10min_avg], this);
            }
        }

        public void Wind_Gust(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Wind_Gust], this);
            }
        }

        public void Ser_Reserved_Out3(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out3], this);
            }
        }

        public void Ser_Reserved_Out4(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out4], this);
            }
        }

        public void Ser_Reserved_Out5(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out5], this);
            }
        }

        public void Dew_Point(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Dew_Point], this);
            }
        }

        public void Wet_Bulb(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Wet_Bulb], this);
            }
        }

        public void Rain_CurrentDay(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Rain_CurrentDay], this);
            }
        }

        public void Rain_24hr(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Rain_24hr], this);
            }
        }

        public void Rain_Month(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Rain_Month], this);
            }
        }

        public void Ser_Reserved_Out6(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out6], this);
            }
        }

        public void Ser_Reserved_Out7(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out7], this);
            }
        }

        public void Ser_Reserved_Out8(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out8], this);
            }
        }

        public void AQI_Indoor(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.AQI_Indoor], this);
            }
        }

        public void AQI_Outdoor(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.AQI_Outdoor], this);
            }
        }

        public void Ser_Reserved_Out9(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out9], this);
            }
        }

        public void Ser_Reserved_Out10(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out10], this);
            }
        }

        public void Ser_Reserved_Out11(Weather_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out11], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Weather_Widget", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            Dig_Reserved_In1 = null;
            Dig_Reserved_In2 = null;
            Dig_Reserved_In3 = null;
            Dig_Reserved_In4 = null;
            Dig_Reserved_In5 = null;
        }

        #endregion

    }
}
