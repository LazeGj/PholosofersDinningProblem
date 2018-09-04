using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilosofersDinnigProblem
{
    public class ForkForDrawing
    {
        public int State { get; set; }
        public Position position1 { get; set; }
        public Position position2 { get; set; }
        public ForkForDrawing(Point one, Point two,Point three, Point four)
        {
            State = 0;
            position1 = new Position(one, two);
            position2 = new Position(three, four);
        }

        public void draw(Graphics g)
        {
            Position temp = null;
            if (State == 0)
                temp = position1;
            else
                temp = position2;

            Pen pen = new Pen(Color.Gray);
            g.DrawLine(pen, temp.point1, temp.point2);
            pen.Dispose();
        }

        public void changeStateUSING()
        {
            if (State == 1)
                return;
            State = 1;
        }

        public void changeStateFREE()
        {
            if (State == 0)
                return;
            State = 0;
        }
    }
}
