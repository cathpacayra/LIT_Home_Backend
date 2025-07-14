using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    /// <summary>
    /// List on the top of the screen: Doors, Gates, etc
    /// </summary>
    /// <summary>
    /// e.g. Doors, Gates...
    /// </summary>
    public interface IAccess_Widget
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Lock_All_Doors;
        event EventHandler<UIEventArgs> Dig_Reserved_In1;
        event EventHandler<UIEventArgs> Close_All_Gates;

        void Show_UnitsList(Access_WidgetBoolInputSigDelegate callback);
        void All_Doors_AreLocked(Access_WidgetBoolInputSigDelegate callback);
        void All_Gates_AreClosed(Access_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out1(Access_WidgetBoolInputSigDelegate callback);
        void NumberOf_Units(Access_WidgetUShortInputSigDelegate callback);
        void NumberOf_Doors(Access_WidgetUShortInputSigDelegate callback);
        void NumberOf_Gates(Access_WidgetUShortInputSigDelegate callback);
        void All_Doors_Status(Access_WidgetStringInputSigDelegate callback);
        void All_Gates_Status(Access_WidgetStringInputSigDelegate callback);

        App_Contract.IAccess_Unit[] Unit { get; }
        App_Contract.IAccess_Door[] Door { get; }
        App_Contract.IAccess_Gate[] Gate { get; }
    }

    public delegate void Access_WidgetBoolInputSigDelegate(BoolInputSig boolInputSig, IAccess_Widget access_Widget);
    public delegate void Access_WidgetUShortInputSigDelegate(UShortInputSig uShortInputSig, IAccess_Widget access_Widget);
    public delegate void Access_WidgetStringInputSigDelegate(StringInputSig stringInputSig, IAccess_Widget access_Widget);

    internal class Access_Widget : IAccess_Widget, IDisposable
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
                public const uint Lock_All_Doors = 1;
                public const uint Dig_Reserved_In1 = 2;
                public const uint Close_All_Gates = 4;

                public const uint Show_UnitsList = 1;
                public const uint All_Doors_AreLocked = 2;
                public const uint All_Gates_AreClosed = 3;
                public const uint Dig_Reserved_Out1 = 4;
            }
            internal static class Numerics
            {

                public const uint NumberOf_Units = 1;
                public const uint NumberOf_Doors = 2;
                public const uint NumberOf_Gates = 3;
            }
            internal static class Strings
            {
                public const uint All_Doors_Status = 1;
                public const uint All_Gates_Status = 2;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Access_Widget(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private static readonly IDictionary<uint, List<uint>> UnitSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 203, new List<uint> { 204, 205, 206, 207, 208 } }};
        private static readonly IDictionary<uint, List<uint>> DoorSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 203, new List<uint> { 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226, 227, 228 } }};
        private static readonly IDictionary<uint, List<uint>> GateSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 203, new List<uint> { 229, 230, 231, 232, 233, 234, 235, 236, 237, 238 } }};

        internal static void ClearDictionaries()
        {
            UnitSmartObjectIdMappings.Clear();
            DoorSmartObjectIdMappings.Clear();
            GateSmartObjectIdMappings.Clear();
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Lock_All_Doors, onLock_All_Doors);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Close_All_Gates, onClose_All_Gates);

            List<uint> unitList = UnitSmartObjectIdMappings[controlJoinId];
            Unit = new App_Contract.IAccess_Unit[unitList.Count];
            for (int index = 0; index < unitList.Count; index++)
            {
                Unit[index] = new App_Contract.Access_Unit(ComponentMediator, unitList[index]); 
            }

            List<uint> doorList = DoorSmartObjectIdMappings[controlJoinId];
            Door = new App_Contract.IAccess_Door[doorList.Count];
            for (int index = 0; index < doorList.Count; index++)
            {
                Door[index] = new App_Contract.Access_Door(ComponentMediator, doorList[index]); 
            }

            List<uint> gateList = GateSmartObjectIdMappings[controlJoinId];
            Gate = new App_Contract.IAccess_Gate[gateList.Count];
            for (int index = 0; index < gateList.Count; index++)
            {
                Gate[index] = new App_Contract.Access_Gate(ComponentMediator, gateList[index]); 
            }

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Unit.Length; index++)
            {
                ((App_Contract.Access_Unit)Unit[index]).AddDevice(device);
            }
            for (int index = 0; index < Door.Length; index++)
            {
                ((App_Contract.Access_Door)Door[index]).AddDevice(device);
            }
            for (int index = 0; index < Gate.Length; index++)
            {
                ((App_Contract.Access_Gate)Gate[index]).AddDevice(device);
            }
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Unit.Length; index++)
            {
                ((App_Contract.Access_Unit)Unit[index]).RemoveDevice(device);
            }
            for (int index = 0; index < Door.Length; index++)
            {
                ((App_Contract.Access_Door)Door[index]).RemoveDevice(device);
            }
            for (int index = 0; index < Gate.Length; index++)
            {
                ((App_Contract.Access_Gate)Gate[index]).RemoveDevice(device);
            }
        }

        #endregion

        #region CH5 Contract

        public App_Contract.IAccess_Unit[] Unit { get; private set; }

        public App_Contract.IAccess_Door[] Door { get; private set; }

        public App_Contract.IAccess_Gate[] Gate { get; private set; }

        public event EventHandler<UIEventArgs> Lock_All_Doors;
        private void onLock_All_Doors(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Lock_All_Doors;
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

        public event EventHandler<UIEventArgs> Close_All_Gates;
        private void onClose_All_Gates(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Close_All_Gates;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Show_UnitsList(Access_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_UnitsList], this);
            }
        }

        public void All_Doors_AreLocked(Access_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.All_Doors_AreLocked], this);
            }
        }

        public void All_Gates_AreClosed(Access_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.All_Gates_AreClosed], this);
            }
        }

        public void Dig_Reserved_Out1(Access_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }


        public void NumberOf_Units(Access_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Units], this);
            }
        }

        public void NumberOf_Doors(Access_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Doors], this);
            }
        }

        public void NumberOf_Gates(Access_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Gates], this);
            }
        }

        public void All_Doors_Status(Access_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.All_Doors_Status], this);
            }
        }

        public void All_Gates_Status(Access_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.All_Gates_Status], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Access_Widget", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            for (int index = 0; index < Unit.Length; index++)
            {
                ((App_Contract.Access_Unit)Unit[index]).Dispose();
            }
            for (int index = 0; index < Door.Length; index++)
            {
                ((App_Contract.Access_Door)Door[index]).Dispose();
            }
            for (int index = 0; index < Gate.Length; index++)
            {
                ((App_Contract.Access_Gate)Gate[index]).Dispose();
            }

            Lock_All_Doors = null;
            Dig_Reserved_In1 = null;
            Close_All_Gates = null;
        }

        #endregion

    }
}
