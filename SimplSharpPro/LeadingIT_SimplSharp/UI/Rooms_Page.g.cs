using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IRooms_Page
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Dig_Reserved_In1;
        event EventHandler<UIEventArgs> Dig_Reserved_In2;
        event EventHandler<UIEventArgs> Dig_Reserved_In3;
        event EventHandler<UIEventArgs> Dig_Reserved_In4;
        event EventHandler<UIEventArgs> An_Reserved_In1;
        event EventHandler<UIEventArgs> Ser_Reserved_In1;

        void Show_RoomsPage(Rooms_PageBoolInputSigDelegate callback);
        void Show_RoomsSelection(Rooms_PageBoolInputSigDelegate callback);
        void Show_LightsWidget(Rooms_PageBoolInputSigDelegate callback);
        void Show_ClimateWidget(Rooms_PageBoolInputSigDelegate callback);
        void Show_CurtainsWidget(Rooms_PageBoolInputSigDelegate callback);
        void Show_AudioWidget(Rooms_PageBoolInputSigDelegate callback);
        void Show_VideoWidget(Rooms_PageBoolInputSigDelegate callback);
        void Show_CinemaWidget(Rooms_PageBoolInputSigDelegate callback);
        void Show_SchedulerWidget(Rooms_PageBoolInputSigDelegate callback);
        void Show_ExhaustFanWidget(Rooms_PageBoolInputSigDelegate callback);
        void Show_QAWidget(Rooms_PageBoolInputSigDelegate callback);
        void Dig_Reserved_Out1(Rooms_PageBoolInputSigDelegate callback);
        void Dig_Reserved_Out2(Rooms_PageBoolInputSigDelegate callback);
        void Dig_Reserved_Out3(Rooms_PageBoolInputSigDelegate callback);
        void Dig_Reserved_Out4(Rooms_PageBoolInputSigDelegate callback);
        void NumberOf_Rooms(Rooms_PageUShortInputSigDelegate callback);
        void An_Reserved_Out1(Rooms_PageUShortInputSigDelegate callback);
        void SelectedSuiteName(Rooms_PageStringInputSigDelegate callback);
        void SelectedRoomName(Rooms_PageStringInputSigDelegate callback);
        void Ser_Reserved_Out1(Rooms_PageStringInputSigDelegate callback);

        App_Contract.IRoom[] Room { get; }
    }

    public delegate void Rooms_PageBoolInputSigDelegate(BoolInputSig boolInputSig, IRooms_Page rooms_Page);
    public delegate void Rooms_PageUShortInputSigDelegate(UShortInputSig uShortInputSig, IRooms_Page rooms_Page);
    public delegate void Rooms_PageStringInputSigDelegate(StringInputSig stringInputSig, IRooms_Page rooms_Page);

    internal class Rooms_Page : IRooms_Page, IDisposable
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
                public const uint Dig_Reserved_In1 = 12;
                public const uint Dig_Reserved_In2 = 13;
                public const uint Dig_Reserved_In3 = 14;
                public const uint Dig_Reserved_In4 = 15;

                public const uint Show_RoomsPage = 1;
                public const uint Show_RoomsSelection = 2;
                public const uint Show_LightsWidget = 3;
                public const uint Show_ClimateWidget = 4;
                public const uint Show_CurtainsWidget = 5;
                public const uint Show_AudioWidget = 6;
                public const uint Show_VideoWidget = 7;
                public const uint Show_CinemaWidget = 8;
                public const uint Show_SchedulerWidget = 9;
                public const uint Show_ExhaustFanWidget = 10;
                public const uint Show_QAWidget = 11;
                public const uint Dig_Reserved_Out1 = 12;
                public const uint Dig_Reserved_Out2 = 13;
                public const uint Dig_Reserved_Out3 = 14;
                public const uint Dig_Reserved_Out4 = 15;
            }
            internal static class Numerics
            {
                public const uint An_Reserved_In1 = 2;

                public const uint NumberOf_Rooms = 1;
                public const uint An_Reserved_Out1 = 2;
            }
            internal static class Strings
            {
                public const uint Ser_Reserved_In1 = 3;

                public const uint SelectedSuiteName = 1;
                public const uint SelectedRoomName = 2;
                public const uint Ser_Reserved_Out1 = 3;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Rooms_Page(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private static readonly IDictionary<uint, List<uint>> RoomSmartObjectIdMappings = new Dictionary<uint, List<uint>> {
            { 90, new List<uint> { 91, 92, 93, 94, 95 } }};

        internal static void ClearDictionaries()
        {
            RoomSmartObjectIdMappings.Clear();
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In2, onDig_Reserved_In2);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In3, onDig_Reserved_In3);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In4, onDig_Reserved_In4);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.An_Reserved_In1, onAn_Reserved_In1);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.Ser_Reserved_In1, onSer_Reserved_In1);

            List<uint> roomList = RoomSmartObjectIdMappings[controlJoinId];
            Room = new App_Contract.IRoom[roomList.Count];
            for (int index = 0; index < roomList.Count; index++)
            {
                Room[index] = new App_Contract.Room(ComponentMediator, roomList[index]); 
            }

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Room.Length; index++)
            {
                ((App_Contract.Room)Room[index]).AddDevice(device);
            }
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Room.Length; index++)
            {
                ((App_Contract.Room)Room[index]).RemoveDevice(device);
            }
        }

        #endregion

        #region CH5 Contract

        public App_Contract.IRoom[] Room { get; private set; }

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


        public void Show_RoomsPage(Rooms_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_RoomsPage], this);
            }
        }

        public void Show_RoomsSelection(Rooms_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_RoomsSelection], this);
            }
        }

        public void Show_LightsWidget(Rooms_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_LightsWidget], this);
            }
        }

        public void Show_ClimateWidget(Rooms_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_ClimateWidget], this);
            }
        }

        public void Show_CurtainsWidget(Rooms_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_CurtainsWidget], this);
            }
        }

        public void Show_AudioWidget(Rooms_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_AudioWidget], this);
            }
        }

        public void Show_VideoWidget(Rooms_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_VideoWidget], this);
            }
        }

        public void Show_CinemaWidget(Rooms_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_CinemaWidget], this);
            }
        }

        public void Show_SchedulerWidget(Rooms_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_SchedulerWidget], this);
            }
        }

        public void Show_ExhaustFanWidget(Rooms_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_ExhaustFanWidget], this);
            }
        }

        public void Show_QAWidget(Rooms_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Show_QAWidget], this);
            }
        }

        public void Dig_Reserved_Out1(Rooms_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }

        public void Dig_Reserved_Out2(Rooms_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out2], this);
            }
        }

        public void Dig_Reserved_Out3(Rooms_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out3], this);
            }
        }

        public void Dig_Reserved_Out4(Rooms_PageBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out4], this);
            }
        }

        public event EventHandler<UIEventArgs> An_Reserved_In1;
        private void onAn_Reserved_In1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = An_Reserved_In1;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void NumberOf_Rooms(Rooms_PageUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Rooms], this);
            }
        }

        public void An_Reserved_Out1(Rooms_PageUShortInputSigDelegate callback)
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


        public void SelectedSuiteName(Rooms_PageStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.SelectedSuiteName], this);
            }
        }

        public void SelectedRoomName(Rooms_PageStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.SelectedRoomName], this);
            }
        }

        public void Ser_Reserved_Out1(Rooms_PageStringInputSigDelegate callback)
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Rooms_Page", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            for (int index = 0; index < Room.Length; index++)
            {
                ((App_Contract.Room)Room[index]).Dispose();
            }

            Dig_Reserved_In1 = null;
            Dig_Reserved_In2 = null;
            Dig_Reserved_In3 = null;
            Dig_Reserved_In4 = null;
            An_Reserved_In1 = null;
            Ser_Reserved_In1 = null;
        }

        #endregion

    }
}
