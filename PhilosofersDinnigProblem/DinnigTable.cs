using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhilosofersDinnigProblem
{
    enum State
    {
        eating,
        thinking,
        waiting
    }
    class DinnigTable
    {
        public int StateMode { get; set; }
        public Philisofers[] philisofers { get; set; }

        public static Point[] coordinates = { new Point(240,85), new Point(84,233), new Point(160, 380), new Point(389, 350), new Point(409, 181) };

        
        Fork forksAndSemaphore { get; set; }
        
        public class Philisofers
        {
            public static Fork forksAndSema;
            public int Mode { get; set; }
            public State state { get; set; }

            public Circle circle { get; set; }
            public Thread thread { get; set; }
            public int position { get; set; }
            public Philisofers(int index,Point center,int mode,Fork forkAndS)             
            {
                forksAndSema = forkAndS;
                Mode = mode;
                state = State.thinking;                
                position = index;
                circle = new Circle(center,state);
                thread = new Thread(new ParameterizedThreadStart(this.start));
            }


            void changeState(State newState)
            {
                state = newState;
                circle.state = newState;
            }

            void eat(Fork forksAndSema)
            {
                //manupilating with the state of the forksForDrawing
                bool startedEating=false;
                forksAndSema.semaphore.WaitOne();//ensuring that no one else will check or get the forks while he is doing that
               

                if (forksAndSema.forks[position] && forksAndSema.forks[(position+1)%5])//are the left fork and right fork free
                {
                    changeState(State.eating);
                    startedEating = true;
                    forksAndSema.forks[position] = false;
                    forksAndSema.forks[(position + 1) % 5] = false;
                    Fork.forkForDrawing[position].changeStateUSING();
                    Fork.forkForDrawing[(position+1)%5].changeStateUSING();
                }
                forksAndSema.semaphore.Release();
                if (startedEating)
                {
                    Thread.Sleep(4000);
                    stopEatingAndthink(forksAndSema);
                }
                else
                {
                    changeState(State.waiting);
                    Thread.Sleep(2000);
                    eat(forksAndSema);
                }

            }

            void eatLeadingToDeadLock(Fork forkAndSema,bool fork1, bool fork2)
            {
               
                forkAndSema.semaphore.WaitOne();
               // bool startedEating = false;
                if (forkAndSema.forks[position ] && !fork1)
                {
                    forkAndSema.forks[position] = false;
                    fork1 = true;

                }
                if (forkAndSema.forks[(position + 1) % 5] && !fork2)
                {
                    forkAndSema.forks[(position + 1) % 5] = false;
                    fork2 = true;
                }
                forkAndSema.semaphore.Release();
                if(fork1 && fork2)
                {
                    changeState(State.eating);
                    Thread.Sleep(3000);
                    stopEatingAndthink(forkAndSema);
                    
                }
                else
                {
                    changeState(State.waiting);
                    Thread.Sleep(300);
                    eatLeadingToDeadLock(forkAndSema, fork1, fork2);
                }


              




            }

            void stopEatingAndthink(Fork forksAndSema)
            {
                forksAndSema.semaphore.WaitOne();
                changeState(State.thinking);
                forksAndSema.forks[position] = true;
                forksAndSema.forks[(position + 1) % 5] = true;
                Fork.forkForDrawing[position].changeStateFREE();
                Fork.forkForDrawing[(position + 1) % 5].changeStateFREE();
                forksAndSema.semaphore.Release();
                //UI signal
                Thread.Sleep(4000);
                eat(forksAndSema);
            }
            public void start(Object data)
            {
               
               //Fork forksAndSema = (Fork)data;
                //if the mode is syncronization
                if (Mode == 1)
                    eat(forksAndSema);
                else
                {
                                    
                    
                    eatLeadingToDeadLock(forksAndSema, false, false);
                }

            }
        }


      

        public DinnigTable(int Mode)
        {
            forksAndSemaphore = new Fork();
            StateMode = Mode;
            philisofers = new Philisofers[5];
            for(int i=0;i<philisofers.Length;++i)
            {
                philisofers[i] = new Philisofers(i,coordinates[i],StateMode,forksAndSemaphore);
            }
            
        }

        public void startEating()
        {
            foreach (var item in philisofers)
            {
                item.thread.Start(forksAndSemaphore);
            }
        }

        public String state()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int i = 0;
          
            foreach (var item in philisofers)
            {
                stringBuilder.Append("Philosofer " + (i+1) + " state is " + item.state);
                stringBuilder.Append("\n");
                i++;
            }
            i = 0;
           /* stringBuilder.Append("Forks:");
            foreach (var item in forksAndSemaphore.forks)
            {
                stringBuilder.Append("Fork  " + i + " is " + forksAndSemaphore.forks[i]);
                stringBuilder.Append("\n");
                i++;
            }*/
          
            return stringBuilder.ToString();
        }

        public void draw(Graphics g)
        {
            foreach (var item in philisofers)
            {
                item.circle.draw(g);
            }

            foreach (var item in Fork.forkForDrawing)
            {
                item.draw(g);
            }
        }

       
    }
}
