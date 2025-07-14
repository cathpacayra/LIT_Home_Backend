using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IGeneral_Widgets
    {
        object UserObject { get; set; }

        void Show_GenWidget_0(General_WidgetsBoolInputSigDelegate callback);
        void Show_GenWidget_1(General_WidgetsBoolInputSigDelegate callback);
        void Show_GenWidget_2(General_WidgetsBoolInputSigDelegate callback);
        void Show_GenWidget_3(General_WidgetsBoolInputSigDelegate callback);
        void Show_GenWidget_4(General_WidgetsBoolInputSigDelegate callback);

        App_Contract.IGeneral_Widget[] Widget { get; }
    }

    public delegate void General_WidgetsBoolInputSigDelegate(BoolInputSig boolInputSig, IGeneral_Widgets general_Widgets);

    internal class General_Widgets : IGeneral_Widgets, IDisposable
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

                public const uint Show_GenWidget_0 = 1;
                public const uint Show_GenWidget_1 = 2;
                public const uint Show_GenWidget_2 = 3;
                public const uint Show_GenWidget_3 = 4;
                public const uint Show_GenWidget_4 = 5;
            }
        }

        #endregion

        #region Construction and Initialization

        internal General_Widgets(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private static readonly IDictionary<uint, List<uint>> WidgetSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 428, new List<uint> { 429, 430, 431, 432, 433 } }};

        internal static void ClearDictionaries()
        {
            WidgetSmartObjectIdMappings.Clear();
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            List<uint> widgetList = WidgetSmartObjectIdMappings[controlJoinId];
            Widget = new App_Contract.IGeneral_Widget[widgetList.Count];
            for (int index = 0; index < widgetList.Count; index++)
            {
                Widget[index] = new App_Contract.General_Widget(ComponentMediator, widgetList[index]); 
            }

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Widget.Length; index++)
            {
                ((App_Contract.General_Widget)Widget[index]).AddDevice(device);
            }
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Widget.Length; index++)
            {
                ((App_Contract.General_Widget)Widget[index]).RemoveDevice(device);
            }
        }

        #endregion

        #region CH5 Contract

        public App_Contract.IGeneral_Widget[] Widget { get; private set; }


        public void Show_GenWidget_0(General_WidgetsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_GenWidget_0], this);
            }
        }

        public void Show_GenWidget_1(General_WidgetsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_GenWidget_1], this);
            }
        }

        public void Show_GenWidget_2(General_WidgetsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_GenWidget_2], this);
            }
        }

        public void Show_GenWidget_3(General_WidgetsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_GenWidget_3], this);
            }
        }

        public void Show_GenWidget_4(General_WidgetsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_GenWidget_4], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "General_Widgets", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            for (int index = 0; index < Widget.Length; index++)
            {
                ((App_Contract.General_Widget)Widget[index]).Dispose();
            }

        }

        #endregion

    }
}
