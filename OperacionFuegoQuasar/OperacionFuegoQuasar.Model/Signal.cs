using System;
using System.Collections.Generic;
using System.Text;

namespace OperacionFuegoQuasar.Model
{
    public class Signal
    {
        /// <summary>
        /// Name og the Satelite
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Distance to Satellite
        /// </summary>
        public float Distance { get; set; }
        /// <summary>
        /// Message transmitted
        /// </summary>
        public string[] Message { get; set; }
    }
}
