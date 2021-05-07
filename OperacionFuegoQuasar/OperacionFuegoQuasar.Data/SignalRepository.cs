using OperacionFuegoQuasar.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OperacionFuegoQuasar.Data
{
    public class SignalRepository : ISignalRepository
    {
        private List<Signal> _repository = new List<Signal>();

        public List<Signal> GetAll()
        {
            return _repository;
        }

        public void Add(Signal signal)
        {
            _repository.Add(signal);
        }

        public void Replace(Signal signal)
        {
            var oldSignal = Get(signal.Name).First();
            oldSignal.Message = signal.Message;
            oldSignal.Distance = signal.Distance;
        }

        public List<Signal> Get(string sateliteName)
        {
            return _repository.Where(_ => _.Name == sateliteName).ToList();
        }

        public void Clear()
        {
            _repository.Clear();
        }
    }
}
