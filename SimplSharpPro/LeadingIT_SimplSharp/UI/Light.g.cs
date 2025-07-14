using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    /// <summary>
    /// On-100%,Off-0%
    /// </summary>
    /// <summary>
    /// Light Level
    /// </summary>
    /// <summary>
    /// High when above 0%
    /// </summary>
    /// <summary>
    /// Light Level
    /// </summary>
    /// <summary>
    /// Light Level String
    /// </summary>
    public interface ILight
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> On_Off_Toggle;
        event EventHandler<UIEventArgs> ColorChip_Select;
        event EventHandler<UIEventArgs> Level_Set;
        event EventHandler<UIEventArgs> An_Reserved_In1;

        void OnOffToggle_FB(LightBoolInputSigDelegate callback);
        void Show_Slider(LightBoolInputSigDelegate callback);
        void Show_ColorChip(LightBoolInputSigDelegate callback);
        void Show_LevelValue(LightBoolInputSigDelegate callback);
        void Level_FB(LightUShortInputSigDelegate callback);
        void An_Reserved_Out1(LightUShortInputSigDelegate callback);
        void ColorChip_Red_FB(LightUShortInputSigDelegate callback);
        void ColorChip_Green_FB(LightUShortInputSigDelegate callback);
        void ColorChip_Blue_FB(LightUShortInputSigDelegate callback);
        void Name(LightStringInputSigDelegate callback);
        void Level_FB_String(LightStringInputSigDelegate callback);

    }

    public delegate void LightBoolInputSigDelegate(BoolInputSig boolInputSig, ILight light);
    public delegate void LightUShortInputSigDelegate(UShortInputSig uShortInputSig, ILight light);
    public delegate void LightStringInputSigDelegate(StringInputSig stringInputSig, ILight light);

    internal class Light : ILight, IDisposable
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
                public const uint On_Off_Toggle = 1;
                public const uint ColorChip_Select = 4;

                public const uint OnOffToggle_FB = 1;
                public const uint Show_Slider = 2;
                public const uint Show_ColorChip = 3;
                public const uint Show_LevelValue = 5;
            }
            internal static class Numerics
            {
                public const uint Level_Set = 1;
                public const uint An_Reserved_In1 = 2;

                public const uint Level_FB = 1;
                public const uint An_Reserved_Out1 = 2;
                public const uint ColorChip_Red_FB = 3;
                public const uint ColorChip_Green_FB = 4;
                public const uint ColorChip_Blue_FB = 5;
            }
            internal static class Strings
            {

                public const uint Name = 1;
                public const uint Level_FB_String = 2;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Light(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.On_Off_Toggle, onOn_Off_Toggle);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ColorChip_Select, onColorChip_Select);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.Level_Set, onLevel_Set);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.An_Reserved_In1, onAn_Reserved_In1);

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

        public event EventHandler<UIEventArgs> On_Off_Toggle;
        private void onOn_Off_Toggle(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = On_Off_Toggle;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ColorChip_Select;
        private void onColorChip_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ColorChip_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void OnOffToggle_FB(LightBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.OnOffToggle_FB], this);
            }
        }

        public void Show_Slider(LightBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Slider], this);
            }
        }

        public void Show_ColorChip(LightBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_ColorChip], this);
            }
        }

        public void Show_LevelValue(LightBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_LevelValue], this);
            }
        }

        public event EventHandler<UIEventArgs> Level_Set;
        private void onLevel_Set(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Level_Set;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> An_Reserved_In1;
        private void onAn_Reserved_In1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = An_Reserved_In1;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Level_FB(LightUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Level_FB], this);
            }
        }

        public void An_Reserved_Out1(LightUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.An_Reserved_Out1], this);
            }
        }

        public void ColorChip_Red_FB(LightUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.ColorChip_Red_FB], this);
            }
        }

        public void ColorChip_Green_FB(LightUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.ColorChip_Green_FB], this);
            }
        }

        public void ColorChip_Blue_FB(LightUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.ColorChip_Blue_FB], this);
            }
        }


        public void Name(LightStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Name], this);
            }
        }

        public void Level_FB_String(LightStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Level_FB_String], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Light", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            On_Off_Toggle = null;
            ColorChip_Select = null;
            Level_Set = null;
            An_Reserved_In1 = null;
        }

        #endregion

    }
}
