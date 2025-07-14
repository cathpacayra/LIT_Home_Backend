using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    /// <summary>
    /// QA-Quick Action
    /// </summary>
    /// <summary>
    /// QA-Quick Action
    /// </summary>
    /// <summary>
    /// QA-Quick Action
    /// </summary>
    /// <summary>
    /// QA-Quick Action
    /// </summary>
    /// <summary>
    /// QA-Quick Action
    /// </summary>
    /// <summary>
    /// QA-Quick Action
    /// </summary>
    public interface IQuick_Actions
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> AreaQA_0_Trigger;
        event EventHandler<UIEventArgs> AreaQA_1_Trigger;
        event EventHandler<UIEventArgs> AreaQA_2_Trigger;
        event EventHandler<UIEventArgs> Dig_Reserved_In1;
        event EventHandler<UIEventArgs> AreaQA_0_Select;
        event EventHandler<UIEventArgs> AreaQA_1_Select;
        event EventHandler<UIEventArgs> AreaQA_2_Select;
        event EventHandler<UIEventArgs> An_Reserved_In1;
        event EventHandler<UIEventArgs> Ser_Reserved_In1;
        event EventHandler<UIEventArgs> Ser_Reserved_In2;
        event EventHandler<UIEventArgs> Ser_Reserved_In3;
        event EventHandler<UIEventArgs> Ser_Reserved_In4;

        void AreaQA_0_Trigger_FB(Quick_ActionsBoolInputSigDelegate callback);
        void AreaQA_1_Trigger_FB(Quick_ActionsBoolInputSigDelegate callback);
        void AreaQA_2_Trigger_FB(Quick_ActionsBoolInputSigDelegate callback);
        void Dig_Reserved_Out1(Quick_ActionsBoolInputSigDelegate callback);
        void AreaQA_0_IsSelected(Quick_ActionsBoolInputSigDelegate callback);
        void AreaQA_1_IsSelected(Quick_ActionsBoolInputSigDelegate callback);
        void AreaQA_2_IsSelected(Quick_ActionsBoolInputSigDelegate callback);
        void An_Reserved_Out1(Quick_ActionsUShortInputSigDelegate callback);
        void QA_0_Name(Quick_ActionsStringInputSigDelegate callback);
        void QA_1_Name(Quick_ActionsStringInputSigDelegate callback);
        void QA_2_Name(Quick_ActionsStringInputSigDelegate callback);
        void QA_3_Name(Quick_ActionsStringInputSigDelegate callback);
        void Ser_Reserved_Out1(Quick_ActionsStringInputSigDelegate callback);
        void Ser_Reserved_Out2(Quick_ActionsStringInputSigDelegate callback);
        void Ser_Reserved_Out3(Quick_ActionsStringInputSigDelegate callback);
        void Ser_Reserved_Out4(Quick_ActionsStringInputSigDelegate callback);
        void AreaQA_0_Name(Quick_ActionsStringInputSigDelegate callback);
        void AreaQA_1_Name(Quick_ActionsStringInputSigDelegate callback);
        void AreaQA_2_Name(Quick_ActionsStringInputSigDelegate callback);

        App_Contract.IQuick_Actions_Edit Edit { get; }
    }

    public delegate void Quick_ActionsBoolInputSigDelegate(BoolInputSig boolInputSig, IQuick_Actions quick_Actions);
    public delegate void Quick_ActionsUShortInputSigDelegate(UShortInputSig uShortInputSig, IQuick_Actions quick_Actions);
    public delegate void Quick_ActionsStringInputSigDelegate(StringInputSig stringInputSig, IQuick_Actions quick_Actions);

    internal class Quick_Actions : IQuick_Actions, IDisposable
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
                public const uint AreaQA_0_Trigger = 1;
                public const uint AreaQA_1_Trigger = 2;
                public const uint AreaQA_2_Trigger = 3;
                public const uint Dig_Reserved_In1 = 4;
                public const uint AreaQA_0_Select = 5;
                public const uint AreaQA_1_Select = 6;
                public const uint AreaQA_2_Select = 7;

                public const uint AreaQA_0_Trigger_FB = 1;
                public const uint AreaQA_1_Trigger_FB = 2;
                public const uint AreaQA_2_Trigger_FB = 3;
                public const uint Dig_Reserved_Out1 = 4;
                public const uint AreaQA_0_IsSelected = 5;
                public const uint AreaQA_1_IsSelected = 6;
                public const uint AreaQA_2_IsSelected = 7;
            }
            internal static class Numerics
            {
                public const uint An_Reserved_In1 = 1;

                public const uint An_Reserved_Out1 = 1;
            }
            internal static class Strings
            {
                public const uint Ser_Reserved_In1 = 5;
                public const uint Ser_Reserved_In2 = 6;
                public const uint Ser_Reserved_In3 = 7;
                public const uint Ser_Reserved_In4 = 8;

                public const uint QA_0_Name = 1;
                public const uint QA_1_Name = 2;
                public const uint QA_2_Name = 3;
                public const uint QA_3_Name = 4;
                public const uint Ser_Reserved_Out1 = 5;
                public const uint Ser_Reserved_Out2 = 6;
                public const uint Ser_Reserved_Out3 = 7;
                public const uint Ser_Reserved_Out4 = 8;
                public const uint AreaQA_0_Name = 9;
                public const uint AreaQA_1_Name = 10;
                public const uint AreaQA_2_Name = 11;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Quick_Actions(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.AreaQA_0_Trigger, onAreaQA_0_Trigger);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.AreaQA_1_Trigger, onAreaQA_1_Trigger);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.AreaQA_2_Trigger, onAreaQA_2_Trigger);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.AreaQA_0_Select, onAreaQA_0_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.AreaQA_1_Select, onAreaQA_1_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.AreaQA_2_Select, onAreaQA_2_Select);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.An_Reserved_In1, onAn_Reserved_In1);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.Ser_Reserved_In1, onSer_Reserved_In1);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.Ser_Reserved_In2, onSer_Reserved_In2);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.Ser_Reserved_In3, onSer_Reserved_In3);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.Ser_Reserved_In4, onSer_Reserved_In4);

            Edit = new App_Contract.Quick_Actions_Edit(ComponentMediator, 273);

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            ((App_Contract.Quick_Actions_Edit)Edit).AddDevice(device);
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            ((App_Contract.Quick_Actions_Edit)Edit).RemoveDevice(device);
        }

        #endregion

        #region CH5 Contract

        public App_Contract.IQuick_Actions_Edit Edit { get; private set; }

        public event EventHandler<UIEventArgs> AreaQA_0_Trigger;
        private void onAreaQA_0_Trigger(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AreaQA_0_Trigger;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> AreaQA_1_Trigger;
        private void onAreaQA_1_Trigger(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AreaQA_1_Trigger;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> AreaQA_2_Trigger;
        private void onAreaQA_2_Trigger(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AreaQA_2_Trigger;
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

        public event EventHandler<UIEventArgs> AreaQA_0_Select;
        private void onAreaQA_0_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AreaQA_0_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> AreaQA_1_Select;
        private void onAreaQA_1_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AreaQA_1_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> AreaQA_2_Select;
        private void onAreaQA_2_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AreaQA_2_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void AreaQA_0_Trigger_FB(Quick_ActionsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.AreaQA_0_Trigger_FB], this);
            }
        }

        public void AreaQA_1_Trigger_FB(Quick_ActionsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.AreaQA_1_Trigger_FB], this);
            }
        }

        public void AreaQA_2_Trigger_FB(Quick_ActionsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.AreaQA_2_Trigger_FB], this);
            }
        }

        public void Dig_Reserved_Out1(Quick_ActionsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }

        public void AreaQA_0_IsSelected(Quick_ActionsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.AreaQA_0_IsSelected], this);
            }
        }

        public void AreaQA_1_IsSelected(Quick_ActionsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.AreaQA_1_IsSelected], this);
            }
        }

        public void AreaQA_2_IsSelected(Quick_ActionsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.AreaQA_2_IsSelected], this);
            }
        }

        public event EventHandler<UIEventArgs> An_Reserved_In1;
        private void onAn_Reserved_In1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = An_Reserved_In1;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void An_Reserved_Out1(Quick_ActionsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.An_Reserved_Out1], this);
            }
        }

        public event EventHandler<UIEventArgs> Ser_Reserved_In1;
        private void onSer_Reserved_In1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Ser_Reserved_In1;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Ser_Reserved_In2;
        private void onSer_Reserved_In2(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Ser_Reserved_In2;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Ser_Reserved_In3;
        private void onSer_Reserved_In3(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Ser_Reserved_In3;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Ser_Reserved_In4;
        private void onSer_Reserved_In4(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Ser_Reserved_In4;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void QA_0_Name(Quick_ActionsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.QA_0_Name], this);
            }
        }

        public void QA_1_Name(Quick_ActionsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.QA_1_Name], this);
            }
        }

        public void QA_2_Name(Quick_ActionsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.QA_2_Name], this);
            }
        }

        public void QA_3_Name(Quick_ActionsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.QA_3_Name], this);
            }
        }

        public void Ser_Reserved_Out1(Quick_ActionsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out1], this);
            }
        }

        public void Ser_Reserved_Out2(Quick_ActionsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out2], this);
            }
        }

        public void Ser_Reserved_Out3(Quick_ActionsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out3], this);
            }
        }

        public void Ser_Reserved_Out4(Quick_ActionsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out4], this);
            }
        }

        public void AreaQA_0_Name(Quick_ActionsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.AreaQA_0_Name], this);
            }
        }

        public void AreaQA_1_Name(Quick_ActionsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.AreaQA_1_Name], this);
            }
        }

        public void AreaQA_2_Name(Quick_ActionsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.AreaQA_2_Name], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Quick_Actions", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            ((App_Contract.Quick_Actions_Edit)Edit).Dispose();

            AreaQA_0_Trigger = null;
            AreaQA_1_Trigger = null;
            AreaQA_2_Trigger = null;
            Dig_Reserved_In1 = null;
            AreaQA_0_Select = null;
            AreaQA_1_Select = null;
            AreaQA_2_Select = null;
            An_Reserved_In1 = null;
            Ser_Reserved_In1 = null;
            Ser_Reserved_In2 = null;
            Ser_Reserved_In3 = null;
            Ser_Reserved_In4 = null;
        }

        #endregion

    }
}
