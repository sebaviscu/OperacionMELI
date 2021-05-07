using OperacionFuegoQuasar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Common
{
    public class Request
    {
        public List<Signal> Satellites { get; set; }
    }
}
