using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilosofersDinnigProblem
{
    class Circle
    {
        public static readonly Color EAT = Color.Green;
        public static readonly Color THINK = Color.Blue;
        public static readonly Color WAIT = Color.Red;
        public static readonly int RADIUS = 30;
        public Point center { get; set; }

        public State state { get; set; }

        public Color color { get; set; }
        public Circle(Point c,State state)
        {
            center = c;
            this.state = state;
            setTheColor();
        }

        void setTheColor()
        {
            if (state == State.eating)
                color = EAT;
            else if (state == State.thinking)
                color = THINK;
            else
                color = WAIT;
        }

        public void draw(Graphics g)
        {
            setTheColor();
            SolidBrush brush = new SolidBrush(color);
            g.FillEllipse(brush, center.X - RADIUS, center.Y - RADIUS, 2 * RADIUS, 2 * RADIUS);
            brush.Dispose();
        }
    }
}
