using OperacionFuegoQuasar.Data;
using OperacionFuegoQuasar.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OperacionFuegoQuasar.Logic
{
    public class GalaxyManager
    {
        private readonly ISatelitesRepository _satelitesRepository;
        private readonly ISignalRepository _signalRepository;

        public GalaxyManager(ISatelitesRepository satelitesRepository, ISignalRepository signalRepository)
        {
            _satelitesRepository = satelitesRepository;
            _signalRepository = signalRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signals">Distancia al emisor tal cual se recibe en cada satélite</param>
        /// <returns>Coordenadas ‘x’ e ‘y’ del emisor del mensaje</returns>
        public Coordinates GetLocation(List<Signal> signals)
        {
            if (signals.Count < 3 && signals.All(_ => !string.IsNullOrEmpty(_.Name)))
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

            var y = (Math.Pow(dist1, 2) - Math.Pow(dist3, 2) - Math.Pow(x, 2) + Math.Pow(x - i, 2) + Math.Pow(j, 2))
                    / (2 * j);

            x += satelite1.Coordinates.X;
            y += satelite1.Coordinates.Y;

            return new Coordinates(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages">mensajes tal cual es recibido en cada satélite</param>
        /// <returns>Mensaje tal cual lo genera el emisor del mensaje</returns>
        public string GetMessage(List<string[]> messages)
        {
            if (!messages.Any())
                return null;

            var finalMessage = new List<string>();
            var maxLength = messages.Max(_ => _.Length);

            for (int i = 0; i < maxLength; i++)
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

            if (finalMessage.Count < maxLength)
                return null;

            return string.Join(" ", finalMessage);
        }

        public void SaveSignal(Signal signal)
        {
            var signalList = _signalRepository.Get(signal.Name);

            if (signalList.Any())
                _signalRepository.Replace(signal);
            else
                _signalRepository.Add(signal);
        }

        public List<Signal> GetSignalsForMessage()
        {
            return _signalRepository.GetAll().Take(3).ToList();
        }

        public void ClearSignals()
        {
            _signalRepository.Clear();
        }
    }
}
