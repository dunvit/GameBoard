using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardExample
{
    public class CelestialObject
    {
        public float Direction { get; set; } // Direction from 0 to 360 degrees. 0 - North,  90 - East, 180 - South, 270 - West

        public float Speed { get; set; } // Pixels in second

        public float Agility { get; set; } // Pixels in second

        public PointF Location { get; set; }
    }
}
