using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface ISheduler_Climate_Spinner
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> NumberOf_SelectedItem;
        event EventHandler<UIEventArgs> NumberOf_Fan_SelectedItem;

        void NumberOf_Items(Sheduler_Climate_SpinnerUShortInputSigDelegate callback);
        void NumberOf_SelectedItem_FB(Sheduler_Climate_SpinnerUShortInputSigDelegate callback);
        void NumberOf_Fan_Items(Sheduler_Climate_SpinnerUShortInputSigDelegate callback);
        void NumberOf_Fan_SelectedItem_FB(Sheduler_Climate_SpinnerUShortInputSigDelegate callback);
        void Item0_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item1_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item2_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item3_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item4_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item5_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item6_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item7_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item8_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item9_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item10_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item11_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item12_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item13_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item14_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item15_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item16_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item17_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item18_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item19_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item20_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item21_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item22_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item23_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item24_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item25_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item26_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item27_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item28_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item29_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item30_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item31_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item32_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item33_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item34_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Item35_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Fan_Item0_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Fan_Item1_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Fan_Item2_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Fan_Item3_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Fan_Item4_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);
        void Fan_Item5_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback);

    }

    public delegate void Sheduler_Climate_SpinnerUShortInputSigDelegate(UShortInputSig uShortInputSig, ISheduler_Climate_Spinner sheduler_Climate_Spinner);
    public delegate void Sheduler_Climate_SpinnerStringInputSigDelegate(StringInputSig stringInputSig, ISheduler_Climate_Spinner sheduler_Climate_Spinner);

    internal class Sheduler_Climate_Spinner : ISheduler_Climate_Spinner, IDisposable
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
            internal static class Numerics
            {
                public const uint NumberOf_SelectedItem = 1;
                public const uint NumberOf_Fan_SelectedItem = 2;

                public const uint NumberOf_Items = 1;
                public const uint NumberOf_SelectedItem_FB = 2;
                public const uint NumberOf_Fan_Items = 3;
                public const uint NumberOf_Fan_SelectedItem_FB = 4;
            }
            internal static class Strings
            {

                public const uint Item0_Name = 1;
                public const uint Item1_Name = 2;
                public const uint Item2_Name = 3;
                public const uint Item3_Name = 4;
                public const uint Item4_Name = 5;
                public const uint Item5_Name = 6;
                public const uint Item6_Name = 7;
                public const uint Item7_Name = 8;
                public const uint Item8_Name = 9;
                public const uint Item9_Name = 10;
                public const uint Item10_Name = 11;
                public const uint Item11_Name = 12;
                public const uint Item12_Name = 13;
                public const uint Item13_Name = 14;
                public const uint Item14_Name = 15;
                public const uint Item15_Name = 16;
                public const uint Item16_Name = 17;
                public const uint Item17_Name = 18;
                public const uint Item18_Name = 19;
                public const uint Item19_Name = 20;
                public const uint Item20_Name = 21;
                public const uint Item21_Name = 22;
                public const uint Item22_Name = 23;
                public const uint Item23_Name = 24;
                public const uint Item24_Name = 25;
                public const uint Item25_Name = 26;
                public const uint Item26_Name = 27;
                public const uint Item27_Name = 28;
                public const uint Item28_Name = 29;
                public const uint Item29_Name = 30;
                public const uint Item30_Name = 31;
                public const uint Item31_Name = 32;
                public const uint Item32_Name = 33;
                public const uint Item33_Name = 34;
                public const uint Item34_Name = 35;
                public const uint Item35_Name = 36;
                public const uint Fan_Item0_Name = 37;
                public const uint Fan_Item1_Name = 38;
                public const uint Fan_Item2_Name = 39;
                public const uint Fan_Item3_Name = 40;
                public const uint Fan_Item4_Name = 41;
                public const uint Fan_Item5_Name = 42;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Sheduler_Climate_Spinner(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.NumberOf_SelectedItem, onNumberOf_SelectedItem);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.NumberOf_Fan_SelectedItem, onNumberOf_Fan_SelectedItem);

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

        public event EventHandler<UIEventArgs> NumberOf_SelectedItem;
        private void onNumberOf_SelectedItem(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = NumberOf_SelectedItem;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> NumberOf_Fan_SelectedItem;
        private void onNumberOf_Fan_SelectedItem(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = NumberOf_Fan_SelectedItem;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void NumberOf_Items(Sheduler_Climate_SpinnerUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Items], this);
            }
        }

        public void NumberOf_SelectedItem_FB(Sheduler_Climate_SpinnerUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_SelectedItem_FB], this);
            }
        }

        public void NumberOf_Fan_Items(Sheduler_Climate_SpinnerUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Fan_Items], this);
            }
        }

        public void NumberOf_Fan_SelectedItem_FB(Sheduler_Climate_SpinnerUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Fan_SelectedItem_FB], this);
            }
        }


        public void Item0_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item0_Name], this);
            }
        }

        public void Item1_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item1_Name], this);
            }
        }

        public void Item2_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item2_Name], this);
            }
        }

        public void Item3_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item3_Name], this);
            }
        }

        public void Item4_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item4_Name], this);
            }
        }

        public void Item5_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item5_Name], this);
            }
        }

        public void Item6_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item6_Name], this);
            }
        }

        public void Item7_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item7_Name], this);
            }
        }

        public void Item8_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item8_Name], this);
            }
        }

        public void Item9_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item9_Name], this);
            }
        }

        public void Item10_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item10_Name], this);
            }
        }

        public void Item11_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item11_Name], this);
            }
        }

        public void Item12_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item12_Name], this);
            }
        }

        public void Item13_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item13_Name], this);
            }
        }

        public void Item14_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item14_Name], this);
            }
        }

        public void Item15_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item15_Name], this);
            }
        }

        public void Item16_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item16_Name], this);
            }
        }

        public void Item17_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item17_Name], this);
            }
        }

        public void Item18_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item18_Name], this);
            }
        }

        public void Item19_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item19_Name], this);
            }
        }

        public void Item20_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item20_Name], this);
            }
        }

        public void Item21_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item21_Name], this);
            }
        }

        public void Item22_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item22_Name], this);
            }
        }

        public void Item23_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item23_Name], this);
            }
        }

        public void Item24_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item24_Name], this);
            }
        }

        public void Item25_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item25_Name], this);
            }
        }

        public void Item26_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item26_Name], this);
            }
        }

        public void Item27_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item27_Name], this);
            }
        }

        public void Item28_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item28_Name], this);
            }
        }

        public void Item29_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item29_Name], this);
            }
        }

        public void Item30_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item30_Name], this);
            }
        }

        public void Item31_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item31_Name], this);
            }
        }

        public void Item32_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item32_Name], this);
            }
        }

        public void Item33_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item33_Name], this);
            }
        }

        public void Item34_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item34_Name], this);
            }
        }

        public void Item35_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item35_Name], this);
            }
        }

        public void Fan_Item0_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Fan_Item0_Name], this);
            }
        }

        public void Fan_Item1_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Fan_Item1_Name], this);
            }
        }

        public void Fan_Item2_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Fan_Item2_Name], this);
            }
        }

        public void Fan_Item3_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Fan_Item3_Name], this);
            }
        }

        public void Fan_Item4_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Fan_Item4_Name], this);
            }
        }

        public void Fan_Item5_Name(Sheduler_Climate_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Fan_Item5_Name], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Sheduler_Climate_Spinner", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            NumberOf_SelectedItem = null;
            NumberOf_Fan_SelectedItem = null;
        }

        #endregion

    }
}
