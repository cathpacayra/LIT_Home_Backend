using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IFooter
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> HomePage_Select;
        event EventHandler<UIEventArgs> RoomsPage_Select;
        event EventHandler<UIEventArgs> MorePage_Select;
        event EventHandler<UIEventArgs> UI_BootUp;

        void HomePage_IsSelected(FooterBoolInputSigDelegate callback);
        void RoomsPage_IsSelected(FooterBoolInputSigDelegate callback);
        void MorePage_IsSelected(FooterBoolInputSigDelegate callback);
        void MainPage_IsSelected(FooterBoolInputSigDelegate callback);

    }

    public delegate void FooterBoolInputSigDelegate(BoolInputSig boolInputSig, IFooter footer);

    internal class Footer : IFooter, IDisposable
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
                public const uint HomePage_Select = 1;
                public const uint RoomsPage_Select = 2;
                public const uint MorePage_Select = 3;
                public const uint UI_BootUp = 6;

                public const uint HomePage_IsSelected = 1;
                public const uint RoomsPage_IsSelected = 2;
                public const uint MorePage_IsSelected = 3;
                public const uint MainPage_IsSelected = 5;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Footer(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.HomePage_Select, onHomePage_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.RoomsPage_Select, onRoomsPage_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.MorePage_Select, onMorePage_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.UI_BootUp, onUI_BootUp);

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

        public event EventHandler<UIEventArgs> HomePage_Select;
        private void onHomePage_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = HomePage_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> RoomsPage_Select;
        private void onRoomsPage_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = RoomsPage_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> MorePage_Select;
        private void onMorePage_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = MorePage_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> UI_BootUp;
        private void onUI_BootUp(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = UI_BootUp;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void HomePage_IsSelected(FooterBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.HomePage_IsSelected], this);
            }
        }

        public void RoomsPage_IsSelected(FooterBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.RoomsPage_IsSelected], this);
            }
        }

        public void MorePage_IsSelected(FooterBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.MorePage_IsSelected], this);
            }
        }

        public void MainPage_IsSelected(FooterBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.MainPage_IsSelected], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Footer", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            HomePage_Select = null;
            RoomsPage_Select = null;
            MorePage_Select = null;
            UI_BootUp = null;
        }

        #endregion

    }
}
