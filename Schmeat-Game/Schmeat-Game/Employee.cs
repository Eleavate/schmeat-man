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
    public delegate void WorkStationTask();
    public class Employee : GameObject
    {
        //the workspacce where the employee is currently at
        public enum Jobs { GetMeatFromStock, CutMeat, SellMeat, None }
        private Jobs workingAt = Jobs.None;

        public enum Carrying { Nothing, Meat, PreparedMeat };

        private Thread employeeThread;
        private Carrying currentlyCarrying = Carrying.Nothing;

        private float speed = 10;
        private Vector2 velocity;

        private Workspace taskPlace;
        public Carrying CurrentlyCarrying { get => currentlyCarrying; set => currentlyCarrying = value; }

        private float deltaTime;

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
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (workingAt != Jobs.None)
            {
                employeeThread.Interrupt();
                Console.WriteLine("interrupting employee sleep");
            }
        }

        /// <summary>
        /// The function where the active thread is running
        /// </summary>
        private void ActiveThread()
        {
            while (true)
            {
                if (taskPlace != null)
                {
                    if (!Hitbox.Intersects(taskPlace.Hitbox))
                    {
                        velocity = Vector2.Zero;
                        Vector2 direction = new Vector2(taskPlace.Position.X - position.X, taskPlace.Position.Y - position.Y);
                        double test = Math.Atan2(direction.Y, direction.X);
                        float XDirection = (float)Math.Cos(test);
                        float YDirection = (float)Math.Sin(test);
                        direction = new Vector2(XDirection, YDirection);
                        velocity = (direction);
                        velocity.Normalize();

                        Vector2 change = ((velocity * speed) * deltaTime);
                        Position += change;
                    }
                }

                switch (taskPlace)
                {
                    case CashRegister:
                        GiveTask(Jobs.SellMeat);
                        break;
                    case null:
                        GiveTask(Jobs.None);
                        break;
                    default:
                        Console.WriteLine("There's an error in the Employee code!");
                        break;
                }

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
                            CashRegister.Sell();
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
        }

        /// <summary>
        /// Changes the task of the chosen employee
        /// </summary>
        /// <param name="workspace"></param>
        private void GiveTask(Jobs newJob)
        {
            //change task
            this.workingAt = newJob;
            Console.WriteLine("steve is working");
        }

        public void DoThing(Workspace task)
        {
            //move to selected workstation
            employeeThread.Interrupt();
            this.taskPlace = task;
        }
    }
}
