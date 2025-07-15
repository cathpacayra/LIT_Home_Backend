/*******************************************************************************************
  (NOTE: demo purposes only!) SIMPL# Pro Module Information
*******************************************************************************************/
/*
Dealer Name: Leading IT Middle East
System Name:
System Number:
Programmer: Namu Go
Comments: This is a Proof-Of-Concept (POC) for converting a SIMPL+ module to SIMPL# Pro.
          It is not intended for production use and may not include all features of the original.
          The code is based on "LITHome v4 Lights Channel v1.usp".
*/


using System;
using Crestron.SimplSharp;

namespace LITHomeLibrary
{
    public class LightEventArgs : EventArgs
    {
        // public bool BoolData = false;  // try implementing after preliminary test is successful
        public int IntData = 0;
        public string StringData = "";

        // public Action ActionData;

        public LightEventArgs() { } //must exist for SIMPL+ compliance
        // public LightEventArgs(bool data) { BoolData = data; }
        public LightEventArgs(int data) { IntData = data; }
        public LightEventArgs(string data) { StringData = data; }
        // public LightEventArgs(Action data) { ActionData = data; }
    }

    public class LightsChannelV1
    {
        // In each class method, choose appropriate EventHandler depending on how you want the LightEventArgs to behave toward its input argument
        // public event EventHandler BooleanEvent;
        // public event EventHandler IntegerEvent;
        // public event EventHandler StringEvent;
        public event EventHandler<LightEventArgs> BooleanEvent;

        public event EventHandler<LightEventArgs> IntegerEvent;
        public event EventHandler<LightEventArgs> StringEvent;

        public void TogglePressed(ushort Is_On_value)
        {
            CrestronConsole.PrintLine("LightsChannelV1.TogglePressed");
            try
            {
                if (Is_On_value == 0)
                {
                    if (BooleanEvent != null)
                    {
                        BooleanEvent(this, new LightEventArgs(1));
                    }
                }
                else
                {
                    if (BooleanEvent != null)
                    {
                        BooleanEvent(this, new LightEventArgs(0));
                    }
                }
            }
            /* 
            the try statement above could be simplified into:

            try
            {
                if (Is_On_value == 0)
                {
                    BooleanEvent?.Invoke(this, new LightEventArgs(1));
                }
                else
                {
                    BooleanEvent?.Invoke(this, new LightEventArgs(0));
                }
            }

            */
            catch (Exception ex)
            {
                ErrorLog.Error("TogglePressed Error: {0}", ex.Message);
            }
        }

        public void Channel_Level_Set(ushort value)
        {
            CrestronConsole.PrintLine("LightsChannelV1.Channel_Level_Set {0}", value);
            if (IntegerEvent != null)
            {
                IntegerEvent(this, new LightEventArgs(value));
            }
        }



        // Constructor
        public LightsChannelV1() { }
    }
}



/*
Cathy's code example:

public void OnSetOffPressed(ushort value)
  {
      try
      {
          if (value == 1)
          {
              _currentChannelLevel = 0;
              if (Is_Off != null) Is_Off();
          }
      }
      catch (Exception ex)
      {
          ErrorLog.Error("OnSetOffPressed Error: {0}", ex.Message);
      }
  }


*/
