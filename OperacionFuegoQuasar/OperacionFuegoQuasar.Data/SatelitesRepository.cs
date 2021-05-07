using OperacionFuegoQuasar.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OperacionFuegoQuasar.Data
{
    public class SatelitesRepository : ISatelitesRepository
    {
        readonly private List<Satelite> _repository;
        public SatelitesRepository()
        {
            _repository = new List<Satelite>();

            _repository.Add(new Satelite() { Name = "Kenobi", Coordinates = new Coordinates(-500, -200) });
            _repository.Add(new Satelite() { Name = "Skywalker", Coordinates = new Coordinates(100, -100) });
            _repository.Add(new Satelite() { Name = "Sato", Coordinates = new Coordinates(500, 100) });
        }

        public Satelite GetByName(string name)
        {
            return _repository.FirstOrDefault(_=>_.Name == name);
        }

    }
}
