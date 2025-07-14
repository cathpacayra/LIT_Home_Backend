using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface IIntercom_ContactList
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Dig_Reserved_In1;

        void Dig_Reserved_Out1(Intercom_ContactListBoolInputSigDelegate callback);
        void NumberOf_Contacts(Intercom_ContactListUShortInputSigDelegate callback);

        App_Contract.IIntercom_Contact[] Contact { get; }
    }

    public delegate void Intercom_ContactListBoolInputSigDelegate(BoolInputSig boolInputSig, IIntercom_ContactList intercom_ContactList);
    public delegate void Intercom_ContactListUShortInputSigDelegate(UShortInputSig uShortInputSig, IIntercom_ContactList intercom_ContactList);

    internal class Intercom_ContactList : IIntercom_ContactList, IDisposable
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
                public const uint Dig_Reserved_In1 = 1;

                public const uint Dig_Reserved_Out1 = 1;
            }
            internal static class Numerics
            {

                public const uint NumberOf_Contacts = 1;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Intercom_ContactList(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private static readonly IDictionary<uint, List<uint>> ContactSmartObjectIdMappings = new Dictionary<uint, List<uint>> {

            { 177, new List<uint> { 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202 } }};

        internal static void ClearDictionaries()
        {
            ContactSmartObjectIdMappings.Clear();
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);

            List<uint> contactList = ContactSmartObjectIdMappings[controlJoinId];
            Contact = new App_Contract.IIntercom_Contact[contactList.Count];
            for (int index = 0; index < contactList.Count; index++)
            {
                Contact[index] = new App_Contract.Intercom_Contact(ComponentMediator, contactList[index]); 
            }

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Contact.Length; index++)
            {
                ((App_Contract.Intercom_Contact)Contact[index]).AddDevice(device);
            }
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            for (int index = 0; index < Contact.Length; index++)
            {
                ((App_Contract.Intercom_Contact)Contact[index]).RemoveDevice(device);
            }
        }

        #endregion

        #region CH5 Contract

        public App_Contract.IIntercom_Contact[] Contact { get; private set; }

        public event EventHandler<UIEventArgs> Dig_Reserved_In1;
        private void onDig_Reserved_In1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Dig_Reserved_In1;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Dig_Reserved_Out1(Intercom_ContactListBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }


        public void NumberOf_Contacts(Intercom_ContactListUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOf_Contacts], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Intercom_ContactList", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            for (int index = 0; index < Contact.Length; index++)
            {
                ((App_Contract.Intercom_Contact)Contact[index]).Dispose();
            }

            Dig_Reserved_In1 = null;
        }

        #endregion

    }
}
