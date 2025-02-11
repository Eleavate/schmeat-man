using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;


namespace Schmeat_Game
{
    public enum Carrying { Nothing, Meat, PreparedMeat };
    public delegate void WorkStationTask();
    public class Employee : GameObject
    {

        
        //the workspacce where the employee is currently at
        public enum Jobs { GetMeatFromStock, CutMeat, SellMeat, None }
        private Jobs workingAt = Jobs.None;

        public enum Carrying { Nothing, Meat, PreparedMeat };

        private Thread employeeThread;
        private Carrying currentlyCarrying = Carrying.Nothing;
        private Queue<WorkStationTask> tasks = new Queue<WorkStationTask>();

        /// <summary>
        /// Standard constructor; starts Thread and sets scale & position
        /// </summary>
        public Employee(Vector2 position)
        {
            employeeThread = new Thread(ActiveThread);
            employeeThread.IsBackground = true;
            employeeThread.Start();
            scale = 0.05f;
            this.position = position;
        }

        /// <summary>
        /// Loads needed assets for this GameObject
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("temp sprite");
        }

        public override void Update(GameTime gameTime)
        {
            if (workingAt != Jobs.None)
            {
                employeeThread.Interrupt();
            }
        }

        /// <summary>
        /// The function where the active thread is running
        /// </summary>
        private void ActiveThread()
        {
            //if the employee has been given a command and has not yet finished
            if (workingAt != Jobs.None)
            {
                //do job (call method)
                switch (workingAt)
                {
                    case Jobs.GetMeatFromStock:
                        //Workspace.(method);
                        break;
                    case Jobs.CutMeat:
                        //Workspace.(method);
                        break;
                    case Jobs.SellMeat:
                        //Workspace.(method);
                        break;
                }
            }
            else
            {
                //sleep until given command
                try
                {
                    Debug.WriteLine(this.ToString() + " is sleeping");
                    Thread.Sleep(Timeout.Infinite);
                }
                //when recieving command
                catch (ThreadInterruptedException)
                {
                    Debug.WriteLine(this.ToString() + " started working");
                }
                catch (ThreadAbortException)
                {
                    Debug.WriteLine(this.ToString() + " just got destroyed");
                }
            }
        }

        /// <summary>
        /// Changes the task of the chosen employee
        /// </summary>
        /// <param name="workspace"></param>
        public void GiveTask(Jobs newJob)
        {
            //change task
            this.workingAt = newJob;
        }
        
        public void doThing(WorkStationTask task)
        {

        }
    }
}
