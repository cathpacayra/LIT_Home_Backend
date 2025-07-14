using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IQuick_Actions_Edit
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Save_Changes;
        event EventHandler<UIEventArgs> Exit_EditPage;
        event EventHandler<UIEventArgs> Dig_Reserved_In3;
        event EventHandler<UIEventArgs> Dig_Reserved_In4;
        event EventHandler<UIEventArgs> General_0_ON_Select;
        event EventHandler<UIEventArgs> General_0_OFF_Select;
        event EventHandler<UIEventArgs> General_1_ON_Select;
        event EventHandler<UIEventArgs> General_1_OFF_Select;
        event EventHandler<UIEventArgs> General_2_ON_Select;
        event EventHandler<UIEventArgs> General_2_OFF_Select;
        event EventHandler<UIEventArgs> General_3_ON_Select;
        event EventHandler<UIEventArgs> General_3_OFF_Select;
        event EventHandler<UIEventArgs> General_4_ON_Select;
        event EventHandler<UIEventArgs> General_4_OFF_Select;
        event EventHandler<UIEventArgs> An_Reserved_In1;
        event EventHandler<UIEventArgs> New_Name_Set;
        event EventHandler<UIEventArgs> Ser_Reserved_In1;

        void Show_EditPage(Quick_Actions_EditBoolInputSigDelegate callback);
        void Show_ClimateSpinner(Quick_Actions_EditBoolInputSigDelegate callback);
        void Show_LightsSpinner(Quick_Actions_EditBoolInputSigDelegate callback);
        void Show_CurtainsSpinner(Quick_Actions_EditBoolInputSigDelegate callback);
        void Show_GlobalSpinner(Quick_Actions_EditBoolInputSigDelegate callback);
        void Show_FanSpinner(Quick_Actions_EditBoolInputSigDelegate callback);
        void Show_RoomsSpinner(Quick_Actions_EditBoolInputSigDelegate callback);
        void Show_Sensors(Quick_Actions_EditBoolInputSigDelegate callback);
        void Show_General(Quick_Actions_EditBoolInputSigDelegate callback);
        void General_0_ON_FB(Quick_Actions_EditBoolInputSigDelegate callback);
        void General_0_OFF_FB(Quick_Actions_EditBoolInputSigDelegate callback);
        void General_1_ON_FB(Quick_Actions_EditBoolInputSigDelegate callback);
        void General_1_OFF_FB(Quick_Actions_EditBoolInputSigDelegate callback);
        void General_2_ON_FB(Quick_Actions_EditBoolInputSigDelegate callback);
        void General_2_OFF_FB(Quick_Actions_EditBoolInputSigDelegate callback);
        void General_3_ON_FB(Quick_Actions_EditBoolInputSigDelegate callback);
        void General_3_OFF_FB(Quick_Actions_EditBoolInputSigDelegate callback);
        void General_4_ON_FB(Quick_Actions_EditBoolInputSigDelegate callback);
        void General_4_OFF_FB(Quick_Actions_EditBoolInputSigDelegate callback);
        void NumberOf_GeneralWidgets(Quick_Actions_EditUShortInputSigDelegate callback);
        void Current_Name(Quick_Actions_EditStringInputSigDelegate callback);
        void Ser_Reserved_Out1(Quick_Actions_EditStringInputSigDelegate callback);

        App_Contract.IQuickActions_Rooms_Spinner RoomsSpinner { get; }
        App_Contract.IScheduler_Lights_Spinner LightsSpinner { get; }
        App_Contract.ISheduler_Climate_Spinner ClimateSpinner { get; }
        App_Contract.IScheduler_Curtains_Spinner CurtainsSpinner { get; }
        App_Contract.IQuickActions_Global_Spinner GlobalSpinner { get; }
    }

    public delegate void Quick_Actions_EditBoolInputSigDelegate(BoolInputSig boolInputSig, IQuick_Actions_Edit quick_Actions_Edit);
    public delegate void Quick_Actions_EditUShortInputSigDelegate(UShortInputSig uShortInputSig, IQuick_Actions_Edit quick_Actions_Edit);
    public delegate void Quick_Actions_EditStringInputSigDelegate(StringInputSig stringInputSig, IQuick_Actions_Edit quick_Actions_Edit);

    internal class Quick_Actions_Edit : IQuick_Actions_Edit, IDisposable
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
                public const uint Save_Changes = 8;
                public const uint Exit_EditPage = 9;
                public const uint Dig_Reserved_In3 = 10;
                public const uint Dig_Reserved_In4 = 11;
                public const uint General_0_ON_Select = 12;
                public const uint General_0_OFF_Select = 13;
                public const uint General_1_ON_Select = 14;
                public const uint General_1_OFF_Select = 15;
                public const uint General_2_ON_Select = 16;
                public const uint General_2_OFF_Select = 17;
                public const uint General_3_ON_Select = 18;
                public const uint General_3_OFF_Select = 19;
                public const uint General_4_ON_Select = 20;
                public const uint General_4_OFF_Select = 21;

                public const uint Show_EditPage = 1;
                public const uint Show_ClimateSpinner = 2;
                public const uint Show_LightsSpinner = 3;
                public const uint Show_CurtainsSpinner = 4;
                public const uint Show_GlobalSpinner = 5;
                public const uint Show_FanSpinner = 6;
                public const uint Show_RoomsSpinner = 7;
                public const uint Show_Sensors = 10;
                public const uint Show_General = 11;
                public const uint General_0_ON_FB = 12;
                public const uint General_0_OFF_FB = 13;
                public const uint General_1_ON_FB = 14;
                public const uint General_1_OFF_FB = 15;
                public const uint General_2_ON_FB = 16;
                public const uint General_2_OFF_FB = 17;
                public const uint General_3_ON_FB = 18;
                public const uint General_3_OFF_FB = 19;
                public const uint General_4_ON_FB = 20;
                public const uint General_4_OFF_FB = 21;
            }
            internal static class Numerics
            {
                public const uint An_Reserved_In1 = 1;

                public const uint NumberOf_GeneralWidgets = 1;
            }
            internal static class Strings
            {
                public const uint New_Name_Set = 1;
                public const uint Ser_Reserved_In1 = 2;

                public const uint Current_Name = 1;
                public const uint Ser_Reserved_Out1 = 2;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Quick_Actions_Edit(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Save_Changes, onSave_Changes);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Exit_EditPage, onExit_EditPage);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In3, onDig_Reserved_In3);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In4, onDig_Reserved_In4);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.General_0_ON_Select, onGeneral_0_ON_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.General_0_OFF_Select, onGeneral_0_OFF_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.General_1_ON_Select, onGeneral_1_ON_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.General_1_OFF_Select, onGeneral_1_OFF_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.General_2_ON_Select, onGeneral_2_ON_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.General_2_OFF_Select, onGeneral_2_OFF_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.General_3_ON_Select, onGeneral_3_ON_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.General_3_OFF_Select, onGeneral_3_OFF_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.General_4_ON_Select, onGeneral_4_ON_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.General_4_OFF_Select, onGeneral_4_OFF_Select);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.An_Reserved_In1, onAn_Reserved_In1);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.New_Name_Set, onNew_Name_Set);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.Ser_Reserved_In1, onSer_Reserved_In1);

            RoomsSpinner = new App_Contract.QuickActions_Rooms_Spinner(ComponentMediator, 274);

            LightsSpinner = new App_Contract.Scheduler_Lights_Spinner(ComponentMediator, 275);

            ClimateSpinner = new App_Contract.Sheduler_Climate_Spinner(ComponentMediator, 276);

            CurtainsSpinner = new App_Contract.Scheduler_Curtains_Spinner(ComponentMediator, 277);

            GlobalSpinner = new App_Contract.QuickActions_Global_Spinner(ComponentMediator, 278);

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            ((App_Contract.QuickActions_Rooms_Spinner)RoomsSpinner).AddDevice(device);
            ((App_Contract.Scheduler_Lights_Spinner)LightsSpinner).AddDevice(device);
            ((App_Contract.Sheduler_Climate_Spinner)ClimateSpinner).AddDevice(device);
            ((App_Contract.Scheduler_Curtains_Spinner)CurtainsSpinner).AddDevice(device);
            ((App_Contract.QuickActions_Global_Spinner)GlobalSpinner).AddDevice(device);
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            ((App_Contract.QuickActions_Rooms_Spinner)RoomsSpinner).RemoveDevice(device);
            ((App_Contract.Scheduler_Lights_Spinner)LightsSpinner).RemoveDevice(device);
            ((App_Contract.Sheduler_Climate_Spinner)ClimateSpinner).RemoveDevice(device);
            ((App_Contract.Scheduler_Curtains_Spinner)CurtainsSpinner).RemoveDevice(device);
            ((App_Contract.QuickActions_Global_Spinner)GlobalSpinner).RemoveDevice(device);
        }

        #endregion

        #region CH5 Contract

        public App_Contract.IQuickActions_Rooms_Spinner RoomsSpinner { get; private set; }

        public App_Contract.IScheduler_Lights_Spinner LightsSpinner { get; private set; }

        public App_Contract.ISheduler_Climate_Spinner ClimateSpinner { get; private set; }

        public App_Contract.IScheduler_Curtains_Spinner CurtainsSpinner { get; private set; }

        public App_Contract.IQuickActions_Global_Spinner GlobalSpinner { get; private set; }

        public event EventHandler<UIEventArgs> Save_Changes;
        private void onSave_Changes(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Save_Changes;
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

        public event EventHandler<UIEventArgs> General_0_ON_Select;
        private void onGeneral_0_ON_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = General_0_ON_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> General_0_OFF_Select;
        private void onGeneral_0_OFF_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = General_0_OFF_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> General_1_ON_Select;
        private void onGeneral_1_ON_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = General_1_ON_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> General_1_OFF_Select;
        private void onGeneral_1_OFF_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = General_1_OFF_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> General_2_ON_Select;
        private void onGeneral_2_ON_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = General_2_ON_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> General_2_OFF_Select;
        private void onGeneral_2_OFF_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = General_2_OFF_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> General_3_ON_Select;
        private void onGeneral_3_ON_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = General_3_ON_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> General_3_OFF_Select;
        private void onGeneral_3_OFF_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = General_3_OFF_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> General_4_ON_Select;
        private void onGeneral_4_ON_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = General_4_ON_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> General_4_OFF_Select;
        private void onGeneral_4_OFF_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = General_4_OFF_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Show_EditPage(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_EditPage], this);
            }
        }

        public void Show_ClimateSpinner(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_ClimateSpinner], this);
            }
        }

        public void Show_LightsSpinner(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_LightsSpinner], this);
            }
        }

        public void Show_CurtainsSpinner(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_CurtainsSpinner], this);
            }
        }

        public void Show_GlobalSpinner(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_GlobalSpinner], this);
            }
        }

        public void Show_FanSpinner(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_FanSpinner], this);
            }
        }

        public void Show_RoomsSpinner(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_RoomsSpinner], this);
            }
        }

        public void Show_Sensors(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Sensors], this);
            }
        }

        public void Show_General(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_General], this);
            }
        }

        public void General_0_ON_FB(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.General_0_ON_FB], this);
            }
        }

        public void General_0_OFF_FB(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.General_0_OFF_FB], this);
            }
        }

        public void General_1_ON_FB(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.General_1_ON_FB], this);
            }
        }

        public void General_1_OFF_FB(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.General_1_OFF_FB], this);
            }
        }

        public void General_2_ON_FB(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.General_2_ON_FB], this);
            }
        }

        public void General_2_OFF_FB(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.General_2_OFF_FB], this);
            }
        }

        public void General_3_ON_FB(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.General_3_ON_FB], this);
            }
        }

        public void General_3_OFF_FB(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.General_3_OFF_FB], this);
            }
        }

        public void General_4_ON_FB(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.General_4_ON_FB], this);
            }
        }

        public void General_4_OFF_FB(Quick_Actions_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.General_4_OFF_FB], this);
            }
        }

        public event EventHandler<UIEventArgs> An_Reserved_In1;
        private void onAn_Reserved_In1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = An_Reserved_In1;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void NumberOf_GeneralWidgets(Quick_Actions_EditUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_GeneralWidgets], this);
            }
        }

        public event EventHandler<UIEventArgs> New_Name_Set;
        private void onNew_Name_Set(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = New_Name_Set;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Ser_Reserved_In1;
        private void onSer_Reserved_In1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Ser_Reserved_In1;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Current_Name(Quick_Actions_EditStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Current_Name], this);
            }
        }

        public void Ser_Reserved_Out1(Quick_Actions_EditStringInputSigDelegate callback)
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Quick_Actions_Edit", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            ((App_Contract.QuickActions_Rooms_Spinner)RoomsSpinner).Dispose();
            ((App_Contract.Scheduler_Lights_Spinner)LightsSpinner).Dispose();
            ((App_Contract.Sheduler_Climate_Spinner)ClimateSpinner).Dispose();
            ((App_Contract.Scheduler_Curtains_Spinner)CurtainsSpinner).Dispose();
            ((App_Contract.QuickActions_Global_Spinner)GlobalSpinner).Dispose();

            Save_Changes = null;
            Exit_EditPage = null;
            Dig_Reserved_In3 = null;
            Dig_Reserved_In4 = null;
            General_0_ON_Select = null;
            General_0_OFF_Select = null;
            General_1_ON_Select = null;
            General_1_OFF_Select = null;
            General_2_ON_Select = null;
            General_2_OFF_Select = null;
            General_3_ON_Select = null;
            General_3_OFF_Select = null;
            General_4_ON_Select = null;
            General_4_OFF_Select = null;
            An_Reserved_In1 = null;
            New_Name_Set = null;
            Ser_Reserved_In1 = null;
        }

        #endregion

    }
}
