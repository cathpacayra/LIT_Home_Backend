using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IGlobalClimate_Widget
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Dig_Reserved_In1;
        event EventHandler<UIEventArgs> Dig_Reserved_In2;
        event EventHandler<UIEventArgs> Dig_Reserved_In3;
        event EventHandler<UIEventArgs> Dig_Reserved_In4;

        void Show_FloorsFilter(GlobalClimate_WidgetBoolInputSigDelegate callback);
        void Show_F1AreasList(GlobalClimate_WidgetBoolInputSigDelegate callback);
        void Show_F2AreasList(GlobalClimate_WidgetBoolInputSigDelegate callback);
        void Show_F3AreasList(GlobalClimate_WidgetBoolInputSigDelegate callback);
        void Show_F4AreasList(GlobalClimate_WidgetBoolInputSigDelegate callback);
        void Show_F5AreasList(GlobalClimate_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out1(GlobalClimate_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out2(GlobalClimate_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out3(GlobalClimate_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out4(GlobalClimate_WidgetBoolInputSigDelegate callback);
        void NumberOf_FloorFilters(GlobalClimate_WidgetUShortInputSigDelegate callback);
        void NumberOf_F1Areas(GlobalClimate_WidgetUShortInputSigDelegate callback);
        void NumberOf_F2Areas(GlobalClimate_WidgetUShortInputSigDelegate callback);
        void NumberOf_F3Areas(GlobalClimate_WidgetUShortInputSigDelegate callback);
        void NumberOf_F4Areas(GlobalClimate_WidgetUShortInputSigDelegate callback);
        void NumberOf_F5Areas(GlobalClimate_WidgetUShortInputSigDelegate callback);
        void An_Reserved_Out1(GlobalClimate_WidgetUShortInputSigDelegate callback);

        App_Contract.IGlobalClimate_Thermostat Thermostat { get; }
        App_Contract.IGlobalClimate_FloorFilter[] FloorFilter { get; }
        App_Contract.IGlobalClimate_F1Area[] F1Area { get; }
        App_Contract.IGlobalClimate_F2Area[] F2Area { get; }
        App_Contract.IGlobalClimate_F3Area[] F3Area { get; }
        App_Contract.IGlobalClimate_F4Area[] F4Area { get; }
        App_Contract.IGlobalClimate_F5Area[] F5Area { get; }
    }

    public delegate void GlobalClimate_WidgetBoolInputSigDelegate(BoolInputSig boolInputSig, IGlobalClimate_Widget globalClimate_Widget);
    public delegate void GlobalClimate_WidgetUShortInputSigDelegate(UShortInputSig uShortInputSig, IGlobalClimate_Widget globalClimate_Widget);

    internal class GlobalClimate_Widget : IGlobalClimate_Widget, IDisposable
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
                public const uint Dig_Reserved_In1 = 7;
                public const uint Dig_Reserved_In2 = 8;
                public const uint Dig_Reserved_In3 = 9;
                public const uint Dig_Reserved_In4 = 10;

                public const uint Show_FloorsFilter = 1;
                public const uint Show_F1AreasList = 2;
                public const uint Show_F2AreasList = 3;
                public const uint Show_F3AreasList = 4;
                public const uint Show_F4AreasList = 5;
                public const uint Show_F5AreasList = 6;
                public const uint Dig_Reserved_Out1 = 7;
                public const uint Dig_Reserved_Out2 = 8;
                public const uint Dig_Reserved_Out3 = 9;
                public const uint Dig_Reserved_Out4 = 10;
            }
            internal static class Numerics
            {

                public const uint NumberOf_FloorFilters = 1;
                public const uint NumberOf_F1Areas = 2;
                public const uint NumberOf_F2Areas = 3;
                public const uint NumberOf_F3Areas = 4;
                public const uint NumberOf_F4Areas = 5;
                public const uint NumberOf_F5Areas = 6;
                public const uint An_Reserved_Out1 = 7;
            }
        }

        #endregion

        #region Construction and Initialization

        internal GlobalClimate_Widget(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private static readonly IDictionary<uint, List<uint>> FloorFilterSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 300, new List<uint> { 302, 303, 304, 305, 306, 307 } }};
        private static readonly IDictionary<uint, List<uint>> F1AreaSmartObjectIdMappings = new Dictionary<uint, List<uint>> {

            { 300, new List<uint> { 308, 309, 310, 311, 312, 313, 314, 315, 316, 317, 318, 319, 320, 321, 322, 323, 324, 325, 326, 327, 328, 329, 330, 331 } }};
        private static readonly IDictionary<uint, List<uint>> F2AreaSmartObjectIdMappings = new Dictionary<uint, List<uint>> {

            { 300, new List<uint> { 332, 333, 334, 335, 336, 337, 338, 339, 340, 341, 342, 343, 344, 345, 346, 347, 348, 349, 350, 351, 352, 353, 354, 355 } }};
        private static readonly IDictionary<uint, List<uint>> F3AreaSmartObjectIdMappings = new Dictionary<uint, List<uint>> {

            { 300, new List<uint> { 356, 357, 358, 359, 360, 361, 362, 363, 364, 365, 366, 367, 368, 369, 370, 371, 372, 373, 374, 375, 376, 377, 378, 379 } }};
        private static readonly IDictionary<uint, List<uint>> F4AreaSmartObjectIdMappings = new Dictionary<uint, List<uint>> {

            { 300, new List<uint> { 380, 381, 382, 383, 384, 385, 386, 387, 388, 389, 390, 391, 392, 393, 394, 395, 396, 397, 398, 399, 400, 401, 402, 403 } }};
        private static readonly IDictionary<uint, List<uint>> F5AreaSmartObjectIdMappings = new Dictionary<uint, List<uint>> {

            { 300, new List<uint> { 404, 405, 406, 407, 408, 409, 410, 411, 412, 413, 414, 415, 416, 417, 418, 419, 420, 421, 422, 423, 424, 425, 426, 427 } }};

        internal static void ClearDictionaries()
        {
            FloorFilterSmartObjectIdMappings.Clear();
            F1AreaSmartObjectIdMappings.Clear();
            F2AreaSmartObjectIdMappings.Clear();
            F3AreaSmartObjectIdMappings.Clear();
            F4AreaSmartObjectIdMappings.Clear();
            F5AreaSmartObjectIdMappings.Clear();
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In2, onDig_Reserved_In2);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In3, onDig_Reserved_In3);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In4, onDig_Reserved_In4);

            Thermostat = new App_Contract.GlobalClimate_Thermostat(ComponentMediator, 301);

            List<uint> floorFilterList = FloorFilterSmartObjectIdMappings[controlJoinId];
            FloorFilter = new App_Contract.IGlobalClimate_FloorFilter[floorFilterList.Count];
            for (int index = 0; index < floorFilterList.Count; index++)
            {
                FloorFilter[index] = new App_Contract.GlobalClimate_FloorFilter(ComponentMediator, floorFilterList[index]); 
            }

            List<uint> f1AreaList = F1AreaSmartObjectIdMappings[controlJoinId];
            F1Area = new App_Contract.IGlobalClimate_F1Area[f1AreaList.Count];
            for (int index = 0; index < f1AreaList.Count; index++)
            {
                F1Area[index] = new App_Contract.GlobalClimate_F1Area(ComponentMediator, f1AreaList[index]); 
            }

            List<uint> f2AreaList = F2AreaSmartObjectIdMappings[controlJoinId];
            F2Area = new App_Contract.IGlobalClimate_F2Area[f2AreaList.Count];
            for (int index = 0; index < f2AreaList.Count; index++)
            {
                F2Area[index] = new App_Contract.GlobalClimate_F2Area(ComponentMediator, f2AreaList[index]); 
            }

            List<uint> f3AreaList = F3AreaSmartObjectIdMappings[controlJoinId];
            F3Area = new App_Contract.IGlobalClimate_F3Area[f3AreaList.Count];
            for (int index = 0; index < f3AreaList.Count; index++)
            {
                F3Area[index] = new App_Contract.GlobalClimate_F3Area(ComponentMediator, f3AreaList[index]); 
            }

            List<uint> f4AreaList = F4AreaSmartObjectIdMappings[controlJoinId];
            F4Area = new App_Contract.IGlobalClimate_F4Area[f4AreaList.Count];
            for (int index = 0; index < f4AreaList.Count; index++)
            {
                F4Area[index] = new App_Contract.GlobalClimate_F4Area(ComponentMediator, f4AreaList[index]); 
            }

            List<uint> f5AreaList = F5AreaSmartObjectIdMappings[controlJoinId];
            F5Area = new App_Contract.IGlobalClimate_F5Area[f5AreaList.Count];
            for (int index = 0; index < f5AreaList.Count; index++)
            {
                F5Area[index] = new App_Contract.GlobalClimate_F5Area(ComponentMediator, f5AreaList[index]); 
            }

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            ((App_Contract.GlobalClimate_Thermostat)Thermostat).AddDevice(device);
            for (int index = 0; index < FloorFilter.Length; index++)
            {
                ((App_Contract.GlobalClimate_FloorFilter)FloorFilter[index]).AddDevice(device);
            }
            for (int index = 0; index < F1Area.Length; index++)
            {
                ((App_Contract.GlobalClimate_F1Area)F1Area[index]).AddDevice(device);
            }
            for (int index = 0; index < F2Area.Length; index++)
            {
                ((App_Contract.GlobalClimate_F2Area)F2Area[index]).AddDevice(device);
            }
            for (int index = 0; index < F3Area.Length; index++)
            {
                ((App_Contract.GlobalClimate_F3Area)F3Area[index]).AddDevice(device);
            }
            for (int index = 0; index < F4Area.Length; index++)
            {
                ((App_Contract.GlobalClimate_F4Area)F4Area[index]).AddDevice(device);
            }
            for (int index = 0; index < F5Area.Length; index++)
            {
                ((App_Contract.GlobalClimate_F5Area)F5Area[index]).AddDevice(device);
            }
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            ((App_Contract.GlobalClimate_Thermostat)Thermostat).RemoveDevice(device);
            for (int index = 0; index < FloorFilter.Length; index++)
            {
                ((App_Contract.GlobalClimate_FloorFilter)FloorFilter[index]).RemoveDevice(device);
            }
            for (int index = 0; index < F1Area.Length; index++)
            {
                ((App_Contract.GlobalClimate_F1Area)F1Area[index]).RemoveDevice(device);
            }
            for (int index = 0; index < F2Area.Length; index++)
            {
                ((App_Contract.GlobalClimate_F2Area)F2Area[index]).RemoveDevice(device);
            }
            for (int index = 0; index < F3Area.Length; index++)
            {
                ((App_Contract.GlobalClimate_F3Area)F3Area[index]).RemoveDevice(device);
            }
            for (int index = 0; index < F4Area.Length; index++)
            {
                ((App_Contract.GlobalClimate_F4Area)F4Area[index]).RemoveDevice(device);
            }
            for (int index = 0; index < F5Area.Length; index++)
            {
                ((App_Contract.GlobalClimate_F5Area)F5Area[index]).RemoveDevice(device);
            }
        }

        #endregion

        #region CH5 Contract

        public App_Contract.IGlobalClimate_Thermostat Thermostat { get; private set; }

        public App_Contract.IGlobalClimate_FloorFilter[] FloorFilter { get; private set; }

        public App_Contract.IGlobalClimate_F1Area[] F1Area { get; private set; }

        public App_Contract.IGlobalClimate_F2Area[] F2Area { get; private set; }

        public App_Contract.IGlobalClimate_F3Area[] F3Area { get; private set; }

        public App_Contract.IGlobalClimate_F4Area[] F4Area { get; private set; }

        public App_Contract.IGlobalClimate_F5Area[] F5Area { get; private set; }

        public event EventHandler<UIEventArgs> Dig_Reserved_In1;
        private void onDig_Reserved_In1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In1;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Dig_Reserved_In2;
        private void onDig_Reserved_In2(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In2;
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


        public void Show_FloorsFilter(GlobalClimate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_FloorsFilter], this);
            }
        }

        public void Show_F1AreasList(GlobalClimate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_F1AreasList], this);
            }
        }

        public void Show_F2AreasList(GlobalClimate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_F2AreasList], this);
            }
        }

        public void Show_F3AreasList(GlobalClimate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_F3AreasList], this);
            }
        }

        public void Show_F4AreasList(GlobalClimate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_F4AreasList], this);
            }
        }

        public void Show_F5AreasList(GlobalClimate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_F5AreasList], this);
            }
        }

        public void Dig_Reserved_Out1(GlobalClimate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }

        public void Dig_Reserved_Out2(GlobalClimate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out2], this);
            }
        }

        public void Dig_Reserved_Out3(GlobalClimate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out3], this);
            }
        }

        public void Dig_Reserved_Out4(GlobalClimate_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out4], this);
            }
        }


        public void NumberOf_FloorFilters(GlobalClimate_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_FloorFilters], this);
            }
        }

        public void NumberOf_F1Areas(GlobalClimate_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_F1Areas], this);
            }
        }

        public void NumberOf_F2Areas(GlobalClimate_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_F2Areas], this);
            }
        }

        public void NumberOf_F3Areas(GlobalClimate_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_F3Areas], this);
            }
        }

        public void NumberOf_F4Areas(GlobalClimate_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_F4Areas], this);
            }
        }

        public void NumberOf_F5Areas(GlobalClimate_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_F5Areas], this);
            }
        }

        public void An_Reserved_Out1(GlobalClimate_WidgetUShortInputSigDelegate callback)
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "GlobalClimate_Widget", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            ((App_Contract.GlobalClimate_Thermostat)Thermostat).Dispose();
            for (int index = 0; index < FloorFilter.Length; index++)
            {
                ((App_Contract.GlobalClimate_FloorFilter)FloorFilter[index]).Dispose();
            }
            for (int index = 0; index < F1Area.Length; index++)
            {
                ((App_Contract.GlobalClimate_F1Area)F1Area[index]).Dispose();
            }
            for (int index = 0; index < F2Area.Length; index++)
            {
                ((App_Contract.GlobalClimate_F2Area)F2Area[index]).Dispose();
            }
            for (int index = 0; index < F3Area.Length; index++)
            {
                ((App_Contract.GlobalClimate_F3Area)F3Area[index]).Dispose();
            }
            for (int index = 0; index < F4Area.Length; index++)
            {
                ((App_Contract.GlobalClimate_F4Area)F4Area[index]).Dispose();
            }
            for (int index = 0; index < F5Area.Length; index++)
            {
                ((App_Contract.GlobalClimate_F5Area)F5Area[index]).Dispose();
            }

            Dig_Reserved_In1 = null;
            Dig_Reserved_In2 = null;
            Dig_Reserved_In3 = null;
            Dig_Reserved_In4 = null;
        }

        #endregion

    }
}
