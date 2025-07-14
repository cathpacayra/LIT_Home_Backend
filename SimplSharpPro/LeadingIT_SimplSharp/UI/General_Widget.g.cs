using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IGeneral_Widget
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> On;
        event EventHandler<UIEventArgs> Off;

        void On_FB(General_WidgetBoolInputSigDelegate callback);
        void Off_FB(General_WidgetBoolInputSigDelegate callback);
        void Name(General_WidgetStringInputSigDelegate callback);
        void Status(General_WidgetStringInputSigDelegate callback);
        void On_Name(General_WidgetStringInputSigDelegate callback);
        void Off_Name(General_WidgetStringInputSigDelegate callback);
        void Icon_URL(General_WidgetStringInputSigDelegate callback);

    }

    public delegate void General_WidgetBoolInputSigDelegate(BoolInputSig boolInputSig, IGeneral_Widget general_Widget);
    public delegate void General_WidgetStringInputSigDelegate(StringInputSig stringInputSig, IGeneral_Widget general_Widget);

    internal class General_Widget : IGeneral_Widget, IDisposable
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
                public const uint On = 1;
                public const uint Off = 2;

                public const uint On_FB = 1;
                public const uint Off_FB = 2;
            }
            internal static class Strings
            {

                public const uint Name = 1;
                public const uint Status = 2;
                public const uint On_Name = 3;
                public const uint Off_Name = 4;
                public const uint Icon_URL = 5;
            }
        }

        #endregion

        #region Construction and Initialization

        internal General_Widget(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.On, onOn);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Off, onOff);

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

        public event EventHandler<UIEventArgs> On;
        private void onOn(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = On;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Off;
        private void onOff(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Off;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void On_FB(General_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.On_FB], this);
            }
        }

        public void Off_FB(General_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Off_FB], this);
            }
        }


        public void Name(General_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Name], this);
            }
        }

        public void Status(General_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Status], this);
            }
        }

        public void On_Name(General_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.On_Name], this);
            }
        }

        public void Off_Name(General_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Off_Name], this);
            }
        }

        public void Icon_URL(General_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Icon_URL], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "General_Widget", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            On = null;
            Off = null;
        }

        #endregion

    }
}
