using System;
using System.Collections.Generic;
using System.Text;

namespace OperacionFuegoQuasar.Model
{
    public class Satelite
    {
        /// <summary>
        /// Name of Satelite
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Coordinates of Satelite
        /// </summary>
        public Coordinates Coordinates { get; set; }
    }
}
