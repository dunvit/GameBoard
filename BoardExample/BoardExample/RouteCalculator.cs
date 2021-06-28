using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardExample
{
    public class RouteCalculator
    {
        public List<PointF> Execute(CelestialObject objectA, CelestialObject objectB, double orbitRadius)
        {
            var result = new List<PointF>
            {
                new PointF(300, 300),
                new PointF(305, 305),
                new PointF(307, 307),
                new PointF(308, 308),
                new PointF(312, 312),
                new PointF(316, 316),
                new PointF(320, 320),
            };

            return result;

        }
    }
}
