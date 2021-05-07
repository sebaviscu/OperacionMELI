using System;
using System.Collections.Generic;
using System.Text;

namespace OperacionFuegoQuasar.Model
{
    public class Coordinates
    {
        public Coordinates(double x, double y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Coordinate X
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Coordinate Y
        /// </summary>
        public double Y { get; set; }

        public override bool Equals(object obj)
        {
            return (obj as Coordinates).X == X && (obj as Coordinates).Y == Y;
        }
    }
}
