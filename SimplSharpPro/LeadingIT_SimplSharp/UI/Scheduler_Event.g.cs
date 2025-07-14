using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    /// <summary>
    /// High when ON
    /// </summary>
    /// <summary>
    /// Optional
    /// </summary>
    public interface IScheduler_Event
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Select;
        event EventHandler<UIEventArgs> On_Off_Toggle;
        event EventHandler<UIEventArgs> Delete_Event_Select;
        event EventHandler<UIEventArgs> Dig_Reserved_In1;

        void Is_Selected(Scheduler_EventBoolInputSigDelegate callback);
        void OnOffToggle_FB(Scheduler_EventBoolInputSigDelegate callback);
        void Delete_Event_IsSelected(Scheduler_EventBoolInputSigDelegate callback);
        void Dig_Reserved_Out1(Scheduler_EventBoolInputSigDelegate callback);
        void Name(Scheduler_EventStringInputSigDelegate callback);
        void Time(Scheduler_EventStringInputSigDelegate callback);
        void Day(Scheduler_EventStringInputSigDelegate callback);
        void Ser_Reserved_Out1(Scheduler_EventStringInputSigDelegate callback);

    }

    public delegate void Scheduler_EventBoolInputSigDelegate(BoolInputSig boolInputSig, IScheduler_Event scheduler_Event);
    public delegate void Scheduler_EventStringInputSigDelegate(StringInputSig stringInputSig, IScheduler_Event scheduler_Event);

    internal class Scheduler_Event : IScheduler_Event, IDisposable
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
                public const uint Select = 1;
                public const uint On_Off_Toggle = 2;
                public const uint Delete_Event_Select = 3;
                public const uint Dig_Reserved_In1 = 4;

                public const uint Is_Selected = 1;
                public const uint OnOffToggle_FB = 2;
                public const uint Delete_Event_IsSelected = 3;
                public const uint Dig_Reserved_Out1 = 4;
            }
            internal static class Strings
            {

                public const uint Name = 1;
                public const uint Time = 2;
                public const uint Day = 3;
                public const uint Ser_Reserved_Out1 = 4;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Scheduler_Event(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Select, onSelect);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.On_Off_Toggle, onOn_Off_Toggle);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Delete_Event_Select, onDelete_Event_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);

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

        public event EventHandler<UIEventArgs> Select;
        private void onSelect(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> On_Off_Toggle;
        private void onOn_Off_Toggle(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = On_Off_Toggle;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Delete_Event_Select;
        private void onDelete_Event_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Delete_Event_Select;
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


        public void Is_Selected(Scheduler_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Is_Selected], this);
            }
        }

        public void OnOffToggle_FB(Scheduler_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.OnOffToggle_FB], this);
            }
        }

        public void Delete_Event_IsSelected(Scheduler_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Delete_Event_IsSelected], this);
            }
        }

        public void Dig_Reserved_Out1(Scheduler_EventBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }


        public void Name(Scheduler_EventStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Name], this);
            }
        }

        public void Time(Scheduler_EventStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Time], this);
            }
        }

        public void Day(Scheduler_EventStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Day], this);
            }
        }

        public void Ser_Reserved_Out1(Scheduler_EventStringInputSigDelegate callback)
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Scheduler_Event", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            Select = null;
            On_Off_Toggle = null;
            Delete_Event_Select = null;
            Dig_Reserved_In1 = null;
        }

        #endregion

    }
}
