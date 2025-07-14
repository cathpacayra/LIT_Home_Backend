using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    /// <summary>
    /// high only on Master Devices
    /// </summary>
    public interface ISuitesPage_Master
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Dig_Reserved_In1;

        void Show_SuiteSelectionPage(SuitesPage_MasterBoolInputSigDelegate callback);
        void Show_FloorsFilter(SuitesPage_MasterBoolInputSigDelegate callback);
        void Show_F1SuitesList(SuitesPage_MasterBoolInputSigDelegate callback);
        void Show_F2SuitesList(SuitesPage_MasterBoolInputSigDelegate callback);
        void Show_F3SuitesList(SuitesPage_MasterBoolInputSigDelegate callback);
        void Show_F4SuitesList(SuitesPage_MasterBoolInputSigDelegate callback);
        void Show_F5SuitesList(SuitesPage_MasterBoolInputSigDelegate callback);
        void Dig_Reserved_Out1(SuitesPage_MasterBoolInputSigDelegate callback);
        void NumberOf_FloorFilters(SuitesPage_MasterUShortInputSigDelegate callback);
        void NumberOf_F1Suites(SuitesPage_MasterUShortInputSigDelegate callback);
        void NumberOf_F2Suites(SuitesPage_MasterUShortInputSigDelegate callback);
        void NumberOf_F3Suites(SuitesPage_MasterUShortInputSigDelegate callback);
        void NumberOf_F4Suites(SuitesPage_MasterUShortInputSigDelegate callback);
        void NumberOf_F5Suites(SuitesPage_MasterUShortInputSigDelegate callback);
        void An_Reserved_Out1(SuitesPage_MasterUShortInputSigDelegate callback);

        App_Contract.IFloor_Filter[] FloorFilter { get; }
        App_Contract.IF1Suite[] F1Suite { get; }
        App_Contract.IF2Suite[] F2Suite { get; }
        App_Contract.IF3Suite[] F3Suite { get; }
        App_Contract.IF4Suite[] F4Suite { get; }
        App_Contract.IF5Suite[] F5Suite { get; }
    }

    public delegate void SuitesPage_MasterBoolInputSigDelegate(BoolInputSig boolInputSig, ISuitesPage_Master suitesPage_Master);
    public delegate void SuitesPage_MasterUShortInputSigDelegate(UShortInputSig uShortInputSig, ISuitesPage_Master suitesPage_Master);

    internal class SuitesPage_Master : ISuitesPage_Master, IDisposable
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
                public const uint Dig_Reserved_In1 = 6;

                public const uint Show_SuiteSelectionPage = 1;
                public const uint Show_FloorsFilter = 2;
                public const uint Show_F1SuitesList = 3;
                public const uint Show_F2SuitesList = 4;
                public const uint Show_F3SuitesList = 5;
                public const uint Show_F4SuitesList = 6;
                public const uint Show_F5SuitesList = 7;
                public const uint Dig_Reserved_Out1 = 8;
            }
            internal static class Numerics
            {

                public const uint NumberOf_FloorFilters = 1;
                public const uint NumberOf_F1Suites = 2;
                public const uint NumberOf_F2Suites = 3;
                public const uint NumberOf_F3Suites = 4;
                public const uint NumberOf_F4Suites = 5;
                public const uint NumberOf_F5Suites = 6;
                public const uint An_Reserved_Out1 = 7;
            }
        }

        #endregion

        #region Construction and Initialization

        internal SuitesPage_Master(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private static readonly IDictionary<uint, List<uint>> FloorFilterSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 3, new List<uint> { 4, 5, 6, 7, 8, 9 } }};
        private static readonly IDictionary<uint, List<uint>> F1SuiteSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 3, new List<uint> { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 } }};
        private static readonly IDictionary<uint, List<uint>> F2SuiteSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 3, new List<uint> { 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41 } }};
        private static readonly IDictionary<uint, List<uint>> F3SuiteSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 3, new List<uint> { 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57 } }};
        private static readonly IDictionary<uint, List<uint>> F4SuiteSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 3, new List<uint> { 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73 } }};
        private static readonly IDictionary<uint, List<uint>> F5SuiteSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 3, new List<uint> { 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89 } }};

        internal static void ClearDictionaries()
        {
            FloorFilterSmartObjectIdMappings.Clear();
            F1SuiteSmartObjectIdMappings.Clear();
            F2SuiteSmartObjectIdMappings.Clear();
            F3SuiteSmartObjectIdMappings.Clear();
            F4SuiteSmartObjectIdMappings.Clear();
            F5SuiteSmartObjectIdMappings.Clear();
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);

            List<uint> floorFilterList = FloorFilterSmartObjectIdMappings[controlJoinId];
            FloorFilter = new App_Contract.IFloor_Filter[floorFilterList.Count];
            for (int index = 0; index < floorFilterList.Count; index++)
            {
                FloorFilter[index] = new App_Contract.Floor_Filter(ComponentMediator, floorFilterList[index]); 
            }

            List<uint> f1SuiteList = F1SuiteSmartObjectIdMappings[controlJoinId];
            F1Suite = new App_Contract.IF1Suite[f1SuiteList.Count];
            for (int index = 0; index < f1SuiteList.Count; index++)
            {
                F1Suite[index] = new App_Contract.F1Suite(ComponentMediator, f1SuiteList[index]); 
            }

            List<uint> f2SuiteList = F2SuiteSmartObjectIdMappings[controlJoinId];
            F2Suite = new App_Contract.IF2Suite[f2SuiteList.Count];
            for (int index = 0; index < f2SuiteList.Count; index++)
            {
                F2Suite[index] = new App_Contract.F2Suite(ComponentMediator, f2SuiteList[index]); 
            }

            List<uint> f3SuiteList = F3SuiteSmartObjectIdMappings[controlJoinId];
            F3Suite = new App_Contract.IF3Suite[f3SuiteList.Count];
            for (int index = 0; index < f3SuiteList.Count; index++)
            {
                F3Suite[index] = new App_Contract.F3Suite(ComponentMediator, f3SuiteList[index]); 
            }

            List<uint> f4SuiteList = F4SuiteSmartObjectIdMappings[controlJoinId];
            F4Suite = new App_Contract.IF4Suite[f4SuiteList.Count];
            for (int index = 0; index < f4SuiteList.Count; index++)
            {
                F4Suite[index] = new App_Contract.F4Suite(ComponentMediator, f4SuiteList[index]); 
            }

            List<uint> f5SuiteList = F5SuiteSmartObjectIdMappings[controlJoinId];
            F5Suite = new App_Contract.IF5Suite[f5SuiteList.Count];
            for (int index = 0; index < f5SuiteList.Count; index++)
            {
                F5Suite[index] = new App_Contract.F5Suite(ComponentMediator, f5SuiteList[index]); 
            }

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < FloorFilter.Length; index++)
            {
                ((App_Contract.Floor_Filter)FloorFilter[index]).AddDevice(device);
            }
            for (int index = 0; index < F1Suite.Length; index++)
            {
                ((App_Contract.F1Suite)F1Suite[index]).AddDevice(device);
            }
            for (int index = 0; index < F2Suite.Length; index++)
            {
                ((App_Contract.F2Suite)F2Suite[index]).AddDevice(device);
            }
            for (int index = 0; index < F3Suite.Length; index++)
            {
                ((App_Contract.F3Suite)F3Suite[index]).AddDevice(device);
            }
            for (int index = 0; index < F4Suite.Length; index++)
            {
                ((App_Contract.F4Suite)F4Suite[index]).AddDevice(device);
            }
            for (int index = 0; index < F5Suite.Length; index++)
            {
                ((App_Contract.F5Suite)F5Suite[index]).AddDevice(device);
            }
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < FloorFilter.Length; index++)
            {
                ((App_Contract.Floor_Filter)FloorFilter[index]).RemoveDevice(device);
            }
            for (int index = 0; index < F1Suite.Length; index++)
            {
                ((App_Contract.F1Suite)F1Suite[index]).RemoveDevice(device);
            }
            for (int index = 0; index < F2Suite.Length; index++)
            {
                ((App_Contract.F2Suite)F2Suite[index]).RemoveDevice(device);
            }
            for (int index = 0; index < F3Suite.Length; index++)
            {
                ((App_Contract.F3Suite)F3Suite[index]).RemoveDevice(device);
            }
            for (int index = 0; index < F4Suite.Length; index++)
            {
                ((App_Contract.F4Suite)F4Suite[index]).RemoveDevice(device);
            }
            for (int index = 0; index < F5Suite.Length; index++)
            {
                ((App_Contract.F5Suite)F5Suite[index]).RemoveDevice(device);
            }
        }

        #endregion

        #region CH5 Contract

        public App_Contract.IFloor_Filter[] FloorFilter { get; private set; }

        public App_Contract.IF1Suite[] F1Suite { get; private set; }

        public App_Contract.IF2Suite[] F2Suite { get; private set; }

        public App_Contract.IF3Suite[] F3Suite { get; private set; }

        public App_Contract.IF4Suite[] F4Suite { get; private set; }

        public App_Contract.IF5Suite[] F5Suite { get; private set; }

        public event EventHandler<UIEventArgs> Dig_Reserved_In1;
        private void onDig_Reserved_In1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In1;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Show_SuiteSelectionPage(SuitesPage_MasterBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_SuiteSelectionPage], this);
            }
        }

        public void Show_FloorsFilter(SuitesPage_MasterBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_FloorsFilter], this);
            }
        }

        public void Show_F1SuitesList(SuitesPage_MasterBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_F1SuitesList], this);
            }
        }

        public void Show_F2SuitesList(SuitesPage_MasterBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_F2SuitesList], this);
            }
        }

        public void Show_F3SuitesList(SuitesPage_MasterBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_F3SuitesList], this);
            }
        }

        public void Show_F4SuitesList(SuitesPage_MasterBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_F4SuitesList], this);
            }
        }

        public void Show_F5SuitesList(SuitesPage_MasterBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_F5SuitesList], this);
            }
        }

        public void Dig_Reserved_Out1(SuitesPage_MasterBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }


        public void NumberOf_FloorFilters(SuitesPage_MasterUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_FloorFilters], this);
            }
        }

        public void NumberOf_F1Suites(SuitesPage_MasterUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_F1Suites], this);
            }
        }

        public void NumberOf_F2Suites(SuitesPage_MasterUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_F2Suites], this);
            }
        }

        public void NumberOf_F3Suites(SuitesPage_MasterUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_F3Suites], this);
            }
        }

        public void NumberOf_F4Suites(SuitesPage_MasterUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_F4Suites], this);
            }
        }

        public void NumberOf_F5Suites(SuitesPage_MasterUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_F5Suites], this);
            }
        }

        public void An_Reserved_Out1(SuitesPage_MasterUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.An_Reserved_Out1], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "SuitesPage_Master", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            for (int index = 0; index < FloorFilter.Length; index++)
            {
                ((App_Contract.Floor_Filter)FloorFilter[index]).Dispose();
            }
            for (int index = 0; index < F1Suite.Length; index++)
            {
                ((App_Contract.F1Suite)F1Suite[index]).Dispose();
            }
            for (int index = 0; index < F2Suite.Length; index++)
            {
                ((App_Contract.F2Suite)F2Suite[index]).Dispose();
            }
            for (int index = 0; index < F3Suite.Length; index++)
            {
                ((App_Contract.F3Suite)F3Suite[index]).Dispose();
            }
            for (int index = 0; index < F4Suite.Length; index++)
            {
                ((App_Contract.F4Suite)F4Suite[index]).Dispose();
            }
            for (int index = 0; index < F5Suite.Length; index++)
            {
                ((App_Contract.F5Suite)F5Suite[index]).Dispose();
            }

            Dig_Reserved_In1 = null;
        }

        #endregion

    }
}
