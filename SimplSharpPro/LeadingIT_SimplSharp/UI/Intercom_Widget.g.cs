using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IIntercom_Widget
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> CamOff_Toggle;
        event EventHandler<UIEventArgs> Widget_Select;
        event EventHandler<UIEventArgs> Widget_Close;
        event EventHandler<UIEventArgs> Location0_Select;
        event EventHandler<UIEventArgs> Location1_Select;
        event EventHandler<UIEventArgs> Location2_Select;
        event EventHandler<UIEventArgs> Location3_Select;
        event EventHandler<UIEventArgs> Location4_Select;
        event EventHandler<UIEventArgs> Location5_Select;
        event EventHandler<UIEventArgs> Dig_Reserved_In3;
        event EventHandler<UIEventArgs> Dig_Reserved_In4;
        event EventHandler<UIEventArgs> Dig_Reserved_In5;
        event EventHandler<UIEventArgs> Dig_Reserved_In6;
        event EventHandler<UIEventArgs> Answer_Select;
        event EventHandler<UIEventArgs> HangUp_Select;
        event EventHandler<UIEventArgs> DND_Select;
        event EventHandler<UIEventArgs> DoorUnlock_Select;
        event EventHandler<UIEventArgs> PTT_Select;
        event EventHandler<UIEventArgs> ExtraFunc0_Select;
        event EventHandler<UIEventArgs> ExtraFunc1_Select;
        event EventHandler<UIEventArgs> Dig_Reserved_In7;
        event EventHandler<UIEventArgs> Dig_Reserved_In8;
        event EventHandler<UIEventArgs> Dig_Reserved_In9;
        event EventHandler<UIEventArgs> Cam0_Select;
        event EventHandler<UIEventArgs> Cam1_Select;
        event EventHandler<UIEventArgs> Cam2_Select;
        event EventHandler<UIEventArgs> Dig_Reserved_In10;
        event EventHandler<UIEventArgs> Vol_Up;
        event EventHandler<UIEventArgs> Vol_Down;
        event EventHandler<UIEventArgs> Mute_Toggle;
        event EventHandler<UIEventArgs> Dig_Reserved_In11;
        event EventHandler<UIEventArgs> Dig_Reserved_In12;
        event EventHandler<UIEventArgs> Dig_Reserved_In13;
        event EventHandler<UIEventArgs> Dig_Reserved_In14;
        event EventHandler<UIEventArgs> Vol_Level_Set;
        event EventHandler<UIEventArgs> An_Reserved_In1;
        event EventHandler<UIEventArgs> An_Reserved_In2;
        event EventHandler<UIEventArgs> An_Reserved_In3;
        event EventHandler<UIEventArgs> Ser_Reserved_In7;
        event EventHandler<UIEventArgs> Ser_Reserved_In8;
        event EventHandler<UIEventArgs> Ser_Reserved_In9;

        void Show_Intercom_Icon(Intercom_WidgetBoolInputSigDelegate callback);
        void Show_ContactList_Icon(Intercom_WidgetBoolInputSigDelegate callback);
        void Show_DialPad_Icon(Intercom_WidgetBoolInputSigDelegate callback);
        void Show_IncomingCall_Page(Intercom_WidgetBoolInputSigDelegate callback);
        void CamOff_Toggle_FB(Intercom_WidgetBoolInputSigDelegate callback);
        void Show_CamOff_Icon(Intercom_WidgetBoolInputSigDelegate callback);
        void Widget_IsSelected(Intercom_WidgetBoolInputSigDelegate callback);
        void Show_LocationsList(Intercom_WidgetBoolInputSigDelegate callback);
        void Location0_IsSelected(Intercom_WidgetBoolInputSigDelegate callback);
        void Location1_IsSelected(Intercom_WidgetBoolInputSigDelegate callback);
        void Location2_IsSelected(Intercom_WidgetBoolInputSigDelegate callback);
        void Location3_IsSelected(Intercom_WidgetBoolInputSigDelegate callback);
        void Location4_IsSelected(Intercom_WidgetBoolInputSigDelegate callback);
        void Location5_IsSelected(Intercom_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out3(Intercom_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out4(Intercom_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out5(Intercom_WidgetBoolInputSigDelegate callback);
        void Show_DoorUnlock_Btn(Intercom_WidgetBoolInputSigDelegate callback);
        void Show_PTT_Btn(Intercom_WidgetBoolInputSigDelegate callback);
        void Show_ExtraFunc0_Btn(Intercom_WidgetBoolInputSigDelegate callback);
        void Show_ExtraFunc1_Btn(Intercom_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out6(Intercom_WidgetBoolInputSigDelegate callback);
        void Answer_IsSelected(Intercom_WidgetBoolInputSigDelegate callback);
        void DND_IsSelected(Intercom_WidgetBoolInputSigDelegate callback);
        void DoorUnlock_IsSelected(Intercom_WidgetBoolInputSigDelegate callback);
        void PTT_IsSelected(Intercom_WidgetBoolInputSigDelegate callback);
        void ExtraFunc0_IsSelected(Intercom_WidgetBoolInputSigDelegate callback);
        void ExtraFunc1_IsSelected(Intercom_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out7(Intercom_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out8(Intercom_WidgetBoolInputSigDelegate callback);
        void Show_Cam0(Intercom_WidgetBoolInputSigDelegate callback);
        void Show_Cam1(Intercom_WidgetBoolInputSigDelegate callback);
        void Show_Cam2(Intercom_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out9(Intercom_WidgetBoolInputSigDelegate callback);
        void Cam0_IsSelected(Intercom_WidgetBoolInputSigDelegate callback);
        void Cam1_IsSelected(Intercom_WidgetBoolInputSigDelegate callback);
        void Cam2_IsSelected(Intercom_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out10(Intercom_WidgetBoolInputSigDelegate callback);
        void Mute_Toggle_FB(Intercom_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out11(Intercom_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out12(Intercom_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out13(Intercom_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out14(Intercom_WidgetBoolInputSigDelegate callback);
        void NumberOf_Locations(Intercom_WidgetUShortInputSigDelegate callback);
        void Vol_Level_FB(Intercom_WidgetUShortInputSigDelegate callback);
        void An_Reserved_Out1(Intercom_WidgetUShortInputSigDelegate callback);
        void An_Reserved_Out2(Intercom_WidgetUShortInputSigDelegate callback);
        void An_Reserved_Out3(Intercom_WidgetUShortInputSigDelegate callback);
        void Video_URL(Intercom_WidgetStringInputSigDelegate callback);
        void SelectedLocation_Name(Intercom_WidgetStringInputSigDelegate callback);
        void Location0_Name(Intercom_WidgetStringInputSigDelegate callback);
        void Location1_Name(Intercom_WidgetStringInputSigDelegate callback);
        void Location2_Name(Intercom_WidgetStringInputSigDelegate callback);
        void Location3_Name(Intercom_WidgetStringInputSigDelegate callback);
        void Location4_Name(Intercom_WidgetStringInputSigDelegate callback);
        void Location5_Name(Intercom_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out1(Intercom_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out2(Intercom_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out3(Intercom_WidgetStringInputSigDelegate callback);
        void ExtraFunc0_Name(Intercom_WidgetStringInputSigDelegate callback);
        void ExtraFunc1_Name(Intercom_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out4(Intercom_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out5(Intercom_WidgetStringInputSigDelegate callback);
        void Cam0_Name(Intercom_WidgetStringInputSigDelegate callback);
        void Cam1_Name(Intercom_WidgetStringInputSigDelegate callback);
        void Cam2_Name(Intercom_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out6(Intercom_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out7(Intercom_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out8(Intercom_WidgetStringInputSigDelegate callback);
        void Ser_Reserved_Out9(Intercom_WidgetStringInputSigDelegate callback);

        App_Contract.IIntercom_Dialpad Dialpad { get; }
        App_Contract.IIntercom_ContactList ContactList { get; }
    }

    public delegate void Intercom_WidgetBoolInputSigDelegate(BoolInputSig boolInputSig, IIntercom_Widget intercom_Widget);
    public delegate void Intercom_WidgetUShortInputSigDelegate(UShortInputSig uShortInputSig, IIntercom_Widget intercom_Widget);
    public delegate void Intercom_WidgetStringInputSigDelegate(StringInputSig stringInputSig, IIntercom_Widget intercom_Widget);

    internal class Intercom_Widget : IIntercom_Widget, IDisposable
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
                public const uint CamOff_Toggle = 5;
                public const uint Widget_Select = 6;
                public const uint Widget_Close = 7;
                public const uint Location0_Select = 10;
                public const uint Location1_Select = 11;
                public const uint Location2_Select = 12;
                public const uint Location3_Select = 13;
                public const uint Location4_Select = 14;
                public const uint Location5_Select = 15;
                public const uint Dig_Reserved_In3 = 16;
                public const uint Dig_Reserved_In4 = 17;
                public const uint Dig_Reserved_In5 = 18;
                public const uint Dig_Reserved_In6 = 24;
                public const uint Answer_Select = 25;
                public const uint HangUp_Select = 26;
                public const uint DND_Select = 27;
                public const uint DoorUnlock_Select = 28;
                public const uint PTT_Select = 29;
                public const uint ExtraFunc0_Select = 30;
                public const uint ExtraFunc1_Select = 31;
                public const uint Dig_Reserved_In7 = 32;
                public const uint Dig_Reserved_In8 = 33;
                public const uint Dig_Reserved_In9 = 38;
                public const uint Cam0_Select = 39;
                public const uint Cam1_Select = 40;
                public const uint Cam2_Select = 41;
                public const uint Dig_Reserved_In10 = 42;
                public const uint Vol_Up = 44;
                public const uint Vol_Down = 45;
                public const uint Mute_Toggle = 46;
                public const uint Dig_Reserved_In11 = 48;
                public const uint Dig_Reserved_In12 = 49;
                public const uint Dig_Reserved_In13 = 50;
                public const uint Dig_Reserved_In14 = 51;

                public const uint Show_Intercom_Icon = 1;
                public const uint Show_ContactList_Icon = 2;
                public const uint Show_DialPad_Icon = 3;
                public const uint Show_IncomingCall_Page = 4;
                public const uint CamOff_Toggle_FB = 5;
                public const uint Show_CamOff_Icon = 6;
                public const uint Widget_IsSelected = 7;
                public const uint Show_LocationsList = 9;
                public const uint Location0_IsSelected = 10;
                public const uint Location1_IsSelected = 11;
                public const uint Location2_IsSelected = 12;
                public const uint Location3_IsSelected = 13;
                public const uint Location4_IsSelected = 14;
                public const uint Location5_IsSelected = 15;
                public const uint Dig_Reserved_Out3 = 16;
                public const uint Dig_Reserved_Out4 = 17;
                public const uint Dig_Reserved_Out5 = 18;
                public const uint Show_DoorUnlock_Btn = 20;
                public const uint Show_PTT_Btn = 21;
                public const uint Show_ExtraFunc0_Btn = 22;
                public const uint Show_ExtraFunc1_Btn = 23;
                public const uint Dig_Reserved_Out6 = 24;
                public const uint Answer_IsSelected = 25;
                public const uint DND_IsSelected = 27;
                public const uint DoorUnlock_IsSelected = 28;
                public const uint PTT_IsSelected = 29;
                public const uint ExtraFunc0_IsSelected = 30;
                public const uint ExtraFunc1_IsSelected = 31;
                public const uint Dig_Reserved_Out7 = 32;
                public const uint Dig_Reserved_Out8 = 33;
                public const uint Show_Cam0 = 35;
                public const uint Show_Cam1 = 36;
                public const uint Show_Cam2 = 37;
                public const uint Dig_Reserved_Out9 = 38;
                public const uint Cam0_IsSelected = 39;
                public const uint Cam1_IsSelected = 40;
                public const uint Cam2_IsSelected = 41;
                public const uint Dig_Reserved_Out10 = 42;
                public const uint Mute_Toggle_FB = 46;
                public const uint Dig_Reserved_Out11 = 48;
                public const uint Dig_Reserved_Out12 = 49;
                public const uint Dig_Reserved_Out13 = 50;
                public const uint Dig_Reserved_Out14 = 51;
            }
            internal static class Numerics
            {
                public const uint Vol_Level_Set = 2;
                public const uint An_Reserved_In1 = 3;
                public const uint An_Reserved_In2 = 4;
                public const uint An_Reserved_In3 = 5;

                public const uint NumberOf_Locations = 1;
                public const uint Vol_Level_FB = 2;
                public const uint An_Reserved_Out1 = 3;
                public const uint An_Reserved_Out2 = 4;
                public const uint An_Reserved_Out3 = 5;
            }
            internal static class Strings
            {
                public const uint Ser_Reserved_In7 = 24;
                public const uint Ser_Reserved_In8 = 25;
                public const uint Ser_Reserved_In9 = 26;

                public const uint Video_URL = 1;
                public const uint SelectedLocation_Name = 2;
                public const uint Location0_Name = 4;
                public const uint Location1_Name = 5;
                public const uint Location2_Name = 6;
                public const uint Location3_Name = 7;
                public const uint Location4_Name = 8;
                public const uint Location5_Name = 9;
                public const uint Ser_Reserved_Out1 = 10;
                public const uint Ser_Reserved_Out2 = 11;
                public const uint Ser_Reserved_Out3 = 12;
                public const uint ExtraFunc0_Name = 14;
                public const uint ExtraFunc1_Name = 15;
                public const uint Ser_Reserved_Out4 = 16;
                public const uint Ser_Reserved_Out5 = 17;
                public const uint Cam0_Name = 19;
                public const uint Cam1_Name = 20;
                public const uint Cam2_Name = 21;
                public const uint Ser_Reserved_Out6 = 22;
                public const uint Ser_Reserved_Out7 = 24;
                public const uint Ser_Reserved_Out8 = 25;
                public const uint Ser_Reserved_Out9 = 26;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Intercom_Widget(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        internal static void ClearDictionaries()
        {
            App_Contract.Intercom_ContactList.ClearDictionaries();
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CamOff_Toggle, onCamOff_Toggle);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Widget_Select, onWidget_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Widget_Close, onWidget_Close);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Location0_Select, onLocation0_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Location1_Select, onLocation1_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Location2_Select, onLocation2_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Location3_Select, onLocation3_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Location4_Select, onLocation4_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Location5_Select, onLocation5_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In3, onDig_Reserved_In3);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In4, onDig_Reserved_In4);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In5, onDig_Reserved_In5);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In6, onDig_Reserved_In6);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Answer_Select, onAnswer_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.HangUp_Select, onHangUp_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.DND_Select, onDND_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.DoorUnlock_Select, onDoorUnlock_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.PTT_Select, onPTT_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ExtraFunc0_Select, onExtraFunc0_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ExtraFunc1_Select, onExtraFunc1_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In7, onDig_Reserved_In7);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In8, onDig_Reserved_In8);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In9, onDig_Reserved_In9);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Cam0_Select, onCam0_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Cam1_Select, onCam1_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Cam2_Select, onCam2_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In10, onDig_Reserved_In10);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Vol_Up, onVol_Up);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Vol_Down, onVol_Down);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Mute_Toggle, onMute_Toggle);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In11, onDig_Reserved_In11);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In12, onDig_Reserved_In12);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In13, onDig_Reserved_In13);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In14, onDig_Reserved_In14);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.Vol_Level_Set, onVol_Level_Set);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.An_Reserved_In1, onAn_Reserved_In1);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.An_Reserved_In2, onAn_Reserved_In2);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.An_Reserved_In3, onAn_Reserved_In3);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.Ser_Reserved_In7, onSer_Reserved_In7);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.Ser_Reserved_In8, onSer_Reserved_In8);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.Ser_Reserved_In9, onSer_Reserved_In9);

            Dialpad = new App_Contract.Intercom_Dialpad(ComponentMediator, 176);

            ContactList = new App_Contract.Intercom_ContactList(ComponentMediator, 177);

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            ((App_Contract.Intercom_Dialpad)Dialpad).AddDevice(device);
            ((App_Contract.Intercom_ContactList)ContactList).AddDevice(device);
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            ((App_Contract.Intercom_Dialpad)Dialpad).RemoveDevice(device);
            ((App_Contract.Intercom_ContactList)ContactList).RemoveDevice(device);
        }

        #endregion

        #region CH5 Contract

        public App_Contract.IIntercom_Dialpad Dialpad { get; private set; }

        public App_Contract.IIntercom_ContactList ContactList { get; private set; }

        public event EventHandler<UIEventArgs> CamOff_Toggle;
        private void onCamOff_Toggle(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CamOff_Toggle;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Widget_Select;
        private void onWidget_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Widget_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Widget_Close;
        private void onWidget_Close(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Widget_Close;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Location0_Select;
        private void onLocation0_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Location0_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Location1_Select;
        private void onLocation1_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Location1_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Location2_Select;
        private void onLocation2_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Location2_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Location3_Select;
        private void onLocation3_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Location3_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Location4_Select;
        private void onLocation4_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Location4_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Location5_Select;
        private void onLocation5_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Location5_Select;
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

        public event EventHandler<UIEventArgs> Dig_Reserved_In6;
        private void onDig_Reserved_In6(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In6;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Answer_Select;
        private void onAnswer_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Answer_Select;
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

        public event EventHandler<UIEventArgs> DND_Select;
        private void onDND_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = DND_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> DoorUnlock_Select;
        private void onDoorUnlock_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = DoorUnlock_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> PTT_Select;
        private void onPTT_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = PTT_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ExtraFunc0_Select;
        private void onExtraFunc0_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ExtraFunc0_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ExtraFunc1_Select;
        private void onExtraFunc1_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ExtraFunc1_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dig_Reserved_In7;
        private void onDig_Reserved_In7(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In7;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dig_Reserved_In8;
        private void onDig_Reserved_In8(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In8;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dig_Reserved_In9;
        private void onDig_Reserved_In9(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In9;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Cam0_Select;
        private void onCam0_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Cam0_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Cam1_Select;
        private void onCam1_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Cam1_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Cam2_Select;
        private void onCam2_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Cam2_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dig_Reserved_In10;
        private void onDig_Reserved_In10(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In10;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Vol_Up;
        private void onVol_Up(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Vol_Up;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Vol_Down;
        private void onVol_Down(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Vol_Down;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Mute_Toggle;
        private void onMute_Toggle(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Mute_Toggle;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dig_Reserved_In11;
        private void onDig_Reserved_In11(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In11;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dig_Reserved_In12;
        private void onDig_Reserved_In12(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In12;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dig_Reserved_In13;
        private void onDig_Reserved_In13(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In13;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dig_Reserved_In14;
        private void onDig_Reserved_In14(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In14;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Show_Intercom_Icon(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Intercom_Icon], this);
            }
        }

        public void Show_ContactList_Icon(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_ContactList_Icon], this);
            }
        }

        public void Show_DialPad_Icon(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_DialPad_Icon], this);
            }
        }

        public void Show_IncomingCall_Page(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_IncomingCall_Page], this);
            }
        }

        public void CamOff_Toggle_FB(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.CamOff_Toggle_FB], this);
            }
        }

        public void Show_CamOff_Icon(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_CamOff_Icon], this);
            }
        }

        public void Widget_IsSelected(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Widget_IsSelected], this);
            }
        }

        public void Show_LocationsList(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_LocationsList], this);
            }
        }

        public void Location0_IsSelected(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Location0_IsSelected], this);
            }
        }

        public void Location1_IsSelected(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Location1_IsSelected], this);
            }
        }

        public void Location2_IsSelected(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Location2_IsSelected], this);
            }
        }

        public void Location3_IsSelected(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Location3_IsSelected], this);
            }
        }

        public void Location4_IsSelected(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Location4_IsSelected], this);
            }
        }

        public void Location5_IsSelected(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Location5_IsSelected], this);
            }
        }

        public void Dig_Reserved_Out3(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out3], this);
            }
        }

        public void Dig_Reserved_Out4(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out4], this);
            }
        }

        public void Dig_Reserved_Out5(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out5], this);
            }
        }

        public void Show_DoorUnlock_Btn(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_DoorUnlock_Btn], this);
            }
        }

        public void Show_PTT_Btn(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_PTT_Btn], this);
            }
        }

        public void Show_ExtraFunc0_Btn(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_ExtraFunc0_Btn], this);
            }
        }

        public void Show_ExtraFunc1_Btn(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_ExtraFunc1_Btn], this);
            }
        }

        public void Dig_Reserved_Out6(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out6], this);
            }
        }

        public void Answer_IsSelected(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Answer_IsSelected], this);
            }
        }

        public void DND_IsSelected(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.DND_IsSelected], this);
            }
        }

        public void DoorUnlock_IsSelected(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.DoorUnlock_IsSelected], this);
            }
        }

        public void PTT_IsSelected(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.PTT_IsSelected], this);
            }
        }

        public void ExtraFunc0_IsSelected(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.ExtraFunc0_IsSelected], this);
            }
        }

        public void ExtraFunc1_IsSelected(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.ExtraFunc1_IsSelected], this);
            }
        }

        public void Dig_Reserved_Out7(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out7], this);
            }
        }

        public void Dig_Reserved_Out8(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out8], this);
            }
        }

        public void Show_Cam0(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Cam0], this);
            }
        }

        public void Show_Cam1(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Cam1], this);
            }
        }

        public void Show_Cam2(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Cam2], this);
            }
        }

        public void Dig_Reserved_Out9(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out9], this);
            }
        }

        public void Cam0_IsSelected(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Cam0_IsSelected], this);
            }
        }

        public void Cam1_IsSelected(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Cam1_IsSelected], this);
            }
        }

        public void Cam2_IsSelected(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Cam2_IsSelected], this);
            }
        }

        public void Dig_Reserved_Out10(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out10], this);
            }
        }

        public void Mute_Toggle_FB(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Mute_Toggle_FB], this);
            }
        }

        public void Dig_Reserved_Out11(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out11], this);
            }
        }

        public void Dig_Reserved_Out12(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out12], this);
            }
        }

        public void Dig_Reserved_Out13(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out13], this);
            }
        }

        public void Dig_Reserved_Out14(Intercom_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out14], this);
            }
        }

        public event EventHandler<UIEventArgs> Vol_Level_Set;
        private void onVol_Level_Set(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Vol_Level_Set;
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

        public event EventHandler<UIEventArgs> An_Reserved_In2;
        private void onAn_Reserved_In2(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = An_Reserved_In2;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> An_Reserved_In3;
        private void onAn_Reserved_In3(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = An_Reserved_In3;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void NumberOf_Locations(Intercom_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Locations], this);
            }
        }

        public void Vol_Level_FB(Intercom_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Vol_Level_FB], this);
            }
        }

        public void An_Reserved_Out1(Intercom_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.An_Reserved_Out1], this);
            }
        }

        public void An_Reserved_Out2(Intercom_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.An_Reserved_Out2], this);
            }
        }

        public void An_Reserved_Out3(Intercom_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.An_Reserved_Out3], this);
            }
        }

        public event EventHandler<UIEventArgs> Ser_Reserved_In7;
        private void onSer_Reserved_In7(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Ser_Reserved_In7;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Ser_Reserved_In8;
        private void onSer_Reserved_In8(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Ser_Reserved_In8;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Ser_Reserved_In9;
        private void onSer_Reserved_In9(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Ser_Reserved_In9;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Video_URL(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Video_URL], this);
            }
        }

        public void SelectedLocation_Name(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.SelectedLocation_Name], this);
            }
        }

        public void Location0_Name(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Location0_Name], this);
            }
        }

        public void Location1_Name(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Location1_Name], this);
            }
        }

        public void Location2_Name(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Location2_Name], this);
            }
        }

        public void Location3_Name(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Location3_Name], this);
            }
        }

        public void Location4_Name(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Location4_Name], this);
            }
        }

        public void Location5_Name(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Location5_Name], this);
            }
        }

        public void Ser_Reserved_Out1(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out1], this);
            }
        }

        public void Ser_Reserved_Out2(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out2], this);
            }
        }

        public void Ser_Reserved_Out3(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out3], this);
            }
        }

        public void ExtraFunc0_Name(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.ExtraFunc0_Name], this);
            }
        }

        public void ExtraFunc1_Name(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.ExtraFunc1_Name], this);
            }
        }

        public void Ser_Reserved_Out4(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out4], this);
            }
        }

        public void Ser_Reserved_Out5(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out5], this);
            }
        }

        public void Cam0_Name(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Cam0_Name], this);
            }
        }

        public void Cam1_Name(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Cam1_Name], this);
            }
        }

        public void Cam2_Name(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Cam2_Name], this);
            }
        }

        public void Ser_Reserved_Out6(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out6], this);
            }
        }

        public void Ser_Reserved_Out7(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out7], this);
            }
        }

        public void Ser_Reserved_Out8(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out8], this);
            }
        }

        public void Ser_Reserved_Out9(Intercom_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out9], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Intercom_Widget", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            ((App_Contract.Intercom_Dialpad)Dialpad).Dispose();
            ((App_Contract.Intercom_ContactList)ContactList).Dispose();

            CamOff_Toggle = null;
            Widget_Select = null;
            Widget_Close = null;
            Location0_Select = null;
            Location1_Select = null;
            Location2_Select = null;
            Location3_Select = null;
            Location4_Select = null;
            Location5_Select = null;
            Dig_Reserved_In3 = null;
            Dig_Reserved_In4 = null;
            Dig_Reserved_In5 = null;
            Dig_Reserved_In6 = null;
            Answer_Select = null;
            HangUp_Select = null;
            DND_Select = null;
            DoorUnlock_Select = null;
            PTT_Select = null;
            ExtraFunc0_Select = null;
            ExtraFunc1_Select = null;
            Dig_Reserved_In7 = null;
            Dig_Reserved_In8 = null;
            Dig_Reserved_In9 = null;
            Cam0_Select = null;
            Cam1_Select = null;
            Cam2_Select = null;
            Dig_Reserved_In10 = null;
            Vol_Up = null;
            Vol_Down = null;
            Mute_Toggle = null;
            Dig_Reserved_In11 = null;
            Dig_Reserved_In12 = null;
            Dig_Reserved_In13 = null;
            Dig_Reserved_In14 = null;
            Vol_Level_Set = null;
            An_Reserved_In1 = null;
            An_Reserved_In2 = null;
            An_Reserved_In3 = null;
            Ser_Reserved_In7 = null;
            Ser_Reserved_In8 = null;
            Ser_Reserved_In9 = null;
        }

        #endregion

    }
}
