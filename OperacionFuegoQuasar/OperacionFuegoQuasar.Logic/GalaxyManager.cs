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
        private readonly SatelitesRepository _repository;

        public GalaxyManager(SatelitesRepository repository)
        {
            _repository = repository;
        }

        public Coordinates GetLocation(List<Signal> signals)
        {
            //var satelite1 = _repository.GetByName(signals[0].Name);
            //var satelite2 = _repository.GetByName(signals[1].Name);
            //var satelite3 = _repository.GetByName(signals[2].Name);

            //var dist1 = signals[0].Distance;
            //var dist2 = signals[1].Distance;
            //var dist3 = signals[2].Distance;

            //var d = Math.Abs(satelite2.Coordinates.X - satelite1.Coordinates.X);
            //var i = Math.Abs(satelite3.Coordinates.X - satelite1.Coordinates.X);
            //var j = Math.Abs(satelite3.Coordinates.Y - satelite1.Coordinates.Y);

            //var x = (Math.Pow(dist1, 2) - Math.Pow(dist2, 2) + Math.Pow(d, 2)) / (2 * d);
            //var y = (Math.Pow(dist1, 2) - Math.Pow(dist3, 2) - Math.Pow(x, 2) + Math.Pow(x - i, 2) + Math.Pow(j, 2)) / (2 * j);
            //x += satelite1.Coordinates.X;
            //y += satelite1.Coordinates.Y;

            return new Coordinates(0, 0);
        }

        public string GetMessage(List<string[]> messages)
        {
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
                        throw new Exception("Index is out of range");
                                    }
            }

            return string.Join(" ", finalMessage);
        }
    }
}
