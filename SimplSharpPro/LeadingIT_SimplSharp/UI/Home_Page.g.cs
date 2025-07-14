using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    /// <summary>
    /// QA_0_Trigger
    /// </summary>
    /// <summary>
    /// QA_1_Trigger
    /// </summary>
    /// <summary>
    /// QA_2_Trigger
    /// </summary>
    /// <summary>
    /// QA_3_Trigger
    /// </summary>
    /// <summary>
    /// Dig_Reserved_In4
    /// </summary>
    /// <summary>
    /// Dig_Reserved_In5
    /// </summary>
    public interface IHome_Page
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> QA_0_Trigger;
        event EventHandler<UIEventArgs> QA_1_Trigger;
        event EventHandler<UIEventArgs> QA_2_Trigger;
        event EventHandler<UIEventArgs> QA_3_Trigger;
        event EventHandler<UIEventArgs> Dig_Reserved_In4;
        event EventHandler<UIEventArgs> Dig_Reserved_In5;

        void Show_QuickActions(Home_PageBoolInputSigDelegate callback);
        void Show_GlobalControls(Home_PageBoolInputSigDelegate callback);
        void Show_IntercomWidget(Home_PageBoolInputSigDelegate callback);
        void Show_AccessWidget(Home_PageBoolInputSigDelegate callback);
        void Show_WeatherWidget(Home_PageBoolInputSigDelegate callback);
        void Show_GlobalClimateWidget(Home_PageBoolInputSigDelegate callback);
        void Show_Time(Home_PageBoolInputSigDelegate callback);
        void QA_0_Trigger_FB(Home_PageBoolInputSigDelegate callback);
        void QA_1_Trigger_FB(Home_PageBoolInputSigDelegate callback);
        void QA_2_Trigger_FB(Home_PageBoolInputSigDelegate callback);
        void QA_3_Trigger_FB(Home_PageBoolInputSigDelegate callback);
        void Show_Small_Logo(Home_PageBoolInputSigDelegate callback);
        void Show_Big_Logo(Home_PageBoolInputSigDelegate callback);
        void Show_GlobalSchedulerWidget(Home_PageBoolInputSigDelegate callback);
        void An_Reserved_Out1(Home_PageUShortInputSigDelegate callback);
        void An_Reserved_Out2(Home_PageUShortInputSigDelegate callback);
        void SuiteName(Home_PageStringInputSigDelegate callback);
        void Time(Home_PageStringInputSigDelegate callback);
        void Date(Home_PageStringInputSigDelegate callback);
        void Small_Logo_URL(Home_PageStringInputSigDelegate callback);
        void Big_Logo_URL(Home_PageStringInputSigDelegate callback);

    }

    public delegate void Home_PageBoolInputSigDelegate(BoolInputSig boolInputSig, IHome_Page home_Page);
    public delegate void Home_PageUShortInputSigDelegate(UShortInputSig uShortInputSig, IHome_Page home_Page);
    public delegate void Home_PageStringInputSigDelegate(StringInputSig stringInputSig, IHome_Page home_Page);

    internal class Home_Page : IHome_Page, IDisposable
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
                public const uint QA_0_Trigger = 8;
                public const uint QA_1_Trigger = 9;
                public const uint QA_2_Trigger = 10;
                public const uint QA_3_Trigger = 11;
                public const uint Dig_Reserved_In4 = 12;
                public const uint Dig_Reserved_In5 = 13;

                public const uint Show_QuickActions = 1;
                public const uint Show_GlobalControls = 2;
                public const uint Show_IntercomWidget = 3;
                public const uint Show_AccessWidget = 4;
                public const uint Show_WeatherWidget = 5;
                public const uint Show_GlobalClimateWidget = 6;
                public const uint Show_Time = 7;
                public const uint QA_0_Trigger_FB = 8;
                public const uint QA_1_Trigger_FB = 9;
                public const uint QA_2_Trigger_FB = 10;
                public const uint QA_3_Trigger_FB = 11;
                public const uint Show_Small_Logo = 12;
                public const uint Show_Big_Logo = 13;
                public const uint Show_GlobalSchedulerWidget = 14;
            }
            internal static class Numerics
            {

                public const uint An_Reserved_Out1 = 1;
                public const uint An_Reserved_Out2 = 2;
            }
            internal static class Strings
            {

                public const uint SuiteName = 1;
                public const uint Time = 2;
                public const uint Date = 3;
                public const uint Small_Logo_URL = 4;
                public const uint Big_Logo_URL = 5;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Home_Page(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.QA_0_Trigger, onQA_0_Trigger);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.QA_1_Trigger, onQA_1_Trigger);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.QA_2_Trigger, onQA_2_Trigger);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.QA_3_Trigger, onQA_3_Trigger);
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

        public event EventHandler<UIEventArgs> QA_0_Trigger;
        private void onQA_0_Trigger(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = QA_0_Trigger;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> QA_1_Trigger;
        private void onQA_1_Trigger(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = QA_1_Trigger;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> QA_2_Trigger;
        private void onQA_2_Trigger(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = QA_2_Trigger;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> QA_3_Trigger;
        private void onQA_3_Trigger(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = QA_3_Trigger;
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


        public void Show_QuickActions(Home_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_QuickActions], this);
            }
        }

        public void Show_GlobalControls(Home_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_GlobalControls], this);
            }
        }

        public void Show_IntercomWidget(Home_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_IntercomWidget], this);
            }
        }

        public void Show_AccessWidget(Home_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_AccessWidget], this);
            }
        }

        public void Show_WeatherWidget(Home_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_WeatherWidget], this);
            }
        }

        public void Show_GlobalClimateWidget(Home_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_GlobalClimateWidget], this);
            }
        }

        public void Show_Time(Home_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Time], this);
            }
        }

        public void QA_0_Trigger_FB(Home_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.QA_0_Trigger_FB], this);
            }
        }

        public void QA_1_Trigger_FB(Home_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.QA_1_Trigger_FB], this);
            }
        }

        public void QA_2_Trigger_FB(Home_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.QA_2_Trigger_FB], this);
            }
        }

        public void QA_3_Trigger_FB(Home_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.QA_3_Trigger_FB], this);
            }
        }

        public void Show_Small_Logo(Home_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Small_Logo], this);
            }
        }

        public void Show_Big_Logo(Home_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_Big_Logo], this);
            }
        }

        public void Show_GlobalSchedulerWidget(Home_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_GlobalSchedulerWidget], this);
            }
        }


        public void An_Reserved_Out1(Home_PageUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.An_Reserved_Out1], this);
            }
        }

        public void An_Reserved_Out2(Home_PageUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.An_Reserved_Out2], this);
            }
        }


        public void SuiteName(Home_PageStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.SuiteName], this);
            }
        }

        public void Time(Home_PageStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Time], this);
            }
        }

        public void Date(Home_PageStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Date], this);
            }
        }

        public void Small_Logo_URL(Home_PageStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Small_Logo_URL], this);
            }
        }

        public void Big_Logo_URL(Home_PageStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Big_Logo_URL], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Home_Page", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            QA_0_Trigger = null;
            QA_1_Trigger = null;
            QA_2_Trigger = null;
            QA_3_Trigger = null;
            Dig_Reserved_In4 = null;
            Dig_Reserved_In5 = null;
        }

        #endregion

    }
}
