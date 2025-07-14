using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IAudio_MultiRoom_Zone
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> On_Off_Toggle;
        event EventHandler<UIEventArgs> Dig_Reserved_In1;
        event EventHandler<UIEventArgs> Vol_Level_Set;
        event EventHandler<UIEventArgs> An_Reserved_In1;

        void OnOffToggle_FB(Audio_MultiRoom_ZoneBoolInputSigDelegate callback);
        void Dig_Reserved_Out1(Audio_MultiRoom_ZoneBoolInputSigDelegate callback);
        void Vol_Level_FB(Audio_MultiRoom_ZoneUShortInputSigDelegate callback);
        void An_Reserved_Out1(Audio_MultiRoom_ZoneUShortInputSigDelegate callback);
        void Name(Audio_MultiRoom_ZoneStringInputSigDelegate callback);
        void Ser_Reserved_Out1(Audio_MultiRoom_ZoneStringInputSigDelegate callback);

    }

    public delegate void Audio_MultiRoom_ZoneBoolInputSigDelegate(BoolInputSig boolInputSig, IAudio_MultiRoom_Zone audio_MultiRoom_Zone);
    public delegate void Audio_MultiRoom_ZoneUShortInputSigDelegate(UShortInputSig uShortInputSig, IAudio_MultiRoom_Zone audio_MultiRoom_Zone);
    public delegate void Audio_MultiRoom_ZoneStringInputSigDelegate(StringInputSig stringInputSig, IAudio_MultiRoom_Zone audio_MultiRoom_Zone);

    internal class Audio_MultiRoom_Zone : IAudio_MultiRoom_Zone, IDisposable
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
                public const uint On_Off_Toggle = 1;
                public const uint Dig_Reserved_In1 = 2;

                public const uint OnOffToggle_FB = 1;
                public const uint Dig_Reserved_Out1 = 2;
            }
            internal static class Numerics
            {
                public const uint Vol_Level_Set = 1;
                public const uint An_Reserved_In1 = 2;

                public const uint Vol_Level_FB = 1;
                public const uint An_Reserved_Out1 = 2;
            }
            internal static class Strings
            {

                public const uint Name = 1;
                public const uint Ser_Reserved_Out1 = 2;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Audio_MultiRoom_Zone(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.On_Off_Toggle, onOn_Off_Toggle);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.Vol_Level_Set, onVol_Level_Set);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.An_Reserved_In1, onAn_Reserved_In1);

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

        public event EventHandler<UIEventArgs> On_Off_Toggle;
        private void onOn_Off_Toggle(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = On_Off_Toggle;
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


        public void OnOffToggle_FB(Audio_MultiRoom_ZoneBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.OnOffToggle_FB], this);
            }
        }

        public void Dig_Reserved_Out1(Audio_MultiRoom_ZoneBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }

        public event EventHandler<UIEventArgs> Vol_Level_Set;
        private void onVol_Level_Set(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Vol_Level_Set;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> An_Reserved_In1;
        private void onAn_Reserved_In1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = An_Reserved_In1;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Vol_Level_FB(Audio_MultiRoom_ZoneUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Vol_Level_FB], this);
            }
        }

        public void An_Reserved_Out1(Audio_MultiRoom_ZoneUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.An_Reserved_Out1], this);
            }
        }


        public void Name(Audio_MultiRoom_ZoneStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Name], this);
            }
        }

        public void Ser_Reserved_Out1(Audio_MultiRoom_ZoneStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Ser_Reserved_Out1], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Audio_MultiRoom_Zone", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            On_Off_Toggle = null;
            Dig_Reserved_In1 = null;
            Vol_Level_Set = null;
            An_Reserved_In1 = null;
        }

        #endregion

    }
}
