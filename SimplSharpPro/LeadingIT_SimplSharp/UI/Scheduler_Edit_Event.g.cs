using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    /// <summary>
    /// HIGH for TimeSpinner; LOW for SunEvent Spinner
    /// </summary>
    /// <summary>
    /// "Edit Event" or "New Event"
    /// </summary>
    public interface IScheduler_Edit_Event
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Sun_Select;
        event EventHandler<UIEventArgs> Mon_Select;
        event EventHandler<UIEventArgs> Tue_Select;
        event EventHandler<UIEventArgs> Wed_Select;
        event EventHandler<UIEventArgs> Thu_Select;
        event EventHandler<UIEventArgs> Fri_Select;
        event EventHandler<UIEventArgs> Sat_Select;
        event EventHandler<UIEventArgs> Dig_Reserved_In4;
        event EventHandler<UIEventArgs> Save_Event;
        event EventHandler<UIEventArgs> Exit_EditPage;
        event EventHandler<UIEventArgs> Delete_SelectedEvent;
        event EventHandler<UIEventArgs> Dig_Reserved_In5;
        event EventHandler<UIEventArgs> TimeOrEvent_Toggle;
        event EventHandler<UIEventArgs> Hour_NumberOf_SelectedItem;
        event EventHandler<UIEventArgs> Minutes_NumberOf_SelectedItem;
        event EventHandler<UIEventArgs> AM_PM_NumberOf_SelectedItem;
        event EventHandler<UIEventArgs> An_Reserved_In1;
        event EventHandler<UIEventArgs> An_Reserved_In2;
        event EventHandler<UIEventArgs> NewEvent_Name_Set;

        void Show_EditPage(Scheduler_Edit_EventBoolInputSigDelegate callback);
        void Show_ClimateScheduler(Scheduler_Edit_EventBoolInputSigDelegate callback);
        void Show_LightsScheduler(Scheduler_Edit_EventBoolInputSigDelegate callback);
        void Show_CurtainsScheduler(Scheduler_Edit_EventBoolInputSigDelegate callback);
        void Show_SensorsScheduler(Scheduler_Edit_EventBoolInputSigDelegate callback);
        void Show_TimeSpinner(Scheduler_Edit_EventBoolInputSigDelegate callback);
        void Show_SunEventSpinner(Scheduler_Edit_EventBoolInputSigDelegate callback);
        void TimeOrEvent_Toggle_FB(Scheduler_Edit_EventBoolInputSigDelegate callback);
        void Sun_IsSelected(Scheduler_Edit_EventBoolInputSigDelegate callback);
        void Mon_IsSelected(Scheduler_Edit_EventBoolInputSigDelegate callback);
        void Tue_IsSelected(Scheduler_Edit_EventBoolInputSigDelegate callback);
        void Wed_IsSelected(Scheduler_Edit_EventBoolInputSigDelegate callback);
        void Thu_IsSelected(Scheduler_Edit_EventBoolInputSigDelegate callback);
        void Fri_IsSelected(Scheduler_Edit_EventBoolInputSigDelegate callback);
        void Sat_IsSelected(Scheduler_Edit_EventBoolInputSigDelegate callback);
        void Show_FanScheduler(Scheduler_Edit_EventBoolInputSigDelegate callback);
        void Dig_Reserved_Out5(Scheduler_Edit_EventBoolInputSigDelegate callback);
        void Hour_NumberOfItem_FB(Scheduler_Edit_EventUShortInputSigDelegate callback);
        void Minutes_NumberOfItem_FB(Scheduler_Edit_EventUShortInputSigDelegate callback);
        void AM_PM_NumberOfItem_FB(Scheduler_Edit_EventUShortInputSigDelegate callback);
        void An_Reserved_Out1(Scheduler_Edit_EventUShortInputSigDelegate callback);
        void An_Reserved_Out2(Scheduler_Edit_EventUShortInputSigDelegate callback);
        void EditPage_Title(Scheduler_Edit_EventStringInputSigDelegate callback);
        void Current_EventName(Scheduler_Edit_EventStringInputSigDelegate callback);
        void Ser_Reserved_Out1(Scheduler_Edit_EventStringInputSigDelegate callback);
        void Ser_Reserved_Out2(Scheduler_Edit_EventStringInputSigDelegate callback);

        App_Contract.ISheduler_Climate_Spinner ClimateSpinner { get; }
        App_Contract.IScheduler_Lights_Spinner LightsSpinner { get; }
        App_Contract.IScheduler_Curtains_Spinner CurtainsSpinner { get; }
        App_Contract.IScheduler_SunEvent_Spinner SunEventSpinner { get; }
        App_Contract.IScheduler_Sensor[] Sensor { get; }
    }

    public delegate void Scheduler_Edit_EventBoolInputSigDelegate(BoolInputSig boolInputSig, IScheduler_Edit_Event scheduler_Edit_Event);
    public delegate void Scheduler_Edit_EventUShortInputSigDelegate(UShortInputSig uShortInputSig, IScheduler_Edit_Event scheduler_Edit_Event);
    public delegate void Scheduler_Edit_EventStringInputSigDelegate(StringInputSig stringInputSig, IScheduler_Edit_Event scheduler_Edit_Event);

    internal class Scheduler_Edit_Event : IScheduler_Edit_Event, IDisposable
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
                public const uint Sun_Select = 7;
                public const uint Mon_Select = 8;
                public const uint Tue_Select = 9;
                public const uint Wed_Select = 10;
                public const uint Thu_Select = 11;
                public const uint Fri_Select = 12;
                public const uint Sat_Select = 13;
                public const uint Dig_Reserved_In4 = 15;
                public const uint Save_Event = 16;
                public const uint Exit_EditPage = 17;
                public const uint Delete_SelectedEvent = 18;
                public const uint Dig_Reserved_In5 = 19;
                public const uint TimeOrEvent_Toggle = 20;

                public const uint Show_EditPage = 1;
                public const uint Show_ClimateScheduler = 2;
                public const uint Show_LightsScheduler = 3;
                public const uint Show_CurtainsScheduler = 4;
                public const uint Show_SensorsScheduler = 5;
                public const uint Show_TimeSpinner = 6;
                public const uint Show_SunEventSpinner = 7;
                public const uint TimeOrEvent_Toggle_FB = 8;
                public const uint Sun_IsSelected = 9;
                public const uint Mon_IsSelected = 10;
                public const uint Tue_IsSelected = 11;
                public const uint Wed_IsSelected = 12;
                public const uint Thu_IsSelected = 13;
                public const uint Fri_IsSelected = 14;
                public const uint Sat_IsSelected = 15;
                public const uint Show_FanScheduler = 16;
                public const uint Dig_Reserved_Out5 = 20;
            }
            internal static class Numerics
            {
                public const uint Hour_NumberOf_SelectedItem = 1;
                public const uint Minutes_NumberOf_SelectedItem = 2;
                public const uint AM_PM_NumberOf_SelectedItem = 3;
                public const uint An_Reserved_In1 = 4;
                public const uint An_Reserved_In2 = 5;

                public const uint Hour_NumberOfItem_FB = 1;
                public const uint Minutes_NumberOfItem_FB = 2;
                public const uint AM_PM_NumberOfItem_FB = 3;
                public const uint An_Reserved_Out1 = 4;
                public const uint An_Reserved_Out2 = 5;
            }
            internal static class Strings
            {
                public const uint NewEvent_Name_Set = 5;

                public const uint EditPage_Title = 1;
                public const uint Current_EventName = 2;
                public const uint Ser_Reserved_Out1 = 4;
                public const uint Ser_Reserved_Out2 = 5;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Scheduler_Edit_Event(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private static readonly IDictionary<uint, List<uint>> SensorSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 159, new List<uint> { 164, 165, 166, 167, 168, 169, 170, 171, 172, 173 } }};

        internal static void ClearDictionaries()
        {
            SensorSmartObjectIdMappings.Clear();
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Sun_Select, onSun_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Mon_Select, onMon_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Tue_Select, onTue_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Wed_Select, onWed_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Thu_Select, onThu_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Fri_Select, onFri_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Sat_Select, onSat_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In4, onDig_Reserved_In4);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Save_Event, onSave_Event);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Exit_EditPage, onExit_EditPage);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Delete_SelectedEvent, onDelete_SelectedEvent);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In5, onDig_Reserved_In5);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.TimeOrEvent_Toggle, onTimeOrEvent_Toggle);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.Hour_NumberOf_SelectedItem, onHour_NumberOf_SelectedItem);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.Minutes_NumberOf_SelectedItem, onMinutes_NumberOf_SelectedItem);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.AM_PM_NumberOf_SelectedItem, onAM_PM_NumberOf_SelectedItem);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.An_Reserved_In1, onAn_Reserved_In1);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.An_Reserved_In2, onAn_Reserved_In2);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.NewEvent_Name_Set, onNewEvent_Name_Set);

            ClimateSpinner = new App_Contract.Sheduler_Climate_Spinner(ComponentMediator, 160);

            LightsSpinner = new App_Contract.Scheduler_Lights_Spinner(ComponentMediator, 161);

            CurtainsSpinner = new App_Contract.Scheduler_Curtains_Spinner(ComponentMediator, 162);

            SunEventSpinner = new App_Contract.Scheduler_SunEvent_Spinner(ComponentMediator, 163);

            List<uint> sensorList = SensorSmartObjectIdMappings[controlJoinId];
            Sensor = new App_Contract.IScheduler_Sensor[sensorList.Count];
            for (int index = 0; index < sensorList.Count; index++)
            {
                Sensor[index] = new App_Contract.Scheduler_Sensor(ComponentMediator, sensorList[index]); 
            }

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            ((App_Contract.Sheduler_Climate_Spinner)ClimateSpinner).AddDevice(device);
            ((App_Contract.Scheduler_Lights_Spinner)LightsSpinner).AddDevice(device);
            ((App_Contract.Scheduler_Curtains_Spinner)CurtainsSpinner).AddDevice(device);
            ((App_Contract.Scheduler_SunEvent_Spinner)SunEventSpinner).AddDevice(device);
            for (int index = 0; index < Sensor.Length; index++)
            {
                ((App_Contract.Scheduler_Sensor)Sensor[index]).AddDevice(device);
            }
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            ((App_Contract.Sheduler_Climate_Spinner)ClimateSpinner).RemoveDevice(device);
            ((App_Contract.Scheduler_Lights_Spinner)LightsSpinner).RemoveDevice(device);
            ((App_Contract.Scheduler_Curtains_Spinner)CurtainsSpinner).RemoveDevice(device);
            ((App_Contract.Scheduler_SunEvent_Spinner)SunEventSpinner).RemoveDevice(device);
            for (int index = 0; index < Sensor.Length; index++)
            {
                ((App_Contract.Scheduler_Sensor)Sensor[index]).RemoveDevice(device);
            }
        }

        #endregion

        #region CH5 Contract

        public App_Contract.ISheduler_Climate_Spinner ClimateSpinner { get; private set; }

        public App_Contract.IScheduler_Lights_Spinner LightsSpinner { get; private set; }

        public App_Contract.IScheduler_Curtains_Spinner CurtainsSpinner { get; private set; }

        public App_Contract.IScheduler_SunEvent_Spinner SunEventSpinner { get; private set; }

        public App_Contract.IScheduler_Sensor[] Sensor { get; private set; }

        public event EventHandler<UIEventArgs> Sun_Select;
        private void onSun_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Sun_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Mon_Select;
        private void onMon_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Mon_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Tue_Select;
        private void onTue_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Tue_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Wed_Select;
        private void onWed_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Wed_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Thu_Select;
        private void onThu_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Thu_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Fri_Select;
        private void onFri_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Fri_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Sat_Select;
        private void onSat_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Sat_Select;
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

        public event EventHandler<UIEventArgs> Save_Event;
        private void onSave_Event(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Save_Event;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Exit_EditPage;
        private void onExit_EditPage(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Exit_EditPage;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Delete_SelectedEvent;
        private void onDelete_SelectedEvent(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Delete_SelectedEvent;
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

        public event EventHandler<UIEventArgs> TimeOrEvent_Toggle;
        private void onTimeOrEvent_Toggle(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = TimeOrEvent_Toggle;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Show_EditPage(Scheduler_Edit_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_EditPage], this);
            }
        }

        public void Show_ClimateScheduler(Scheduler_Edit_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_ClimateScheduler], this);
            }
        }

        public void Show_LightsScheduler(Scheduler_Edit_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_LightsScheduler], this);
            }
        }

        public void Show_CurtainsScheduler(Scheduler_Edit_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_CurtainsScheduler], this);
            }
        }

        public void Show_SensorsScheduler(Scheduler_Edit_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_SensorsScheduler], this);
            }
        }

        public void Show_TimeSpinner(Scheduler_Edit_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_TimeSpinner], this);
            }
        }

        public void Show_SunEventSpinner(Scheduler_Edit_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_SunEventSpinner], this);
            }
        }

        public void TimeOrEvent_Toggle_FB(Scheduler_Edit_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.TimeOrEvent_Toggle_FB], this);
            }
        }

        public void Sun_IsSelected(Scheduler_Edit_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Sun_IsSelected], this);
            }
        }

        public void Mon_IsSelected(Scheduler_Edit_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Mon_IsSelected], this);
            }
        }

        public void Tue_IsSelected(Scheduler_Edit_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Tue_IsSelected], this);
            }
        }

        public void Wed_IsSelected(Scheduler_Edit_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Wed_IsSelected], this);
            }
        }

        public void Thu_IsSelected(Scheduler_Edit_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Thu_IsSelected], this);
            }
        }

        public void Fri_IsSelected(Scheduler_Edit_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Fri_IsSelected], this);
            }
        }

        public void Sat_IsSelected(Scheduler_Edit_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Sat_IsSelected], this);
            }
        }

        public void Show_FanScheduler(Scheduler_Edit_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_FanScheduler], this);
            }
        }

        public void Dig_Reserved_Out5(Scheduler_Edit_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out5], this);
            }
        }

        public event EventHandler<UIEventArgs> Hour_NumberOf_SelectedItem;
        private void onHour_NumberOf_SelectedItem(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Hour_NumberOf_SelectedItem;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Minutes_NumberOf_SelectedItem;
        private void onMinutes_NumberOf_SelectedItem(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Minutes_NumberOf_SelectedItem;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> AM_PM_NumberOf_SelectedItem;
        private void onAM_PM_NumberOf_SelectedItem(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AM_PM_NumberOf_SelectedItem;
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


        public void Hour_NumberOfItem_FB(Scheduler_Edit_EventUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Hour_NumberOfItem_FB], this);
            }
        }

        public void Minutes_NumberOfItem_FB(Scheduler_Edit_EventUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Minutes_NumberOfItem_FB], this);
            }
        }

        public void AM_PM_NumberOfItem_FB(Scheduler_Edit_EventUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.AM_PM_NumberOfItem_FB], this);
            }
        }

        public void An_Reserved_Out1(Scheduler_Edit_EventUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.An_Reserved_Out1], this);
            }
        }

        public void An_Reserved_Out2(Scheduler_Edit_EventUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.An_Reserved_Out2], this);
            }
        }

        public event EventHandler<UIEventArgs> NewEvent_Name_Set;
        private void onNewEvent_Name_Set(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = NewEvent_Name_Set;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void EditPage_Title(Scheduler_Edit_EventStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.EditPage_Title], this);
            }
        }

        public void Current_EventName(Scheduler_Edit_EventStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Current_EventName], this);
            }
        }

        public void Ser_Reserved_Out1(Scheduler_Edit_EventStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out1], this);
            }
        }

        public void Ser_Reserved_Out2(Scheduler_Edit_EventStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out2], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Scheduler_Edit_Event", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            ((App_Contract.Sheduler_Climate_Spinner)ClimateSpinner).Dispose();
            ((App_Contract.Scheduler_Lights_Spinner)LightsSpinner).Dispose();
            ((App_Contract.Scheduler_Curtains_Spinner)CurtainsSpinner).Dispose();
            ((App_Contract.Scheduler_SunEvent_Spinner)SunEventSpinner).Dispose();
            for (int index = 0; index < Sensor.Length; index++)
            {
                ((App_Contract.Scheduler_Sensor)Sensor[index]).Dispose();
            }

            Sun_Select = null;
            Mon_Select = null;
            Tue_Select = null;
            Wed_Select = null;
            Thu_Select = null;
            Fri_Select = null;
            Sat_Select = null;
            Dig_Reserved_In4 = null;
            Save_Event = null;
            Exit_EditPage = null;
            Delete_SelectedEvent = null;
            Dig_Reserved_In5 = null;
            TimeOrEvent_Toggle = null;
            Hour_NumberOf_SelectedItem = null;
            Minutes_NumberOf_SelectedItem = null;
            AM_PM_NumberOf_SelectedItem = null;
            An_Reserved_In1 = null;
            An_Reserved_In2 = null;
            NewEvent_Name_Set = null;
        }

        #endregion

    }
}
