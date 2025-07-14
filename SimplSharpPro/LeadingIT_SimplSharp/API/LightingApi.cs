
using Crestron.SimplSharp.Net;
using Crestron.SimplSharp.WebScripting;
using LeadingIT_SimplSharp.Logic;
using LeadingIT_SimplSharp.Models;
using System.Collections.Generic;

namespace LeadingIT_SimplSharp.API
{
    [RestController(Url = "/api/lights")]
    public class LightingApi : IRestController
    {
        private readonly LightingManager _manager;

        // Constructor with LightingManager
        public LightingApi(LightingManager manager)
        {
            _manager = manager;
        }

        [RestApiGet]
        public List<LightContract> GetLights()
        {
            return _manager.GetCurrentLights();
        }
    }
}
