using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhilosofersDinnigProblem
{
    public partial class Form1 : Form
    {
        DinnigTable dinnigTable;
        int state;
       
        Timer timer;
        
        public Form1()
        {
            state = 1;
            dinnigTable = new DinnigTable(state);
            InitializeComponent();
            this.DoubleBuffered = true;
            timer = this.timer1;
            //this.BackgroundImage = new Bitmap((@"C:\Users\Laze\Desktop\Philosofers.jpg"));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dinnigTable.startEating();
            timer.Enabled = true;
            timer.Start();
        }

        void thick(object sender, EventArgs e)
        {
            Status.Text = dinnigTable.state();
        }

        private void next_Click(object sender, EventArgs e)
        {
            Status.Text = dinnigTable.state();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Status.Text = dinnigTable.state();
            Invalidate(true);
            
        }

      

       

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Properties.Resources.table, 115, 115);
            //e.Graphics.fi
            e.Graphics.DrawLine(new Pen(Color.Black), new Point(20, 10), new Point(250, 250));
            dinnigTable.draw(e.Graphics);
           
        }

        private void noDeadlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deadlockToolStripMenuItem.Checked = false;
            if (state != 1)
            {
                timer.Stop();
                dinnigTable = new DinnigTable(1);
                timer.Start();
            }
        }

        private void deadlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noDeadlockToolStripMenuItem.Checked = false;
            if (state != 2)
            {
                timer.Stop();
                state = 2;
                dinnigTable = new DinnigTable(state);
                timer.Start();
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            if (Stop.Text == "Stop")
            {
                timer1.Stop();
                Stop.Text = "Resume";
            }
            else
            {
                timer1.Start();
                Stop.Text = "Stop";

            }
        }
    }
}
