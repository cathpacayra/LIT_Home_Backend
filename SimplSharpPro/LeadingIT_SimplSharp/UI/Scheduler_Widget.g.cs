using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    /// <summary>
    /// Shows the delete alert window
    /// </summary>
    public interface IScheduler_Widget
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Cancel_DeleteEvent;

        void Show_DeletePopUp(Scheduler_WidgetBoolInputSigDelegate callback);
        void NumberOf_Events(Scheduler_WidgetUShortInputSigDelegate callback);

        App_Contract.IScheduler_Event[] Event { get; }
        App_Contract.IScheduler_Edit_Event Edit { get; }
    }

    public delegate void Scheduler_WidgetBoolInputSigDelegate(BoolInputSig boolInputSig, IScheduler_Widget scheduler_Widget);
    public delegate void Scheduler_WidgetUShortInputSigDelegate(UShortInputSig uShortInputSig, IScheduler_Widget scheduler_Widget);

    internal class Scheduler_Widget : IScheduler_Widget, IDisposable
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
                public const uint Cancel_DeleteEvent = 2;

                public const uint Show_DeletePopUp = 1;
            }
            internal static class Numerics
            {

                public const uint NumberOf_Events = 1;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Scheduler_Widget(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private static readonly IDictionary<uint, List<uint>> EventSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 143, new List<uint> { 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158 } }};

        internal static void ClearDictionaries()
        {
            EventSmartObjectIdMappings.Clear();

            App_Contract.Scheduler_Edit_Event.ClearDictionaries();
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Cancel_DeleteEvent, onCancel_DeleteEvent);

            List<uint> eventList = EventSmartObjectIdMappings[controlJoinId];
            Event = new App_Contract.IScheduler_Event[eventList.Count];
            for (int index = 0; index < eventList.Count; index++)
            {
                Event[index] = new App_Contract.Scheduler_Event(ComponentMediator, eventList[index]); 
            }

            Edit = new App_Contract.Scheduler_Edit_Event(ComponentMediator, 159);

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Event.Length; index++)
            {
                ((App_Contract.Scheduler_Event)Event[index]).AddDevice(device);
            }
            ((App_Contract.Scheduler_Edit_Event)Edit).AddDevice(device);
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Event.Length; index++)
            {
                ((App_Contract.Scheduler_Event)Event[index]).RemoveDevice(device);
            }
            ((App_Contract.Scheduler_Edit_Event)Edit).RemoveDevice(device);
        }

        #endregion

        #region CH5 Contract

        public App_Contract.IScheduler_Event[] Event { get; private set; }

        public App_Contract.IScheduler_Edit_Event Edit { get; private set; }

        public event EventHandler<UIEventArgs> Cancel_DeleteEvent;
        private void onCancel_DeleteEvent(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Cancel_DeleteEvent;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Show_DeletePopUp(Scheduler_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_DeletePopUp], this);
            }
        }


        public void NumberOf_Events(Scheduler_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Events], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Scheduler_Widget", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            for (int index = 0; index < Event.Length; index++)
            {
                ((App_Contract.Scheduler_Event)Event[index]).Dispose();
            }
            ((App_Contract.Scheduler_Edit_Event)Edit).Dispose();

            Cancel_DeleteEvent = null;
        }

        #endregion

    }
}
