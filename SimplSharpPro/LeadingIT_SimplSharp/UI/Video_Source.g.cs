using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IVideo_Source
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Select;
        event EventHandler<UIEventArgs> Dig_Reserved_In1;

        void IsSelected(Video_SourceBoolInputSigDelegate callback);
        void Dig_Reserved_Out1(Video_SourceBoolInputSigDelegate callback);
        void Logo_URL(Video_SourceStringInputSigDelegate callback);

    }

    public delegate void Video_SourceBoolInputSigDelegate(BoolInputSig boolInputSig, IVideo_Source video_Source);
    public delegate void Video_SourceStringInputSigDelegate(StringInputSig stringInputSig, IVideo_Source video_Source);

    internal class Video_Source : IVideo_Source, IDisposable
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
                public const uint Dig_Reserved_In1 = 2;

                public const uint IsSelected = 1;
                public const uint Dig_Reserved_Out1 = 2;
            }
            internal static class Strings
            {

                public const uint Logo_URL = 1;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Video_Source(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Select, onSelect);
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

        public event EventHandler<UIEventArgs> Dig_Reserved_In1;
        private void onDig_Reserved_In1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In1;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void IsSelected(Video_SourceBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.IsSelected], this);
            }
        }

        public void Dig_Reserved_Out1(Video_SourceBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }


        public void Logo_URL(Video_SourceStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Logo_URL], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Video_Source", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
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
            Dig_Reserved_In1 = null;
        }

        #endregion

    }
}
