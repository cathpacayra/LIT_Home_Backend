using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace App_Contract
{
    /// <summary>
    /// Common Interface for Root Contracts.
    /// </summary>
    public interface IContract
    {
        object UserObject { get; set; }
        void AddDevice(BasicTriListWithSmartObject device);
        void RemoveDevice(BasicTriListWithSmartObject device);
    }

    public class Contract : IContract, IDisposable
    {
        #region Components

        private ComponentMediator ComponentMediator { get; set; }

        public App_Contract.IFooter Footer { get { return (App_Contract.IFooter)InternalFooter; } }
        private App_Contract.Footer InternalFooter { get; set; }

        public App_Contract.IHome_Page HomePage { get { return (App_Contract.IHome_Page)InternalHomePage; } }
        private App_Contract.Home_Page InternalHomePage { get; set; }

        public App_Contract.ISuitesPage_Master SuitesPageMaster { get { return (App_Contract.ISuitesPage_Master)InternalSuitesPageMaster; } }
        private App_Contract.SuitesPage_Master InternalSuitesPageMaster { get; set; }

        public App_Contract.IRooms_Page RoomsPage { get { return (App_Contract.IRooms_Page)InternalRoomsPage; } }
        private App_Contract.Rooms_Page InternalRoomsPage { get; set; }

        public App_Contract.ILights_Widget Lights { get { return (App_Contract.ILights_Widget)InternalLights; } }
        private App_Contract.Lights_Widget InternalLights { get; set; }

        public App_Contract.IClimate_Widget Climate { get { return (App_Contract.IClimate_Widget)InternalClimate; } }
        private App_Contract.Climate_Widget InternalClimate { get; set; }

        public App_Contract.ICurtains_Widget Curtains { get { return (App_Contract.ICurtains_Widget)InternalCurtains; } }
        private App_Contract.Curtains_Widget InternalCurtains { get; set; }

        public App_Contract.IScheduler_Widget Scheduler { get { return (App_Contract.IScheduler_Widget)InternalScheduler; } }
        private App_Contract.Scheduler_Widget InternalScheduler { get; set; }

        public App_Contract.IExhaustFan_Widget ExhaustFan { get { return (App_Contract.IExhaustFan_Widget)InternalExhaustFan; } }
        private App_Contract.ExhaustFan_Widget InternalExhaustFan { get; set; }

        public App_Contract.IIntercom_Widget Intercom { get { return (App_Contract.IIntercom_Widget)InternalIntercom; } }
        private App_Contract.Intercom_Widget InternalIntercom { get; set; }

        public App_Contract.IAccess_Widget Access { get { return (App_Contract.IAccess_Widget)InternalAccess; } }
        private App_Contract.Access_Widget InternalAccess { get; set; }

        public App_Contract.IAudio_Widget Audio { get { return (App_Contract.IAudio_Widget)InternalAudio; } }
        private App_Contract.Audio_Widget InternalAudio { get; set; }

        public App_Contract.IQuick_Actions QuickActions { get { return (App_Contract.IQuick_Actions)InternalQuickActions; } }
        private App_Contract.Quick_Actions InternalQuickActions { get; set; }

        public App_Contract.IVideo_Widget Video { get { return (App_Contract.IVideo_Widget)InternalVideo; } }
        private App_Contract.Video_Widget InternalVideo { get; set; }

        public App_Contract.IWeather_Widget Weather { get { return (App_Contract.IWeather_Widget)InternalWeather; } }
        private App_Contract.Weather_Widget InternalWeather { get; set; }

        public App_Contract.IMore_Page MorePage { get { return (App_Contract.IMore_Page)InternalMorePage; } }
        private App_Contract.More_Page InternalMorePage { get; set; }

        public App_Contract.IGlobalClimate_Widget GlobalClimate { get { return (App_Contract.IGlobalClimate_Widget)InternalGlobalClimate; } }
        private App_Contract.GlobalClimate_Widget InternalGlobalClimate { get; set; }

        public App_Contract.IGeneral_Widgets GeneralWidgets { get { return (App_Contract.IGeneral_Widgets)InternalGeneralWidgets; } }
        private App_Contract.General_Widgets InternalGeneralWidgets { get; set; }

        #endregion

        #region Construction and Initialization

        public Contract()
            : this(new List<BasicTriListWithSmartObject>().ToArray())
        {
        }

        public Contract(BasicTriListWithSmartObject device)
            : this(new [] { device })
        {
        }

        public Contract(BasicTriListWithSmartObject[] devices)
        {
            if (devices == null)
                throw new ArgumentNullException("Devices is null");

            ComponentMediator = new ComponentMediator();

            InternalFooter = new App_Contract.Footer(ComponentMediator, 1);
            InternalHomePage = new App_Contract.Home_Page(ComponentMediator, 2);
            InternalSuitesPageMaster = new App_Contract.SuitesPage_Master(ComponentMediator, 3);
            InternalRoomsPage = new App_Contract.Rooms_Page(ComponentMediator, 90);
            InternalLights = new App_Contract.Lights_Widget(ComponentMediator, 96);
            InternalClimate = new App_Contract.Climate_Widget(ComponentMediator, 121);
            InternalCurtains = new App_Contract.Curtains_Widget(ComponentMediator, 122);
            InternalScheduler = new App_Contract.Scheduler_Widget(ComponentMediator, 143);
            InternalExhaustFan = new App_Contract.ExhaustFan_Widget(ComponentMediator, 174);
            InternalIntercom = new App_Contract.Intercom_Widget(ComponentMediator, 175);
            InternalAccess = new App_Contract.Access_Widget(ComponentMediator, 203);
            InternalAudio = new App_Contract.Audio_Widget(ComponentMediator, 239);
            InternalQuickActions = new App_Contract.Quick_Actions(ComponentMediator, 272);
            InternalVideo = new App_Contract.Video_Widget(ComponentMediator, 279);
            InternalWeather = new App_Contract.Weather_Widget(ComponentMediator, 293);
            InternalMorePage = new App_Contract.More_Page(ComponentMediator, 294);
            InternalGlobalClimate = new App_Contract.GlobalClimate_Widget(ComponentMediator, 300);
            InternalGeneralWidgets = new App_Contract.General_Widgets(ComponentMediator, 428);

            for (int index = 0; index < devices.Length; index++)
            {
                AddDevice(devices[index]);
            }
        }

        public static void ClearDictionaries()
        {
            App_Contract.Scheduler_Widget.ClearDictionaries();
            App_Contract.Intercom_Widget.ClearDictionaries();
        }

        #endregion

        #region Standard Contract Members

        public object UserObject { get; set; }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            InternalFooter.AddDevice(device);
            InternalHomePage.AddDevice(device);
            InternalSuitesPageMaster.AddDevice(device);
            InternalRoomsPage.AddDevice(device);
            InternalLights.AddDevice(device);
            InternalClimate.AddDevice(device);
            InternalCurtains.AddDevice(device);
            InternalScheduler.AddDevice(device);
            InternalExhaustFan.AddDevice(device);
            InternalIntercom.AddDevice(device);
            InternalAccess.AddDevice(device);
            InternalAudio.AddDevice(device);
            InternalQuickActions.AddDevice(device);
            InternalVideo.AddDevice(device);
            InternalWeather.AddDevice(device);
            InternalMorePage.AddDevice(device);
            InternalGlobalClimate.AddDevice(device);
            InternalGeneralWidgets.AddDevice(device);
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            InternalFooter.RemoveDevice(device);
            InternalHomePage.RemoveDevice(device);
            InternalSuitesPageMaster.RemoveDevice(device);
            InternalRoomsPage.RemoveDevice(device);
            InternalLights.RemoveDevice(device);
            InternalClimate.RemoveDevice(device);
            InternalCurtains.RemoveDevice(device);
            InternalScheduler.RemoveDevice(device);
            InternalExhaustFan.RemoveDevice(device);
            InternalIntercom.RemoveDevice(device);
            InternalAccess.RemoveDevice(device);
            InternalAudio.RemoveDevice(device);
            InternalQuickActions.RemoveDevice(device);
            InternalVideo.RemoveDevice(device);
            InternalWeather.RemoveDevice(device);
            InternalMorePage.RemoveDevice(device);
            InternalGlobalClimate.RemoveDevice(device);
            InternalGeneralWidgets.RemoveDevice(device);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            InternalFooter.Dispose();
            InternalHomePage.Dispose();
            InternalSuitesPageMaster.Dispose();
            InternalRoomsPage.Dispose();
            InternalLights.Dispose();
            InternalClimate.Dispose();
            InternalCurtains.Dispose();
            InternalScheduler.Dispose();
            InternalExhaustFan.Dispose();
            InternalIntercom.Dispose();
            InternalAccess.Dispose();
            InternalAudio.Dispose();
            InternalQuickActions.Dispose();
            InternalVideo.Dispose();
            InternalWeather.Dispose();
            InternalMorePage.Dispose();
            InternalGlobalClimate.Dispose();
            InternalGeneralWidgets.Dispose();
            ComponentMediator.Dispose(); 
        }

        #endregion

    }
}
