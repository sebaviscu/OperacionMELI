using OperacionFuegoQuasar.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OperacionFuegoQuasar.Data
{
    public interface ISignalRepository
    {
        public List<Signal> GetAll();

        public void Add(Signal signal);

        public void Replace(Signal signal);

        public List<Signal> Get(string sateliteName);

        public void Clear();
    }
}
