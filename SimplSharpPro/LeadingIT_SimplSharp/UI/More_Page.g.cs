using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IMore_Page
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Theme_1_Select;
        event EventHandler<UIEventArgs> Theme_2_Select;
        event EventHandler<UIEventArgs> Theme_3_Select;
        event EventHandler<UIEventArgs> Theme_4_Select;
        event EventHandler<UIEventArgs> Theme_5_Select;
        event EventHandler<UIEventArgs> Dig_Reserved_In1;
        event EventHandler<UIEventArgs> Dig_Reserved_In2;
        event EventHandler<UIEventArgs> AutoTheme_Enable;
        event EventHandler<UIEventArgs> Scr_Brightness_Set;
        event EventHandler<UIEventArgs> KeypadsColorPicker_Red;
        event EventHandler<UIEventArgs> KeypadsColorPicker_Green;
        event EventHandler<UIEventArgs> KeypadsColorPicker_Blue;
        event EventHandler<UIEventArgs> KeypadsBrightness_Set;

        void Theme_1_IsSelected(More_PageBoolInputSigDelegate callback);
        void Theme_2_IsSelected(More_PageBoolInputSigDelegate callback);
        void Theme_3_IsSelected(More_PageBoolInputSigDelegate callback);
        void Theme_4_IsSelected(More_PageBoolInputSigDelegate callback);
        void Theme_5_IsSelected(More_PageBoolInputSigDelegate callback);
        void Dig_Reserved_Out1(More_PageBoolInputSigDelegate callback);
        void Dig_Reserved_Out2(More_PageBoolInputSigDelegate callback);
        void Show_Scr_Brightness(More_PageBoolInputSigDelegate callback);
        void Show_Menu(More_PageBoolInputSigDelegate callback);
        void AutoTheme_Enable_FB(More_PageBoolInputSigDelegate callback);
        void Show_Status(More_PageBoolInputSigDelegate callback);
        void Show_KeypadsBacklight(More_PageBoolInputSigDelegate callback);
        void Show_KeypadsColorChip(More_PageBoolInputSigDelegate callback);
        void Show_FullScreenMode_Btn(More_PageBoolInputSigDelegate callback);
        void NumberOf_Menu_Items(More_PageUShortInputSigDelegate callback);
        void Scr_Brightness_FB(More_PageUShortInputSigDelegate callback);
        void KeypadsColorPicker_Red_FB(More_PageUShortInputSigDelegate callback);
        void KeypadsColorPicker_Green_FB(More_PageUShortInputSigDelegate callback);
        void KeypadsColorPicker_Blue_FB(More_PageUShortInputSigDelegate callback);
        void KeypadsBrightness_FB(More_PageUShortInputSigDelegate callback);
        void Status_Text(More_PageStringInputSigDelegate callback);

        App_Contract.IMorePage_Menu_Item[] Menu_Item { get; }
    }

    public delegate void More_PageBoolInputSigDelegate(BoolInputSig boolInputSig, IMore_Page more_Page);
    public delegate void More_PageUShortInputSigDelegate(UShortInputSig uShortInputSig, IMore_Page more_Page);
    public delegate void More_PageStringInputSigDelegate(StringInputSig stringInputSig, IMore_Page more_Page);

    internal class More_Page : IMore_Page, IDisposable
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
                public const uint Theme_1_Select = 1;
                public const uint Theme_2_Select = 2;
                public const uint Theme_3_Select = 3;
                public const uint Theme_4_Select = 4;
                public const uint Theme_5_Select = 5;
                public const uint Dig_Reserved_In1 = 6;
                public const uint Dig_Reserved_In2 = 7;
                public const uint AutoTheme_Enable = 11;

                public const uint Theme_1_IsSelected = 1;
                public const uint Theme_2_IsSelected = 2;
                public const uint Theme_3_IsSelected = 3;
                public const uint Theme_4_IsSelected = 4;
                public const uint Theme_5_IsSelected = 5;
                public const uint Dig_Reserved_Out1 = 6;
                public const uint Dig_Reserved_Out2 = 7;
                public const uint Show_Scr_Brightness = 8;
                public const uint Show_Menu = 9;
                public const uint AutoTheme_Enable_FB = 11;
                public const uint Show_Status = 13;
                public const uint Show_KeypadsBacklight = 14;
                public const uint Show_KeypadsColorChip = 15;
                public const uint Show_FullScreenMode_Btn = 16;
            }
            internal static class Numerics
            {
                public const uint Scr_Brightness_Set = 2;
                public const uint KeypadsColorPicker_Red = 3;
                public const uint KeypadsColorPicker_Green = 4;
                public const uint KeypadsColorPicker_Blue = 5;
                public const uint KeypadsBrightness_Set = 6;

                public const uint NumberOf_Menu_Items = 1;
                public const uint Scr_Brightness_FB = 2;
                public const uint KeypadsColorPicker_Red_FB = 3;
                public const uint KeypadsColorPicker_Green_FB = 4;
                public const uint KeypadsColorPicker_Blue_FB = 5;
                public const uint KeypadsBrightness_FB = 6;
            }
            internal static class Strings
            {

                public const uint Status_Text = 1;
            }
        }

        #endregion

        #region Construction and Initialization

        internal More_Page(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private static readonly IDictionary<uint, List<uint>> Menu_ItemSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 294, new List<uint> { 295, 296, 297, 298, 299 } }};

        internal static void ClearDictionaries()
        {
            Menu_ItemSmartObjectIdMappings.Clear();
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Theme_1_Select, onTheme_1_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Theme_2_Select, onTheme_2_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Theme_3_Select, onTheme_3_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Theme_4_Select, onTheme_4_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Theme_5_Select, onTheme_5_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In2, onDig_Reserved_In2);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.AutoTheme_Enable, onAutoTheme_Enable);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.Scr_Brightness_Set, onScr_Brightness_Set);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.KeypadsColorPicker_Red, onKeypadsColorPicker_Red);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.KeypadsColorPicker_Green, onKeypadsColorPicker_Green);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.KeypadsColorPicker_Blue, onKeypadsColorPicker_Blue);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.KeypadsBrightness_Set, onKeypadsBrightness_Set);

            List<uint> menu_ItemList = Menu_ItemSmartObjectIdMappings[controlJoinId];
            Menu_Item = new App_Contract.IMorePage_Menu_Item[menu_ItemList.Count];
            for (int index = 0; index < menu_ItemList.Count; index++)
            {
                Menu_Item[index] = new App_Contract.MorePage_Menu_Item(ComponentMediator, menu_ItemList[index]); 
            }

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Menu_Item.Length; index++)
            {
                ((App_Contract.MorePage_Menu_Item)Menu_Item[index]).AddDevice(device);
            }
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Menu_Item.Length; index++)
            {
                ((App_Contract.MorePage_Menu_Item)Menu_Item[index]).RemoveDevice(device);
            }
        }

        #endregion

        #region CH5 Contract

        public App_Contract.IMorePage_Menu_Item[] Menu_Item { get; private set; }

        public event EventHandler<UIEventArgs> Theme_1_Select;
        private void onTheme_1_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Theme_1_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Theme_2_Select;
        private void onTheme_2_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Theme_2_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Theme_3_Select;
        private void onTheme_3_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Theme_3_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Theme_4_Select;
        private void onTheme_4_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Theme_4_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Theme_5_Select;
        private void onTheme_5_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Theme_5_Select;
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

        public event EventHandler<UIEventArgs> AutoTheme_Enable;
        private void onAutoTheme_Enable(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AutoTheme_Enable;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Theme_1_IsSelected(More_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Theme_1_IsSelected], this);
            }
        }

        public void Theme_2_IsSelected(More_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Theme_2_IsSelected], this);
            }
        }

        public void Theme_3_IsSelected(More_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Theme_3_IsSelected], this);
            }
        }

        public void Theme_4_IsSelected(More_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Theme_4_IsSelected], this);
            }
        }

        public void Theme_5_IsSelected(More_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Theme_5_IsSelected], this);
            }
        }

        public void Dig_Reserved_Out1(More_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }

        public void Dig_Reserved_Out2(More_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out2], this);
            }
        }

        public void Show_Scr_Brightness(More_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Scr_Brightness], this);
            }
        }

        public void Show_Menu(More_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Menu], this);
            }
        }

        public void AutoTheme_Enable_FB(More_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.AutoTheme_Enable_FB], this);
            }
        }

        public void Show_Status(More_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Status], this);
            }
        }

        public void Show_KeypadsBacklight(More_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_KeypadsBacklight], this);
            }
        }

        public void Show_KeypadsColorChip(More_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_KeypadsColorChip], this);
            }
        }

        public void Show_FullScreenMode_Btn(More_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_FullScreenMode_Btn], this);
            }
        }

        public event EventHandler<UIEventArgs> Scr_Brightness_Set;
        private void onScr_Brightness_Set(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Scr_Brightness_Set;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> KeypadsColorPicker_Red;
        private void onKeypadsColorPicker_Red(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = KeypadsColorPicker_Red;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> KeypadsColorPicker_Green;
        private void onKeypadsColorPicker_Green(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = KeypadsColorPicker_Green;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> KeypadsColorPicker_Blue;
        private void onKeypadsColorPicker_Blue(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = KeypadsColorPicker_Blue;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> KeypadsBrightness_Set;
        private void onKeypadsBrightness_Set(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = KeypadsBrightness_Set;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void NumberOf_Menu_Items(More_PageUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Menu_Items], this);
            }
        }

        public void Scr_Brightness_FB(More_PageUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Scr_Brightness_FB], this);
            }
        }

        public void KeypadsColorPicker_Red_FB(More_PageUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.KeypadsColorPicker_Red_FB], this);
            }
        }

        public void KeypadsColorPicker_Green_FB(More_PageUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.KeypadsColorPicker_Green_FB], this);
            }
        }

        public void KeypadsColorPicker_Blue_FB(More_PageUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.KeypadsColorPicker_Blue_FB], this);
            }
        }

        public void KeypadsBrightness_FB(More_PageUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.KeypadsBrightness_FB], this);
            }
        }


        public void Status_Text(More_PageStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Status_Text], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "More_Page", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            for (int index = 0; index < Menu_Item.Length; index++)
            {
                ((App_Contract.MorePage_Menu_Item)Menu_Item[index]).Dispose();
            }

            Theme_1_Select = null;
            Theme_2_Select = null;
            Theme_3_Select = null;
            Theme_4_Select = null;
            Theme_5_Select = null;
            Dig_Reserved_In1 = null;
            Dig_Reserved_In2 = null;
            AutoTheme_Enable = null;
            Scr_Brightness_Set = null;
            KeypadsColorPicker_Red = null;
            KeypadsColorPicker_Green = null;
            KeypadsColorPicker_Blue = null;
            KeypadsBrightness_Set = null;
        }

        #endregion

    }
}
