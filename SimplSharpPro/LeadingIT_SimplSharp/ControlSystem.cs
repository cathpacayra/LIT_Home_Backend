using App_Contract;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.CrestronThread;
using Crestron.SimplSharpPro.UI;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace LeadingIT_SimplSharp
{
    public class ControlSystem : CrestronControlSystem
    {
        private Contract uiContract;
        private XpanelForSmartGraphics xpanel;
        private XpanelForSmartGraphics xpanel1;
        private WebSocketServer wsServer;
        private List<string> clickedWidgets = new List<string>();
        private readonly string clickedWidgetsPath = @"\NVRAM\clicked_widgets.json";

        public ControlSystem() : base()
        {
            try
            {
                Thread.MaxNumberOfUserThreads = 20;

                CrestronEnvironment.SystemEventHandler += _ControllerSystemEventHandler;
                CrestronEnvironment.ProgramStatusEventHandler += _ControllerProgramEventHandler;
                CrestronEnvironment.EthernetEventHandler += _ControllerEthernetEventHandler;
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in constructor: {0}", e.Message);
            }
        }

        public override void InitializeSystem()
        {
            try
            {
                uiContract = new Contract();

                xpanel = new XpanelForSmartGraphics(0x15, this);
                xpanel1 = new XpanelForSmartGraphics(0x16, this);

                xpanel.OnlineStatusChange += HandlePanelOnlineStatusChange;
                xpanel1.OnlineStatusChange += HandlePanelOnlineStatusChange;

                if (xpanel.Register() == eDeviceRegistrationUnRegistrationResponse.Success)
                {
                    uiContract.AddDevice(xpanel);
                    CrestronConsole.PrintLine($"xpanel 0x15 registered");
                }

                if (xpanel1.Register() == eDeviceRegistrationUnRegistrationResponse.Success)
                {
                    uiContract.AddDevice(xpanel1);
                    CrestronConsole.PrintLine($"xpanel 0x16 registered");
                }

                SetActivePage();
                uiContract.HomePage.QA_3_Trigger += HomePage_QA_3_Trigger;
                LoadClickedWidgets();

                CrestronConsole.PrintLine("System initialized.");
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in InitializeSystem: {0}", e.Message);
            }
        }

        private void SetActivePage()
        {
            uiContract.Footer.UI_BootUp += Footer_UI_BootUp;
            uiContract.Footer.HomePage_Select += Footer_HomePage_IsSelected;
            uiContract.Footer.RoomsPage_Select += Footer_RoomsPage_IsSelected;
            uiContract.Footer.MorePage_Select += Footer_MorePage_IsSelected;
        }

        private void Footer_UI_BootUp(object sender, UIEventArgs e)
        {
            SelectHomePage();
            AddWidgetClick($"{DateTime.Now}: ClickEvent: footer boot up");
        }

        private void Footer_HomePage_IsSelected(object sender, UIEventArgs e)
        {
            DeselectRoomsPage();
            DeselectMorePage();
            SelectHomePage();
        }

        private void Footer_RoomsPage_IsSelected(object sender, UIEventArgs e)
        {
            DeselectHomePage();
            DeselectMorePage();
            SelectRoomsPage();
        }

        private void Footer_MorePage_IsSelected(object sender, UIEventArgs e)
        {
            DeselectHomePage();
            DeselectRoomsPage();
            SelectMorePage();
        }

       
        private void AddWidgetClick(string widgetName)
        {
            if (!clickedWidgets.Contains(widgetName))
            {
                clickedWidgets.Add(widgetName);
                SaveClickedWidgets();
            }
        }

        private void SaveClickedWidgets()
        {
            try
            {
                var json = JsonConvert.SerializeObject(clickedWidgets, Formatting.Indented);
                File.WriteAllText(clickedWidgetsPath, json);
            }
            catch (Exception ex)
            {
                ErrorLog.Error("Error saving clicked widgets: " + ex.Message);
            }
        }

        private void LoadClickedWidgets()
        {
            try
            {
                if (File.Exists(clickedWidgetsPath))
                {
                    var json = File.ReadAllText(clickedWidgetsPath);
                    clickedWidgets = JsonConvert.DeserializeObject<List<string>>(json);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error("Error loading clicked widgets: " + ex.Message);
            }
        }
        private void PrintClickedWidgets()
        {
            try
            {
                string json = File.ReadAllText(clickedWidgetsPath);
                CrestronConsole.PrintLine(json);
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("Error reading clicked_widgets.json: " + ex.Message);
            }
        }

        private void SelectHomePage()
        {
            uiContract.Footer.HomePage_IsSelected((sig, _) => sig.BoolValue = true);
            uiContract.Footer.MainPage_IsSelected((sig, _) => sig.BoolValue = true);
            HomePage_Settings();
            AddWidgetClick($"{DateTime.Now}: ClickEvent: Home Page is Selected");
        }


        private void DeselectHomePage()
        {
            uiContract.Footer.HomePage_IsSelected((sig, _) => sig.BoolValue = false);

            uiContract.HomePage.Show_AccessWidget((sig, _) => sig.BoolValue = false);
            uiContract.HomePage.Show_GlobalClimateWidget((sig, _) => sig.BoolValue = false);
            uiContract.HomePage.Show_GlobalControls((sig, _) => sig.BoolValue = false);
            uiContract.HomePage.Show_GlobalSchedulerWidget((sig, _) => sig.BoolValue = false);
            uiContract.HomePage.Show_IntercomWidget((sig, _) => sig.BoolValue = false);
            uiContract.HomePage.Show_QuickActions((sig, _) => sig.BoolValue = false);
            uiContract.HomePage.Show_Time((sig, _) => sig.BoolValue = false);
            uiContract.HomePage.Show_WeatherWidget((sig, _) => sig.BoolValue = false);

            AddWidgetClick($"{DateTime.Now}: ClickEvent: Deselect Homne page");
        }

        private void SelectRoomsPage()
        {
            uiContract.Footer.RoomsPage_IsSelected((sig, _) => sig.BoolValue = true);
            RoomPage_Settings();


            AddWidgetClick($"{DateTime.Now}: ClickEvent: Select room page");
        }

        private void DeselectRoomsPage()
        {
            uiContract.Footer.RoomsPage_IsSelected((sig, _) => sig.BoolValue = false);

            uiContract.RoomsPage.Show_RoomsPage((sig, _) => sig.BoolValue = false);
            uiContract.RoomsPage.Show_RoomsSelection((sig, _) => sig.BoolValue = false);
            uiContract.RoomsPage.Show_LightsWidget((sig, _) => sig.BoolValue = false);
            uiContract.RoomsPage.Show_ClimateWidget((sig, _) => sig.BoolValue = false);
            uiContract.RoomsPage.Show_CurtainsWidget((sig, _) => sig.BoolValue = false);
            uiContract.RoomsPage.Show_AudioWidget((sig, _) => sig.BoolValue = false);
            uiContract.RoomsPage.Show_VideoWidget((sig, _) => sig.BoolValue = false);
            uiContract.RoomsPage.Show_CinemaWidget((sig, _) => sig.BoolValue = false);
            uiContract.RoomsPage.Show_SchedulerWidget((sig, _) => sig.BoolValue = false);
            uiContract.RoomsPage.Show_ExhaustFanWidget((sig, _) => sig.BoolValue = false);

            AddWidgetClick($"{DateTime.Now}: ClickEvent: Deselect room page");
        }

        private void SelectMorePage()
        {
            uiContract.Footer.MorePage_IsSelected((sig, _) => sig.BoolValue = true);
            MorePage_Settings();
            AddWidgetClick($"{DateTime.Now}: ClickEvent: Select more page");
        }

        private void DeselectMorePage()
        {
            uiContract.Footer.MorePage_IsSelected((sig, _) => sig.BoolValue = false);

            uiContract.MorePage.Show_Menu((sig, _) => sig.BoolValue = false);
            uiContract.MorePage.Show_Scr_Brightness((sig, _) => sig.BoolValue = false);
            uiContract.MorePage.Show_Status((sig, _) => sig.BoolValue = false);
            uiContract.MorePage.AutoTheme_Enable_FB((sig, _) => sig.BoolValue = false);
            uiContract.MorePage.Show_KeypadsBacklight((sig, _) => sig.BoolValue = false);
            uiContract.MorePage.Show_FullScreenMode_Btn((sig, _) => sig.BoolValue = false);

            AddWidgetClick($"{DateTime.Now}: ClickEvent: Deselect more page");
        }

        private void HomePage_QA_3_Trigger(object sender, UIEventArgs e)
        {
            uiContract.HomePage.QA_3_Trigger_FB((sig, _) => sig.BoolValue = true);
            AddWidgetClick($"{DateTime.Now}: ClickEvent: QA_3_Trigger");
        }

        private void HomePage_Settings()
        {
            uiContract.HomePage.SuiteName((sig, _) => sig.StringValue = "Presidential Suite");
            uiContract.HomePage.Time((sig, _) => sig.StringValue = DateTime.Now.ToString("hh:mm tt"));
            uiContract.HomePage.Date((sig, _) => sig.StringValue = DateTime.Now.ToString("ddd MMM dd"));

            uiContract.HomePage.Show_AccessWidget((sig, _) => sig.BoolValue = true);
            uiContract.HomePage.Show_GlobalClimateWidget((sig, _) => sig.BoolValue = true);
            uiContract.HomePage.Show_GlobalControls((sig, _) => sig.BoolValue = true);
            uiContract.HomePage.Show_GlobalSchedulerWidget((sig, _) => sig.BoolValue = true);
            uiContract.HomePage.Show_IntercomWidget((sig, _) => sig.BoolValue = true);
            uiContract.HomePage.Show_QuickActions((sig, _) => sig.BoolValue = true);
            uiContract.HomePage.Show_Time((sig, _) => sig.BoolValue = true);
            uiContract.HomePage.Show_WeatherWidget((sig, _) => sig.BoolValue = true);
        }

        private void RoomPage_Settings()
        {
            uiContract.RoomsPage.Show_RoomsPage((sig, _) => sig.BoolValue = true);
            uiContract.RoomsPage.Show_RoomsSelection((sig, _) => sig.BoolValue = true);
            uiContract.RoomsPage.Show_LightsWidget((sig, _) => sig.BoolValue = true);
            uiContract.RoomsPage.Show_ClimateWidget((sig, _) => sig.BoolValue = true);
            uiContract.RoomsPage.Show_CurtainsWidget((sig, _) => sig.BoolValue = true);
            uiContract.RoomsPage.Show_AudioWidget((sig, _) => sig.BoolValue = true);
            uiContract.RoomsPage.Show_VideoWidget((sig, _) => sig.BoolValue = true);
            uiContract.RoomsPage.Show_CinemaWidget((sig, _) => sig.BoolValue = true);
            uiContract.RoomsPage.Show_SchedulerWidget((sig, _) => sig.BoolValue = true);
            uiContract.RoomsPage.Show_ExhaustFanWidget((sig, _) => sig.BoolValue = true);
        }

        private void MorePage_Settings()
        {
            uiContract.MorePage.Show_Scr_Brightness((sig, _) => sig.BoolValue = true);
            uiContract.MorePage.Show_Menu((sig, _) => sig.BoolValue = true);
            uiContract.MorePage.AutoTheme_Enable_FB((sig, _) => sig.BoolValue = true);
            uiContract.MorePage.Show_Status((sig, _) => sig.BoolValue = true);
            uiContract.MorePage.Show_KeypadsBacklight((sig, _) => sig.BoolValue = true);
            uiContract.MorePage.Show_FullScreenMode_Btn((sig, _) => sig.BoolValue = true);
        }

        private void HandlePanelOnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            var state = args.DeviceOnLine ? "Online" : "Offline";
            ErrorLog.Notice($"xPanel {currentDevice.ID} is {state}");
        }


        void _ControllerEthernetEventHandler(EthernetEventArgs ethernetEventArgs)
        {
            switch (ethernetEventArgs.EthernetEventType)
            {//Determine the event type Link Up or Link Down
                case (eEthernetEventType.LinkDown):
                    //Next need to determine which adapter the event is for. 
                    //LAN is the adapter is the port connected to external networks.
                    if (ethernetEventArgs.EthernetAdapter == EthernetAdapterType.EthernetLANAdapter)
                    {
                        //
                    }
                    break;
                case (eEthernetEventType.LinkUp):
                    if (ethernetEventArgs.EthernetAdapter == EthernetAdapterType.EthernetLANAdapter)
                    {

                    }
                    break;
            }
        }

        void _ControllerProgramEventHandler(eProgramStatusEventType programStatusEventType)
        {
            switch (programStatusEventType)
            {
                case (eProgramStatusEventType.Paused):
                    //The program has been paused.  Pause all user threads/timers as needed.
                    break;
                case (eProgramStatusEventType.Resumed):
                    //The program has been resumed. Resume all the user threads/timers as needed.
                    break;
                case (eProgramStatusEventType.Stopping):
                    if (wsServer != null)
                    {
                        wsServer.Stop();
                        wsServer = null;
                        CrestronConsole.PrintLine("[WS] WebSocket server stopped.");
                    }
                    break;
            }
        }

        void _ControllerSystemEventHandler(eSystemEventType systemEventType)
        {
            switch (systemEventType)
            {
                case (eSystemEventType.DiskInserted):
                    //Removable media was detected on the system
                    break;
                case (eSystemEventType.DiskRemoved):
                    //Removable media was detached from the system
                    break;
                case (eSystemEventType.Rebooting):
                    //The system is rebooting. 
                    //Very limited time to preform clean up and save any settings to disk.
                    break;
            }
        }
    }
}
