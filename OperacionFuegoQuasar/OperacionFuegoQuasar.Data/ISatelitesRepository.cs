using OperacionFuegoQuasar.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OperacionFuegoQuasar.Data
{
    public interface ISatelitesRepository
    {
        public Satelite GetByName(string name);

    }
}
