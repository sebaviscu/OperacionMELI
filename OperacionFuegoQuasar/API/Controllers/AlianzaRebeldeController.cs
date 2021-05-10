using API.Common;
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
        public ActionResult GetLocationAndMessage([FromBody]Request request) 
        {
            if (request == null || request.Satellites != null)
                return NotFound();

            var position = _galaxyManager.GetLocation(request.Satellites);
            var message = _galaxyManager.GetMessage(request.Satellites.Select(_ => _.Message).ToList());

            if (position == null || message == null)
                return NotFound();

            return Ok(new { position, message });
        }

        [HttpPost]
        [Route("topsecret_split/{satelliteName}")]
        public ActionResult SaveSignal([FromBody] Signal signal, string satelliteName)
        {
            if (signal == null || string.IsNullOrEmpty(satelliteName))
                return NotFound();

            signal.Name = satelliteName;

            _galaxyManager.SaveSignal(signal);

            return Ok();
        } 

        [HttpGet]
        [Route("topsecret_split/")]
        public ActionResult GetLocationAndMessageBySavedSignals()
        {
            var signals = _galaxyManager.GetSignalsForMessage();

            if (signals == null)
                return NotFound();

            var position = _galaxyManager.GetLocation(signals);
            var message = _galaxyManager.GetMessage(signals.Select(_ => _.Message).ToList());

            if (position == null || message == null)
                return NotFound();

            _galaxyManager.ClearSignals();

            return Ok(new { position, message });
        }
    }
}
