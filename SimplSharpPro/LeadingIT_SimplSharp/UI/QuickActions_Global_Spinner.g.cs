using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IQuickActions_Global_Spinner
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> NumberOf_SelectedItem;

        void NumberOf_Items(QuickActions_Global_SpinnerUShortInputSigDelegate callback);
        void NumberOf_SelectedItem_FB(QuickActions_Global_SpinnerUShortInputSigDelegate callback);
        void Item0_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback);
        void Item1_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback);
        void Item2_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback);
        void Item3_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback);
        void Item4_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback);
        void Item5_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback);
        void Item6_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback);
        void Item7_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback);
        void Item8_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback);
        void Item9_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback);

    }

    public delegate void QuickActions_Global_SpinnerUShortInputSigDelegate(UShortInputSig uShortInputSig, IQuickActions_Global_Spinner quickActions_Global_Spinner);
    public delegate void QuickActions_Global_SpinnerStringInputSigDelegate(StringInputSig stringInputSig, IQuickActions_Global_Spinner quickActions_Global_Spinner);

    internal class QuickActions_Global_Spinner : IQuickActions_Global_Spinner, IDisposable
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
                public const uint NumberOf_SelectedItem = 2;

                public const uint NumberOf_Items = 1;
                public const uint NumberOf_SelectedItem_FB = 2;
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
            }
        }

        #endregion

        #region Construction and Initialization

        internal QuickActions_Global_Spinner(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.NumberOf_SelectedItem, onNumberOf_SelectedItem);

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


        public void NumberOf_Items(QuickActions_Global_SpinnerUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Items], this);
            }
        }

        public void NumberOf_SelectedItem_FB(QuickActions_Global_SpinnerUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_SelectedItem_FB], this);
            }
        }


        public void Item0_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item0_Name], this);
            }
        }

        public void Item1_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item1_Name], this);
            }
        }

        public void Item2_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item2_Name], this);
            }
        }

        public void Item3_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item3_Name], this);
            }
        }

        public void Item4_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item4_Name], this);
            }
        }

        public void Item5_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item5_Name], this);
            }
        }

        public void Item6_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item6_Name], this);
            }
        }

        public void Item7_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item7_Name], this);
            }
        }

        public void Item8_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item8_Name], this);
            }
        }

        public void Item9_Name(QuickActions_Global_SpinnerStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Item9_Name], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "QuickActions_Global_Spinner", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
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
        }

        #endregion

    }
}
