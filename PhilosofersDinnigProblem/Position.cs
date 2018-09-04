using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilosofersDinnigProblem
{
    public class Position
    {
        public Point point1 { get; set; }
        public Point point2 { get; set; }

        public Position(Point p1,Point p2)
        {
            point1 = p1;
            point2 = p2;
        }
    }
}
