using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    public interface ILights_Scenes
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Scene0_Select;
        event EventHandler<UIEventArgs> Scene1_Select;
        event EventHandler<UIEventArgs> Scene2_Select;
        event EventHandler<UIEventArgs> Scene3_Select;
        event EventHandler<UIEventArgs> Dig_Reserved_In1;

        void Scene0_IsSelected(Lights_ScenesBoolInputSigDelegate callback);
        void Scene1_IsSelected(Lights_ScenesBoolInputSigDelegate callback);
        void Scene2_IsSelected(Lights_ScenesBoolInputSigDelegate callback);
        void Scene3_IsSelected(Lights_ScenesBoolInputSigDelegate callback);
        void Dig_Reserved_Out1(Lights_ScenesBoolInputSigDelegate callback);
        void Scene0_Name(Lights_ScenesStringInputSigDelegate callback);
        void Scene1_Name(Lights_ScenesStringInputSigDelegate callback);
        void Scene2_Name(Lights_ScenesStringInputSigDelegate callback);
        void Scene3_Name(Lights_ScenesStringInputSigDelegate callback);

        App_Contract.ILights_Scenes_Edit Edit { get; }
    }

    public delegate void Lights_ScenesBoolInputSigDelegate(BoolInputSig boolInputSig, ILights_Scenes lights_Scenes);
    public delegate void Lights_ScenesStringInputSigDelegate(StringInputSig stringInputSig, ILights_Scenes lights_Scenes);

    internal class Lights_Scenes : ILights_Scenes, IDisposable
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
                public const uint Scene0_Select = 1;
                public const uint Scene1_Select = 2;
                public const uint Scene2_Select = 3;
                public const uint Scene3_Select = 4;
                public const uint Dig_Reserved_In1 = 5;

                public const uint Scene0_IsSelected = 1;
                public const uint Scene1_IsSelected = 2;
                public const uint Scene2_IsSelected = 3;
                public const uint Scene3_IsSelected = 4;
                public const uint Dig_Reserved_Out1 = 5;
            }
            internal static class Strings
            {

                public const uint Scene0_Name = 1;
                public const uint Scene1_Name = 2;
                public const uint Scene2_Name = 3;
                public const uint Scene3_Name = 4;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Lights_Scenes(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Scene0_Select, onScene0_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Scene1_Select, onScene1_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Scene2_Select, onScene2_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Scene3_Select, onScene3_Select);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Dig_Reserved_In1, onDig_Reserved_In1);

            Edit = new App_Contract.Lights_Scenes_Edit(ComponentMediator, 120);

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            ((App_Contract.Lights_Scenes_Edit)Edit).AddDevice(device);
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
            ((App_Contract.Lights_Scenes_Edit)Edit).RemoveDevice(device);
        }

        #endregion

        #region CH5 Contract

        public App_Contract.ILights_Scenes_Edit Edit { get; private set; }

        public event EventHandler<UIEventArgs> Scene0_Select;
        private void onScene0_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Scene0_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Scene1_Select;
        private void onScene1_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Scene1_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Scene2_Select;
        private void onScene2_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Scene2_Select;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Scene3_Select;
        private void onScene3_Select(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Scene3_Select;
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


        public void Scene0_IsSelected(Lights_ScenesBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Scene0_IsSelected], this);
            }
        }

        public void Scene1_IsSelected(Lights_ScenesBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Scene1_IsSelected], this);
            }
        }

        public void Scene2_IsSelected(Lights_ScenesBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Scene2_IsSelected], this);
            }
        }

        public void Scene3_IsSelected(Lights_ScenesBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Scene3_IsSelected], this);
            }
        }

        public void Dig_Reserved_Out1(Lights_ScenesBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Dig_Reserved_Out1], this);
            }
        }


        public void Scene0_Name(Lights_ScenesStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Scene0_Name], this);
            }
        }

        public void Scene1_Name(Lights_ScenesStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Scene1_Name], this);
            }
        }

        public void Scene2_Name(Lights_ScenesStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Scene2_Name], this);
            }
        }

        public void Scene3_Name(Lights_ScenesStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Scene3_Name], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Lights_Scenes", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            ((App_Contract.Lights_Scenes_Edit)Edit).Dispose();

            Scene0_Select = null;
            Scene1_Select = null;
            Scene2_Select = null;
            Scene3_Select = null;
            Dig_Reserved_In1 = null;
        }

        #endregion

    }
}
