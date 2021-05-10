using OperacionFuegoQuasar.Data;
using OperacionFuegoQuasar.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace OperacionFuegoQuasar.Logic
{
    public class GalaxyManager
    {
        private readonly ISatelitesRepository _satelitesRepository;
        private readonly ISignalRepository _signalRepository;
        private readonly ILogger<GalaxyManager> _logger;

        public GalaxyManager(ISatelitesRepository satelitesRepository, ISignalRepository signalRepository, ILogger<GalaxyManager> logger)
        {
            _satelitesRepository = satelitesRepository;
            _signalRepository = signalRepository;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signals">Distancia al emisor tal cual se recibe en cada satélite</param>
        /// <returns>Coordenadas ‘x’ e ‘y’ del emisor del mensaje</returns>
        public Coordinates GetLocation(List<Signal> signals)
        {
            // https://es.wikipedia.org/wiki/Trilateracion

            try
            {
                if (signals.Count != 3 || signals.Any(_ => string.IsNullOrEmpty(_.Name)))
                    return null;

                var satelite1 = _satelitesRepository.GetByName(signals[0].Name);
                var satelite2 = _satelitesRepository.GetByName(signals[1].Name);
                var satelite3 = _satelitesRepository.GetByName(signals[2].Name);

                if (satelite1 == null || satelite2 == null || satelite3 == null)
                    return null;

                var dist1 = signals[0].Distance;
                var dist2 = signals[1].Distance;
                var dist3 = signals[2].Distance;

                var d = Math.Abs(satelite2.Coordinates.X - satelite1.Coordinates.X);
                var i = Math.Abs(satelite3.Coordinates.X - satelite1.Coordinates.X);
                var j = Math.Abs(satelite3.Coordinates.Y - satelite1.Coordinates.Y);

                var x = (Math.Pow(dist1, 2) - Math.Pow(dist2, 2) + Math.Pow(d, 2))
                        / (2 * d);

                var y = ((Math.Pow(dist1, 2) - Math.Pow(dist3, 2) - Math.Pow(x, 2) + Math.Pow(x - i, 2) + Math.Pow(j, 2))
                        / (2 * j));

                x += satelite1.Coordinates.X;
                y += satelite1.Coordinates.Y;

                return new Coordinates(x, y);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"An error occurred when trying to retrieve the Coordinates: {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages">mensajes tal cual es recibido en cada satélite</param>
        /// <returns>Mensaje tal cual lo genera el emisor del mensaje</returns>
        public string GetMessage(List<string[]> messages)
        {
            try
            {
                if (!messages.Any())
                    return null;

                var finalMessage = new List<string>();

                for (int i = 0; i < messages.Max(_ => _.Length); i++)
                {
                    foreach (var m in messages)
                    {
                        if (!string.IsNullOrEmpty(m[i]))
                        {
                            finalMessage.Add(m[i]);
                            break;
                        }
                        else if (messages.Last() == m)
                            return null;
                    }
                }

                return string.Join(" ", finalMessage);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"An error occurred when trying to retrieve the Message: {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signal">Signs to be saved</param>
        public Signal SaveSignal(Signal signal)
        {
            var satelite = _satelitesRepository.GetByName(signal.Name);
            if (satelite == null) return null;

            var signalList = _signalRepository.Get(signal.Name);

            if (signalList != null && signalList.Any())
                _signalRepository.Replace(signal);
            else
                _signalRepository.Add(signal);

            return signal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns a list of three signals</returns>
        public List<Signal> GetSignalsForMessage()
        {
            var treeSignals = _signalRepository.GetAll();

            if (treeSignals == null || treeSignals.Count < 3)
                return null;

            return treeSignals.Take(3).ToList();
        }

        /// <summary>
        /// Clear the signal list
        /// </summary>
        public void ClearSignals()
        {
            _signalRepository.Clear();
        }
    }
}
