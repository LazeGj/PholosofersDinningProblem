using PhilosofersDinnigProblem;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class Fork
{
    public Semaphore semaphore { get; set; }
    public bool[] forks { get; set; }

    public static ForkForDrawing[] forkForDrawing ={new ForkForDrawing(new Point(10,10), new Point(20,20),new Point(30,30), new Point(40,40))
    ,new ForkForDrawing(new Point(), new Point(),new Point(), new Point())
    ,new ForkForDrawing(new Point(), new Point(),new Point(), new Point())
     ,new ForkForDrawing(new Point(), new Point(),new Point(), new Point())
    ,new ForkForDrawing(new Point(), new Point(),new Point(), new Point())
    };
    public Fork()
    {
        forks = new bool[5];
        for (int i = 0; i < forks.Length; i++)
        {
            forks[i] = true;
        }
        semaphore = new Semaphore(1, 1);
    }
}