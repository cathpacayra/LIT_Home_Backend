using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    /// <summary>
    /// High when muted
    /// </summary>
    /// <summary>
    /// Should send string 'NOW PLAYING' or 'NOT PLAYING'
    /// </summary>
    public interface IAudio_Widget
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Power_Toggle;
        event EventHandler<UIEventArgs> Play;
        event EventHandler<UIEventArgs> Pause;
        event EventHandler<UIEventArgs> Repeat;
        event EventHandler<UIEventArgs> Shuffle;
        event EventHandler<UIEventArgs> Next;
        event EventHandler<UIEventArgs> Previous;
        event EventHandler<UIEventArgs> Vol_Up;
        event EventHandler<UIEventArgs> Vol_Down;
        event EventHandler<UIEventArgs> Mute_Toggle;
        event EventHandler<UIEventArgs> Dig_Reserved_In1;
        event EventHandler<UIEventArgs> Dig_Reserved_In2;
        event EventHandler<UIEventArgs> Vol_Level_Set;
        event EventHandler<UIEventArgs> An_Reserved_In1;
        event EventHandler<UIEventArgs> ProgressBar_Level_Set;

        void Power_Toggle_FB(Audio_WidgetBoolInputSigDelegate callback);
        void Repeat_FB(Audio_WidgetBoolInputSigDelegate callback);
        void Shuffle_FB(Audio_WidgetBoolInputSigDelegate callback);
        void Mute_Toggle_FB(Audio_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out1(Audio_WidgetBoolInputSigDelegate callback);
        void Dig_Reserved_Out2(Audio_WidgetBoolInputSigDelegate callback);
        void NumberOf_Sources(Audio_WidgetUShortInputSigDelegate callback);
        void NumberOf_Zones(Audio_WidgetUShortInputSigDelegate callback);
        void Vol_Level_FB(Audio_WidgetUShortInputSigDelegate callback);
        void ProgressBar_Level_FB(Audio_WidgetUShortInputSigDelegate callback);
        void TrackProgress_Time(Audio_WidgetUShortInputSigDelegate callback);
        void TrackDuration_Time(Audio_WidgetUShortInputSigDelegate callback);
        void SelectedSource_Name(Audio_WidgetStringInputSigDelegate callback);
        void Image_URL(Audio_WidgetStringInputSigDelegate callback);
        void Album_Name(Audio_WidgetStringInputSigDelegate callback);
        void Artist_Name(Audio_WidgetStringInputSigDelegate callback);
        void Song_Name(Audio_WidgetStringInputSigDelegate callback);
        void Volume_Value(Audio_WidgetStringInputSigDelegate callback);
        void Playing_Status(Audio_WidgetStringInputSigDelegate callback);
        void TrackProgressTime_Value(Audio_WidgetStringInputSigDelegate callback);
        void TrackDurationTime_Value(Audio_WidgetStringInputSigDelegate callback);

        App_Contract.IAudio_MultiRoom_Zone[] Zone { get; }
        App_Contract.IAudio_Source[] Source { get; }
    }

    public delegate void Audio_WidgetBoolInputSigDelegate(BoolInputSig boolInputSig, IAudio_Widget audio_Widget);
    public delegate void Audio_WidgetUShortInputSigDelegate(UShortInputSig uShortInputSig, IAudio_Widget audio_Widget);
    public delegate void Audio_WidgetStringInputSigDelegate(StringInputSig stringInputSig, IAudio_Widget audio_Widget);

    internal class Audio_Widget : IAudio_Widget, IDisposable
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
                public const uint Power_Toggle = 1;
                public const uint Play = 2;
                public const uint Pause = 3;
                public const uint Repeat = 4;
                public const uint Shuffle = 5;
                public const uint Next = 6;
                public const uint Previous = 7;
                public const uint Vol_Up = 8;
                public const uint Vol_Down = 9;
                public const uint Mute_Toggle = 10;
                public const uint Dig_Reserved_In1 = 11;
                public const uint Dig_Reserved_In2 = 12;

                public const uint Power_Toggle_FB = 1;
                public const uint Repeat_FB = 4;
                public const uint Shuffle_FB = 5;
                public const uint Mute_Toggle_FB = 10;
                public const uint Dig_Reserved_Out1 = 11;
                public const uint Dig_Reserved_Out2 = 12;
            }
            internal static class Numerics
            {
                public const uint Vol_Level_Set = 2;
                public const uint An_Reserved_In1 = 3;
                public const uint ProgressBar_Level_Set = 4;

                public const uint NumberOf_Sources = 1;
                public const uint NumberOf_Zones = 2;
                public const uint Vol_Level_FB = 3;
                public const uint ProgressBar_Level_FB = 4;
                public const uint TrackProgress_Time = 5;
                public const uint TrackDuration_Time = 6;
            }
            internal static class Strings
            {

                public const uint SelectedSource_Name = 1;
                public const uint Image_URL = 2;
                public const uint Album_Name = 3;
                public const uint Artist_Name = 4;
                public const uint Song_Name = 5;
                public const uint Volume_Value = 6;
                public const uint Playing_Status = 7;
                public const uint TrackProgressTime_Value = 8;
                public const uint TrackDurationTime_Value = 9;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Audio_Widget(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private static readonly IDictionary<uint, List<uint>> ZoneSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 239, new List<uint> { 240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258, 259 } }};
        private static readonly IDictionary<uint, List<uint>> SourceSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 239, new List<uint> { 260, 261, 262, 263, 264, 265, 266, 267, 268, 269, 270, 271 } }};

        internal static void ClearDictionaries()
        {
            ZoneSmartObjectIdMappings.Clear();
            SourceSmartObjectIdMappings.Clear();
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Power_Toggle, onPower_Toggle);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Play, onPlay);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Pause, onPause);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Repeat, onRepeat);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Shuffle, onShuffle);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Next, onNext);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Previous, onPrevious);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Vol_Up, onVol_Up);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Vol_Down, onVol_Down);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Mute_Toggle, onMute_Toggle);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In2, onDig_Reserved_In2);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.Vol_Level_Set, onVol_Level_Set);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.An_Reserved_In1, onAn_Reserved_In1);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.ProgressBar_Level_Set, onProgressBar_Level_Set);

            List<uint> zoneList = ZoneSmartObjectIdMappings[controlJoinId];
            Zone = new App_Contract.IAudio_MultiRoom_Zone[zoneList.Count];
            for (int index = 0; index < zoneList.Count; index++)
            {
                Zone[index] = new App_Contract.Audio_MultiRoom_Zone(ComponentMediator, zoneList[index]); 
            }

            List<uint> sourceList = SourceSmartObjectIdMappings[controlJoinId];
            Source = new App_Contract.IAudio_Source[sourceList.Count];
            for (int index = 0; index < sourceList.Count; index++)
            {
                Source[index] = new App_Contract.Audio_Source(ComponentMediator, sourceList[index]); 
            }

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Zone.Length; index++)
            {
                ((App_Contract.Audio_MultiRoom_Zone)Zone[index]).AddDevice(device);
            }
            for (int index = 0; index < Source.Length; index++)
            {
                ((App_Contract.Audio_Source)Source[index]).AddDevice(device);
            }
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Zone.Length; index++)
            {
                ((App_Contract.Audio_MultiRoom_Zone)Zone[index]).RemoveDevice(device);
            }
            for (int index = 0; index < Source.Length; index++)
            {
                ((App_Contract.Audio_Source)Source[index]).RemoveDevice(device);
            }
        }

        #endregion

        #region CH5 Contract

        public App_Contract.IAudio_MultiRoom_Zone[] Zone { get; private set; }

        public App_Contract.IAudio_Source[] Source { get; private set; }

        public event EventHandler<UIEventArgs> Power_Toggle;
        private void onPower_Toggle(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Power_Toggle;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Play;
        private void onPlay(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Play;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Pause;
        private void onPause(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Pause;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Repeat;
        private void onRepeat(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Repeat;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Shuffle;
        private void onShuffle(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Shuffle;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Next;
        private void onNext(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Next;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Previous;
        private void onPrevious(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Previous;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Vol_Up;
        private void onVol_Up(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Vol_Up;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Vol_Down;
        private void onVol_Down(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Vol_Down;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Mute_Toggle;
        private void onMute_Toggle(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Mute_Toggle;
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

        public event EventHandler<UIEventArgs> Dig_Reserved_In2;
        private void onDig_Reserved_In2(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In2;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Power_Toggle_FB(Audio_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Power_Toggle_FB], this);
            }
        }

        public void Repeat_FB(Audio_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Repeat_FB], this);
            }
        }

        public void Shuffle_FB(Audio_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Shuffle_FB], this);
            }
        }

        public void Mute_Toggle_FB(Audio_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Mute_Toggle_FB], this);
            }
        }

        public void Dig_Reserved_Out1(Audio_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }

        public void Dig_Reserved_Out2(Audio_WidgetBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out2], this);
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

        public event EventHandler<UIEventArgs> ProgressBar_Level_Set;
        private void onProgressBar_Level_Set(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ProgressBar_Level_Set;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void NumberOf_Sources(Audio_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Sources], this);
            }
        }

        public void NumberOf_Zones(Audio_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Zones], this);
            }
        }

        public void Vol_Level_FB(Audio_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Vol_Level_FB], this);
            }
        }

        public void ProgressBar_Level_FB(Audio_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.ProgressBar_Level_FB], this);
            }
        }

        public void TrackProgress_Time(Audio_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.TrackProgress_Time], this);
            }
        }

        public void TrackDuration_Time(Audio_WidgetUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.TrackDuration_Time], this);
            }
        }


        public void SelectedSource_Name(Audio_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.SelectedSource_Name], this);
            }
        }

        public void Image_URL(Audio_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Image_URL], this);
            }
        }

        public void Album_Name(Audio_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Album_Name], this);
            }
        }

        public void Artist_Name(Audio_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Artist_Name], this);
            }
        }

        public void Song_Name(Audio_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Song_Name], this);
            }
        }

        public void Volume_Value(Audio_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Volume_Value], this);
            }
        }

        public void Playing_Status(Audio_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Playing_Status], this);
            }
        }

        public void TrackProgressTime_Value(Audio_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.TrackProgressTime_Value], this);
            }
        }

        public void TrackDurationTime_Value(Audio_WidgetStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.TrackDurationTime_Value], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Audio_Widget", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            for (int index = 0; index < Zone.Length; index++)
            {
                ((App_Contract.Audio_MultiRoom_Zone)Zone[index]).Dispose();
            }
            for (int index = 0; index < Source.Length; index++)
            {
                ((App_Contract.Audio_Source)Source[index]).Dispose();
            }

            Power_Toggle = null;
            Play = null;
            Pause = null;
            Repeat = null;
            Shuffle = null;
            Next = null;
            Previous = null;
            Vol_Up = null;
            Vol_Down = null;
            Mute_Toggle = null;
            Dig_Reserved_In1 = null;
            Dig_Reserved_In2 = null;
            Vol_Level_Set = null;
            An_Reserved_In1 = null;
            ProgressBar_Level_Set = null;
        }

        #endregion

    }
}
