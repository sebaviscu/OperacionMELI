using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperacionFuegoQuasar.Logic;
using OperacionFuegoQuasar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlianzaRebeldeController : ControllerBase
    {
        private GalaxyManager _galaxyManager;

        public AlianzaRebeldeController(GalaxyManager galaxyManager)
        {
            _galaxyManager = galaxyManager;
        }

        [HttpPost]
        [Route("topsecret/")]
        public IActionResult GetLocation([FromBody]List<Signal> signals) 
        {
            var position = _galaxyManager.GetLocation(signals);
            var message = _galaxyManager.GetMessage(signals.Select(_ => _.Message).ToList());

            return Ok(new { position, message });
        }
    }
}
