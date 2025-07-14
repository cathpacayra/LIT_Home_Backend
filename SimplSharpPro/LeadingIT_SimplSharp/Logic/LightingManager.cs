using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.DeviceSupport;
using LeadingIT_SimplSharp.Models;
using System.Collections.Generic;

namespace LeadingIT_SimplSharp.Logic
{
    public class LightingManager
    {
        private BasicTriList _panel;
        private ushort _moduleCount = 0;
        private List<LightContract> _lights = new List<LightContract>();

        public LightingManager(BasicTriList panel)
        {
            _panel = panel;
            _panel.SigChange += Panel_SigChange;
        }

        private void Panel_SigChange(BasicTriList sender, SigEventArgs args)
        {
            if (args.Sig.Type == eSigType.UShort && args.Sig.Number == JoinMap.LightModuleCount)
            {
                _moduleCount = args.Sig.UShortValue;
                UpdateLights();
            }
            else if (args.Sig.Type == eSigType.Bool)
            {
                ushort join = (ushort)args.Sig.Number;
                // Update existing light state if join matches one in the list
                for (int idx = 0; idx < _lights.Count; idx++)
                {
                    if (_lights[idx].OnJoin == join)
                        _lights[idx].IsOn = args.Sig.BoolValue;
                }
            }
        }

        private void UpdateLights()
        {
            _lights.Clear();
            for (ushort i = 0; i < _moduleCount; i++)
            {
                ushort join = (ushort)(JoinMap.LightOnStartJoin + i);
                bool isOn = _panel.BooleanInput[join].BoolValue;
                _lights.Add(new LightContract
                {
                    Id = i + 1,
                    Name = $"Light {i + 1}",
                    OnJoin = join,
                    IsOn = isOn
                });
            }
        }

        public List<LightContract> GetCurrentLights()
        {
            return _lights;
        }
    }

}
