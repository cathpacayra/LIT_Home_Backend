using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface ILights_Scenes_Edit
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> SaveChanges;
        event EventHandler<UIEventArgs> Dig_Reserved_In1;
        event EventHandler<UIEventArgs> Scene0_SetNewName;
        event EventHandler<UIEventArgs> Scene1_SetNewName;
        event EventHandler<UIEventArgs> Scene2_SetNewName;
        event EventHandler<UIEventArgs> Scene3_SetNewName;

        void Dig_Reserved_Out1(Lights_Scenes_EditBoolInputSigDelegate callback);
        void Scene0_CurrentName(Lights_Scenes_EditStringInputSigDelegate callback);
        void Scene1_CurrentName(Lights_Scenes_EditStringInputSigDelegate callback);
        void Scene2_CurrentName(Lights_Scenes_EditStringInputSigDelegate callback);
        void Scene3_CurrentName(Lights_Scenes_EditStringInputSigDelegate callback);

    }

    public delegate void Lights_Scenes_EditBoolInputSigDelegate(BoolInputSig boolInputSig, ILights_Scenes_Edit lights_Scenes_Edit);
    public delegate void Lights_Scenes_EditStringInputSigDelegate(StringInputSig stringInputSig, ILights_Scenes_Edit lights_Scenes_Edit);

    internal class Lights_Scenes_Edit : ILights_Scenes_Edit, IDisposable
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
                public const uint SaveChanges = 1;
                public const uint Dig_Reserved_In1 = 2;

                public const uint Dig_Reserved_Out1 = 2;
            }
            internal static class Strings
            {
                public const uint Scene0_SetNewName = 2;
                public const uint Scene1_SetNewName = 4;
                public const uint Scene2_SetNewName = 6;
                public const uint Scene3_SetNewName = 8;

                public const uint Scene0_CurrentName = 1;
                public const uint Scene1_CurrentName = 3;
                public const uint Scene2_CurrentName = 5;
                public const uint Scene3_CurrentName = 7;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Lights_Scenes_Edit(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.SaveChanges, onSaveChanges);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.Scene0_SetNewName, onScene0_SetNewName);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.Scene1_SetNewName, onScene1_SetNewName);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.Scene2_SetNewName, onScene2_SetNewName);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.Scene3_SetNewName, onScene3_SetNewName);

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

        public event EventHandler<UIEventArgs> SaveChanges;
        private void onSaveChanges(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = SaveChanges;
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


        public void Dig_Reserved_Out1(Lights_Scenes_EditBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }

        public event EventHandler<UIEventArgs> Scene0_SetNewName;
        private void onScene0_SetNewName(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Scene0_SetNewName;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Scene1_SetNewName;
        private void onScene1_SetNewName(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Scene1_SetNewName;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Scene2_SetNewName;
        private void onScene2_SetNewName(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Scene2_SetNewName;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Scene3_SetNewName;
        private void onScene3_SetNewName(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Scene3_SetNewName;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Scene0_CurrentName(Lights_Scenes_EditStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Scene0_CurrentName], this);
            }
        }

        public void Scene1_CurrentName(Lights_Scenes_EditStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Scene1_CurrentName], this);
            }
        }

        public void Scene2_CurrentName(Lights_Scenes_EditStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Scene2_CurrentName], this);
            }
        }

        public void Scene3_CurrentName(Lights_Scenes_EditStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Scene3_CurrentName], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Lights_Scenes_Edit", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            SaveChanges = null;
            Dig_Reserved_In1 = null;
            Scene0_SetNewName = null;
            Scene1_SetNewName = null;
            Scene2_SetNewName = null;
            Scene3_SetNewName = null;
        }

        #endregion

    }
}
