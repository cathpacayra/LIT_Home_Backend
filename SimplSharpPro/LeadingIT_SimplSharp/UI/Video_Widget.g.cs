using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IVideo_Widget
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> PowerOff_PopUp_Confirm;
        event EventHandler<UIEventArgs> PowerOff_PopUp_Cancel;
        event EventHandler<UIEventArgs> Power_Toggle;
        event EventHandler<UIEventArgs> Vol_Up;
        event EventHandler<UIEventArgs> Vol_Down;
        event EventHandler<UIEventArgs> Mute_Toggle;
        event EventHandler<UIEventArgs> CustomCotrolButton_0_Select;
        event EventHandler<UIEventArgs> CustomCotrolButton_1_Select;
        event EventHandler<UIEventArgs> CustomCotrolButton_2_Select;
        event EventHandler<UIEventArgs> CustomCotrolButton_3_Select;
        event EventHandler<UIEventArgs> CustomCotrolButton_4_Select;
        event EventHandler<UIEventArgs> AspectRatio_0_Select;
        event EventHandler<UIEventArgs> AspectRatio_1_Select;
        event EventHandler<UIEventArgs> AspectRatio_2_Select;
        event EventHandler<UIEventArgs> AspectRatio_3_Select;
        event EventHandler<UIEventArgs> AspectRatio_4_Select;
        event EventHandler<UIEventArgs> CustomCotrolButton_5_Select;
        event EventHandler<UIEventArgs> CustomCotrolButton_6_Select;
        event EventHandler<UIEventArgs> CustomCotrolButton_7_Select;
        event EventHandler<UIEventArgs> CustomCotrolButton_8_Select;
        event EventHandler<UIEventArgs> CustomCotrolButton_9_Select;
        event EventHandler<UIEventArgs> Vol_Level_Set;
        event EventHandler<UIEventArgs> An_Reserved_In1;
        event EventHandler<UIEventArgs> An_Reserved_In2;

        void Show_WhenPowerOff(Video_WidgetBoolInputSigDelegate callback);
        void Show_Sources(Video_WidgetBoolInputSigDelegate callback);
        void Show_WarmUp(Video_WidgetBoolInputSigDelegate callback);
        void Show_ShutDown(Video_WidgetBoolInputSigDelegate callback);
        void Show_CustomControls_Button(Video_WidgetBoolInputSigDelegate callback);
        void Show_Projector_Button(Video_WidgetBoolInputSigDelegate callback);
        void Show_Remote_Button(Video_WidgetBoolInputSigDelegate callback);
        void Show_PowerOff_PopUp(Video_WidgetBoolInputSigDelegate callback);
        void PowerToggle_FB(Video_WidgetBoolInputSigDelegate callback);
        void MuteToggle_FB(Video_WidgetBoolInputSigDelegate callback);
        void Show_CustomControls_List(Video_WidgetBoolInputSigDelegate callback);
        void Show_AspectRatio_List(Video_WidgetBoolInputSigDelegate callback);
        void CustomCotrolButton_0_IsSelected(Video_WidgetBoolInputSigDelegate callback);
        void CustomCotrolButton_1_IsSelected(Video_WidgetBoolInputSigDelegate callback);
        void CustomCotrolButton_2_IsSelected(Video_WidgetBoolInputSigDelegate callback);
        void CustomCotrolButton_3_IsSelected(Video_WidgetBoolInputSigDelegate callback);
        void CustomCotrolButton_4_IsSelected(Video_WidgetBoolInputSigDelegate callback);
        void AspectRatio_0_IsSelected(Video_WidgetBoolInputSigDelegate callback);
        void AspectRatio_1_IsSelected(Video_WidgetBoolInputSigDelegate callback);
        void AspectRatio_2_IsSelected(Video_WidgetBoolInputSigDelegate callback);
        void AspectRatio_3_IsSelected(Video_WidgetBoolInputSigDelegate callback);
        void AspectRatio_4_IsSelected(Video_WidgetBoolInputSigDelegate callback);
        void CustomCotrolButton_5_IsSelected(Video_WidgetBoolInputSigDelegate callback);
        void CustomCotrolButton_6_IsSelected(Video_WidgetBoolInputSigDelegate callback);
        void CustomCotrolButton_7_IsSelected(Video_WidgetBoolInputSigDelegate callback);
        void CustomCotrolButton_8_IsSelected(Video_WidgetBoolInputSigDelegate callback);
        void CustomCotrolButton_9_IsSelected(Video_WidgetBoolInputSigDelegate callback);
        void NumberOf_Sources(Video_WidgetUShortInputSigDelegate callback);
        void NumberOf_CustomControls(Video_WidgetUShortInputSigDelegate callback);
        void NumberOf_AspectRatios(Video_WidgetUShortInputSigDelegate callback);
        void Vol_Level_FB(Video_WidgetUShortInputSigDelegate callback);
        void An_Reserved_Out1(Video_WidgetUShortInputSigDelegate callback);
        void An_Reserved_Out2(Video_WidgetUShortInputSigDelegate callback);
        void WarmUp_Period(Video_WidgetStringInputSigDelegate callback);
        void Volume_Value(Video_WidgetStringInputSigDelegate callback);
        void Playing_Status(Video_WidgetStringInputSigDelegate callback);
        void SelectedSource_Name(Video_WidgetStringInputSigDelegate callback);
        void CustomControlButton_0_Name(Video_WidgetStringInputSigDelegate callback);
        void CustomControlButton_1_Name(Video_WidgetStringInputSigDelegate callback);
        void CustomControlButton_2_Name(Video_WidgetStringInputSigDelegate callback);
        void CustomControlButton_3_Name(Video_WidgetStringInputSigDelegate callback);
        void CustomControlButton_4_Name(Video_WidgetStringInputSigDelegate callback);
        void CustomControlButton_5_Name(Video_WidgetStringInputSigDelegate callback);
        void CustomControlButton_6_Name(Video_WidgetStringInputSigDelegate callback);
        void CustomControlButton_7_Name(Video_WidgetStringInputSigDelegate callback);
        void CustomControlButton_8_Name(Video_WidgetStringInputSigDelegate callback);
        void CustomControlButton_9_Name(Video_WidgetStringInputSigDelegate callback);
        void AspectRatio_0_Name(Video_WidgetStringInputSigDelegate callback);
        void AspectRatio_1_Name(Video_WidgetStringInputSigDelegate callback);
        void AspectRatio_2_Name(Video_WidgetStringInputSigDelegate callback);
        void AspectRatio_3_Name(Video_WidgetStringInputSigDelegate callback);
        void AspectRatio_4_Name(Video_WidgetStringInputSigDelegate callback);

        App_Contract.IVideo_Source[] Source { get; }
        App_Contract.IVideo_Remote Remote { get; }
    }

    public delegate void Video_WidgetBoolInputSigDelegate(BoolInputSig boolInputSig, IVideo_Widget video_Widget);
    public delegate void Video_WidgetUShortInputSigDelegate(UShortInputSig uShortInputSig, IVideo_Widget video_Widget);
    public delegate void Video_WidgetStringInputSigDelegate(StringInputSig stringInputSig, IVideo_Widget video_Widget);

    internal class Video_Widget : IVideo_Widget, IDisposable
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
                public const uint PowerOff_PopUp_Confirm = 10;
                public const uint PowerOff_PopUp_Cancel = 11;
                public const uint Power_Toggle = 12;
                public const uint Vol_Up = 13;
                public const uint Vol_Down = 14;
                public const uint Mute_Toggle = 15;
                public const uint CustomCotrolButton_0_Select = 19;
                public const uint CustomCotrolButton_1_Select = 20;
                public const uint CustomCotrolButton_2_Select = 21;
                public const uint CustomCotrolButton_3_Select = 22;
                public const uint CustomCotrolButton_4_Select = 23;
                public const uint AspectRatio_0_Select = 24;
                public const uint AspectRatio_1_Select = 25;
                public const uint AspectRatio_2_Select = 26;
                public const uint AspectRatio_3_Select = 27;
                public const uint AspectRatio_4_Select = 28;
                public const uint CustomCotrolButton_5_Select = 30;
                public const uint CustomCotrolButton_6_Select = 31;
                public const uint CustomCotrolButton_7_Select = 32;
                public const uint CustomCotrolButton_8_Select = 33;
                public const uint CustomCotrolButton_9_Select = 34;

                public const uint Show_WhenPowerOff = 1;
                public const uint Show_Sources = 2;
                public const uint Show_WarmUp = 3;
                public const uint Show_ShutDown = 4;
                public const uint Show_CustomControls_Button = 5;
                public const uint Show_Projector_Button = 6;
                public const uint Show_Remote_Button = 7;
                public const uint Show_PowerOff_PopUp = 9;
                public const uint PowerToggle_FB = 12;
                public const uint MuteToggle_FB = 15;
                public const uint Show_CustomControls_List = 17;
                public const uint Show_AspectRatio_List = 18;
                public const uint CustomCotrolButton_0_IsSelected = 19;
                public const uint CustomCotrolButton_1_IsSelected = 20;
                public const uint CustomCotrolButton_2_IsSelected = 21;
                public const uint CustomCotrolButton_3_IsSelected = 22;
                public const uint CustomCotrolButton_4_IsSelected = 23;
                public const uint AspectRatio_0_IsSelected = 24;
                public const uint AspectRatio_1_IsSelected = 25;
                public const uint AspectRatio_2_IsSelected = 26;
                public const uint AspectRatio_3_IsSelected = 27;
                public const uint AspectRatio_4_IsSelected = 28;
                public const uint CustomCotrolButton_5_IsSelected = 30;
                public const uint CustomCotrolButton_6_IsSelected = 31;
                public const uint CustomCotrolButton_7_IsSelected = 32;
                public const uint CustomCotrolButton_8_IsSelected = 33;
                public const uint CustomCotrolButton_9_IsSelected = 34;
            }
            internal static class Numerics
            {
                public const uint Vol_Level_Set = 5;
                public const uint An_Reserved_In1 = 7;
                public const uint An_Reserved_In2 = 8;

                public const uint NumberOf_Sources = 1;
                public const uint NumberOf_CustomControls = 2;
                public const uint NumberOf_AspectRatios = 3;
                public const uint Vol_Level_FB = 5;
                public const uint An_Reserved_Out1 = 7;
                public const uint An_Reserved_Out2 = 8;
            }
            internal static class Strings
            {

                public const uint WarmUp_Period = 1;
                public const uint Volume_Value = 3;
                public const uint Playing_Status = 4;
                public const uint SelectedSource_Name = 5;
                public const uint CustomControlButton_0_Name = 7;
                public const uint CustomControlButton_1_Name = 8;
                public const uint CustomControlButton_2_Name = 9;
                public const uint CustomControlButton_3_Name = 10;
                public const uint CustomControlButton_4_Name = 11;
                public const uint CustomControlButton_5_Name = 12;
                public const uint CustomControlButton_6_Name = 13;
                public const uint CustomControlButton_7_Name = 14;
                public const uint CustomControlButton_8_Name = 15;
                public const uint CustomControlButton_9_Name = 16;
                public const uint AspectRatio_0_Name = 17;
                public const uint AspectRatio_1_Name = 18;
                public const uint AspectRatio_2_Name = 19;
                public const uint AspectRatio_3_Name = 20;
                public const uint AspectRatio_4_Name = 21;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Video_Widget(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private static readonly IDictionary<uint, List<uint>> SourceSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 279, new List<uint> { 280, 281, 282, 283, 284, 285, 286, 287, 288, 289, 290, 291 } }};

        internal static void ClearDictionaries()
        {
            SourceSmartObjectIdMappings.Clear();
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.PowerOff_PopUp_Confirm, onPowerOff_PopUp_Confirm);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.PowerOff_PopUp_Cancel, onPowerOff_PopUp_Cancel);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Power_Toggle, onPower_Toggle);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Vol_Up, onVol_Up);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Vol_Down, onVol_Down);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Mute_Toggle, onMute_Toggle);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CustomCotrolButton_0_Select, onCustomCotrolButton_0_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CustomCotrolButton_1_Select, onCustomCotrolButton_1_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CustomCotrolButton_2_Select, onCustomCotrolButton_2_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CustomCotrolButton_3_Select, onCustomCotrolButton_3_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CustomCotrolButton_4_Select, onCustomCotrolButton_4_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.AspectRatio_0_Select, onAspectRatio_0_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.AspectRatio_1_Select, onAspectRatio_1_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.AspectRatio_2_Select, onAspectRatio_2_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.AspectRatio_3_Select, onAspectRatio_3_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.AspectRatio_4_Select, onAspectRatio_4_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CustomCotrolButton_5_Select, onCustomCotrolButton_5_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CustomCotrolButton_6_Select, onCustomCotrolButton_6_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CustomCotrolButton_7_Select, onCustomCotrolButton_7_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CustomCotrolButton_8_Select, onCustomCotrolButton_8_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CustomCotrolButton_9_Select, onCustomCotrolButton_9_Select);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.Vol_Level_Set, onVol_Level_Set);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.An_Reserved_In1, onAn_Reserved_In1);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.An_Reserved_In2, onAn_Reserved_In2);

            List<uint> sourceList = SourceSmartObjectIdMappings[controlJoinId];
            Source = new App_Contract.IVideo_Source[sourceList.Count];
            for (int index = 0; index < sourceList.Count; index++)
            {
                Source[index] = new App_Contract.Video_Source(ComponentMediator, sourceList[index]); 
            }

            Remote = new App_Contract.Video_Remote(ComponentMediator, 292);

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Source.Length; index++)
            {
                ((App_Contract.Video_Source)Source[index]).AddDevice(device);
            }
            ((App_Contract.Video_Remote)Remote).AddDevice(device);
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Source.Length; index++)
            {
                ((App_Contract.Video_Source)Source[index]).RemoveDevice(device);
            }
            ((App_Contract.Video_Remote)Remote).RemoveDevice(device);
        }

        #endregion

        #region CH5 Contract

        public App_Contract.IVideo_Source[] Source { get; private set; }

        public App_Contract.IVideo_Remote Remote { get; private set; }

        public event EventHandler<UIEventArgs> PowerOff_PopUp_Confirm;
        private void onPowerOff_PopUp_Confirm(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = PowerOff_PopUp_Confirm;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> PowerOff_PopUp_Cancel;
        private void onPowerOff_PopUp_Cancel(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = PowerOff_PopUp_Cancel;
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

        public event EventHandler<UIEventArgs> CustomCotrolButton_0_Select;
        private void onCustomCotrolButton_0_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CustomCotrolButton_0_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CustomCotrolButton_1_Select;
        private void onCustomCotrolButton_1_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CustomCotrolButton_1_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CustomCotrolButton_2_Select;
        private void onCustomCotrolButton_2_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CustomCotrolButton_2_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CustomCotrolButton_3_Select;
        private void onCustomCotrolButton_3_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CustomCotrolButton_3_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CustomCotrolButton_4_Select;
        private void onCustomCotrolButton_4_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CustomCotrolButton_4_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> AspectRatio_0_Select;
        private void onAspectRatio_0_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AspectRatio_0_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> AspectRatio_1_Select;
        private void onAspectRatio_1_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AspectRatio_1_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> AspectRatio_2_Select;
        private void onAspectRatio_2_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AspectRatio_2_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> AspectRatio_3_Select;
        private void onAspectRatio_3_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AspectRatio_3_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> AspectRatio_4_Select;
        private void onAspectRatio_4_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AspectRatio_4_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CustomCotrolButton_5_Select;
        private void onCustomCotrolButton_5_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CustomCotrolButton_5_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CustomCotrolButton_6_Select;
        private void onCustomCotrolButton_6_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CustomCotrolButton_6_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CustomCotrolButton_7_Select;
        private void onCustomCotrolButton_7_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CustomCotrolButton_7_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CustomCotrolButton_8_Select;
        private void onCustomCotrolButton_8_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CustomCotrolButton_8_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CustomCotrolButton_9_Select;
        private void onCustomCotrolButton_9_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CustomCotrolButton_9_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Show_WhenPowerOff(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_WhenPowerOff], this);
            }
        }

        public void Show_Sources(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Sources], this);
            }
        }

        public void Show_WarmUp(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_WarmUp], this);
            }
        }

        public void Show_ShutDown(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_ShutDown], this);
            }
        }

        public void Show_CustomControls_Button(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_CustomControls_Button], this);
            }
        }

        public void Show_Projector_Button(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Projector_Button], this);
            }
        }

        public void Show_Remote_Button(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Remote_Button], this);
            }
        }

        public void Show_PowerOff_PopUp(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_PowerOff_PopUp], this);
            }
        }

        public void PowerToggle_FB(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.PowerToggle_FB], this);
            }
        }

        public void MuteToggle_FB(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.MuteToggle_FB], this);
            }
        }

        public void Show_CustomControls_List(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_CustomControls_List], this);
            }
        }

        public void Show_AspectRatio_List(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_AspectRatio_List], this);
            }
        }

        public void CustomCotrolButton_0_IsSelected(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.CustomCotrolButton_0_IsSelected], this);
            }
        }

        public void CustomCotrolButton_1_IsSelected(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.CustomCotrolButton_1_IsSelected], this);
            }
        }

        public void CustomCotrolButton_2_IsSelected(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.CustomCotrolButton_2_IsSelected], this);
            }
        }

        public void CustomCotrolButton_3_IsSelected(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.CustomCotrolButton_3_IsSelected], this);
            }
        }

        public void CustomCotrolButton_4_IsSelected(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.CustomCotrolButton_4_IsSelected], this);
            }
        }

        public void AspectRatio_0_IsSelected(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.AspectRatio_0_IsSelected], this);
            }
        }

        public void AspectRatio_1_IsSelected(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.AspectRatio_1_IsSelected], this);
            }
        }

        public void AspectRatio_2_IsSelected(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.AspectRatio_2_IsSelected], this);
            }
        }

        public void AspectRatio_3_IsSelected(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.AspectRatio_3_IsSelected], this);
            }
        }

        public void AspectRatio_4_IsSelected(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.AspectRatio_4_IsSelected], this);
            }
        }

        public void CustomCotrolButton_5_IsSelected(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.CustomCotrolButton_5_IsSelected], this);
            }
        }

        public void CustomCotrolButton_6_IsSelected(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.CustomCotrolButton_6_IsSelected], this);
            }
        }

        public void CustomCotrolButton_7_IsSelected(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.CustomCotrolButton_7_IsSelected], this);
            }
        }

        public void CustomCotrolButton_8_IsSelected(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.CustomCotrolButton_8_IsSelected], this);
            }
        }

        public void CustomCotrolButton_9_IsSelected(Video_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.CustomCotrolButton_9_IsSelected], this);
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


        public void NumberOf_Sources(Video_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Sources], this);
            }
        }

        public void NumberOf_CustomControls(Video_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_CustomControls], this);
            }
        }

        public void NumberOf_AspectRatios(Video_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_AspectRatios], this);
            }
        }

        public void Vol_Level_FB(Video_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Vol_Level_FB], this);
            }
        }

        public void An_Reserved_Out1(Video_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.An_Reserved_Out1], this);
            }
        }

        public void An_Reserved_Out2(Video_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.An_Reserved_Out2], this);
            }
        }


        public void WarmUp_Period(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.WarmUp_Period], this);
            }
        }

        public void Volume_Value(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Volume_Value], this);
            }
        }

        public void Playing_Status(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Playing_Status], this);
            }
        }

        public void SelectedSource_Name(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.SelectedSource_Name], this);
            }
        }

        public void CustomControlButton_0_Name(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.CustomControlButton_0_Name], this);
            }
        }

        public void CustomControlButton_1_Name(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.CustomControlButton_1_Name], this);
            }
        }

        public void CustomControlButton_2_Name(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.CustomControlButton_2_Name], this);
            }
        }

        public void CustomControlButton_3_Name(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.CustomControlButton_3_Name], this);
            }
        }

        public void CustomControlButton_4_Name(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.CustomControlButton_4_Name], this);
            }
        }

        public void CustomControlButton_5_Name(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.CustomControlButton_5_Name], this);
            }
        }

        public void CustomControlButton_6_Name(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.CustomControlButton_6_Name], this);
            }
        }

        public void CustomControlButton_7_Name(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.CustomControlButton_7_Name], this);
            }
        }

        public void CustomControlButton_8_Name(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.CustomControlButton_8_Name], this);
            }
        }

        public void CustomControlButton_9_Name(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.CustomControlButton_9_Name], this);
            }
        }

        public void AspectRatio_0_Name(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.AspectRatio_0_Name], this);
            }
        }

        public void AspectRatio_1_Name(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.AspectRatio_1_Name], this);
            }
        }

        public void AspectRatio_2_Name(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.AspectRatio_2_Name], this);
            }
        }

        public void AspectRatio_3_Name(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.AspectRatio_3_Name], this);
            }
        }

        public void AspectRatio_4_Name(Video_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.AspectRatio_4_Name], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Video_Widget", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            for (int index = 0; index < Source.Length; index++)
            {
                ((App_Contract.Video_Source)Source[index]).Dispose();
            }
            ((App_Contract.Video_Remote)Remote).Dispose();

            PowerOff_PopUp_Confirm = null;
            PowerOff_PopUp_Cancel = null;
            Power_Toggle = null;
            Vol_Up = null;
            Vol_Down = null;
            Mute_Toggle = null;
            CustomCotrolButton_0_Select = null;
            CustomCotrolButton_1_Select = null;
            CustomCotrolButton_2_Select = null;
            CustomCotrolButton_3_Select = null;
            CustomCotrolButton_4_Select = null;
            AspectRatio_0_Select = null;
            AspectRatio_1_Select = null;
            AspectRatio_2_Select = null;
            AspectRatio_3_Select = null;
            AspectRatio_4_Select = null;
            CustomCotrolButton_5_Select = null;
            CustomCotrolButton_6_Select = null;
            CustomCotrolButton_7_Select = null;
            CustomCotrolButton_8_Select = null;
            CustomCotrolButton_9_Select = null;
            Vol_Level_Set = null;
            An_Reserved_In1 = null;
            An_Reserved_In2 = null;
        }

        #endregion

    }
}
