using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface ICurtains_Widget
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> FirstLayer_Open;
        event EventHandler<UIEventArgs> FirstLayer_Stop;
        event EventHandler<UIEventArgs> FirstLayer_Close;
        event EventHandler<UIEventArgs> SecondLayer_Open;
        event EventHandler<UIEventArgs> SecondLayer_Stop;
        event EventHandler<UIEventArgs> SecondLayer_Close;
        event EventHandler<UIEventArgs> ThirdLayer_Open;
        event EventHandler<UIEventArgs> ThirdLayer_Stop;
        event EventHandler<UIEventArgs> ThirdLayer_Close;
        event EventHandler<UIEventArgs> Dig_Reserved_In1;

        void Show_FirstLayerControls(Curtains_WidgetBoolInputSigDelegate callback);
        void Show_SecondLayerControls(Curtains_WidgetBoolInputSigDelegate callback);
        void Show_ThirdLayerControls(Curtains_WidgetBoolInputSigDelegate callback);
        void Show_SliderControls(Curtains_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out1(Curtains_WidgetBoolInputSigDelegate callback);
        void NumberOf_Curtains(Curtains_WidgetUShortInputSigDelegate callback);
        void FirstLayer_Name(Curtains_WidgetStringInputSigDelegate callback);
        void SecondLayer_Name(Curtains_WidgetStringInputSigDelegate callback);
        void ThirdLayer_Name(Curtains_WidgetStringInputSigDelegate callback);

        App_Contract.ICurtain[] Curtain { get; }
        App_Contract.ICurtains_Scenes Scenes { get; }
    }

    public delegate void Curtains_WidgetBoolInputSigDelegate(BoolInputSig boolInputSig, ICurtains_Widget curtains_Widget);
    public delegate void Curtains_WidgetUShortInputSigDelegate(UShortInputSig uShortInputSig, ICurtains_Widget curtains_Widget);
    public delegate void Curtains_WidgetStringInputSigDelegate(StringInputSig stringInputSig, ICurtains_Widget curtains_Widget);

    internal class Curtains_Widget : ICurtains_Widget, IDisposable
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
                public const uint FirstLayer_Open = 1;
                public const uint FirstLayer_Stop = 2;
                public const uint FirstLayer_Close = 3;
                public const uint SecondLayer_Open = 4;
                public const uint SecondLayer_Stop = 5;
                public const uint SecondLayer_Close = 6;
                public const uint ThirdLayer_Open = 13;
                public const uint ThirdLayer_Stop = 14;
                public const uint ThirdLayer_Close = 15;
                public const uint Dig_Reserved_In1 = 17;

                public const uint Show_FirstLayerControls = 1;
                public const uint Show_SecondLayerControls = 2;
                public const uint Show_ThirdLayerControls = 3;
                public const uint Show_SliderControls = 4;
                public const uint Dig_Reserved_Out1 = 17;
            }
            internal static class Numerics
            {

                public const uint NumberOf_Curtains = 1;
            }
            internal static class Strings
            {

                public const uint FirstLayer_Name = 1;
                public const uint SecondLayer_Name = 2;
                public const uint ThirdLayer_Name = 3;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Curtains_Widget(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private static readonly IDictionary<uint, List<uint>> CurtainSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 122, new List<uint> { 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140 } }};

        internal static void ClearDictionaries()
        {
            CurtainSmartObjectIdMappings.Clear();
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.FirstLayer_Open, onFirstLayer_Open);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.FirstLayer_Stop, onFirstLayer_Stop);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.FirstLayer_Close, onFirstLayer_Close);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.SecondLayer_Open, onSecondLayer_Open);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.SecondLayer_Stop, onSecondLayer_Stop);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.SecondLayer_Close, onSecondLayer_Close);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ThirdLayer_Open, onThirdLayer_Open);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ThirdLayer_Stop, onThirdLayer_Stop);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ThirdLayer_Close, onThirdLayer_Close);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);

            List<uint> curtainList = CurtainSmartObjectIdMappings[controlJoinId];
            Curtain = new App_Contract.ICurtain[curtainList.Count];
            for (int index = 0; index < curtainList.Count; index++)
            {
                Curtain[index] = new App_Contract.Curtain(ComponentMediator, curtainList[index]); 
            }

            Scenes = new App_Contract.Curtains_Scenes(ComponentMediator, 141);

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Curtain.Length; index++)
            {
                ((App_Contract.Curtain)Curtain[index]).AddDevice(device);
            }
            ((App_Contract.Curtains_Scenes)Scenes).AddDevice(device);
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Curtain.Length; index++)
            {
                ((App_Contract.Curtain)Curtain[index]).RemoveDevice(device);
            }
            ((App_Contract.Curtains_Scenes)Scenes).RemoveDevice(device);
        }

        #endregion

        #region CH5 Contract

        public App_Contract.ICurtain[] Curtain { get; private set; }

        public App_Contract.ICurtains_Scenes Scenes { get; private set; }

        public event EventHandler<UIEventArgs> FirstLayer_Open;
        private void onFirstLayer_Open(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = FirstLayer_Open;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> FirstLayer_Stop;
        private void onFirstLayer_Stop(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = FirstLayer_Stop;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> FirstLayer_Close;
        private void onFirstLayer_Close(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = FirstLayer_Close;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> SecondLayer_Open;
        private void onSecondLayer_Open(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = SecondLayer_Open;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> SecondLayer_Stop;
        private void onSecondLayer_Stop(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = SecondLayer_Stop;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> SecondLayer_Close;
        private void onSecondLayer_Close(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = SecondLayer_Close;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ThirdLayer_Open;
        private void onThirdLayer_Open(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ThirdLayer_Open;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ThirdLayer_Stop;
        private void onThirdLayer_Stop(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ThirdLayer_Stop;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ThirdLayer_Close;
        private void onThirdLayer_Close(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ThirdLayer_Close;
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


        public void Show_FirstLayerControls(Curtains_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_FirstLayerControls], this);
            }
        }

        public void Show_SecondLayerControls(Curtains_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_SecondLayerControls], this);
            }
        }

        public void Show_ThirdLayerControls(Curtains_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_ThirdLayerControls], this);
            }
        }

        public void Show_SliderControls(Curtains_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_SliderControls], this);
            }
        }

        public void Dig_Reserved_Out1(Curtains_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }


        public void NumberOf_Curtains(Curtains_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Curtains], this);
            }
        }


        public void FirstLayer_Name(Curtains_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.FirstLayer_Name], this);
            }
        }

        public void SecondLayer_Name(Curtains_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.SecondLayer_Name], this);
            }
        }

        public void ThirdLayer_Name(Curtains_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.ThirdLayer_Name], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Curtains_Widget", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            for (int index = 0; index < Curtain.Length; index++)
            {
                ((App_Contract.Curtain)Curtain[index]).Dispose();
            }
            ((App_Contract.Curtains_Scenes)Scenes).Dispose();

            FirstLayer_Open = null;
            FirstLayer_Stop = null;
            FirstLayer_Close = null;
            SecondLayer_Open = null;
            SecondLayer_Stop = null;
            SecondLayer_Close = null;
            ThirdLayer_Open = null;
            ThirdLayer_Stop = null;
            ThirdLayer_Close = null;
            Dig_Reserved_In1 = null;
        }

        #endregion

    }
}
