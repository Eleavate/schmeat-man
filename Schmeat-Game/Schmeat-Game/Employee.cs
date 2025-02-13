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
    public enum Carrying { Nothing, Meat, PreparedMeat };
    public enum Jobs { GetMeatFromStock, CutMeat, SellMeat, None }
    public class Employee : GameObject
    {
        //the workspacce where the employee is currently at
        private Jobs workingAt = Jobs.None;
        private Carrying currentlyCarrying = Carrying.Nothing;

        private float speed = 200f;
        private Thread employeeThread;

        private Vector2 velocity;
        private float deltaTime;

        private Workspace taskPlace;
        public Carrying CurrentlyCarrying { get => currentlyCarrying; set => currentlyCarrying = value; }

        /// <summary>
        /// Standard constructor; starts Thread and sets scale & position
        /// </summary>
        public Employee(Vector2 position)
        {
            employeeThread = new Thread(ActiveThread);
            employeeThread.IsBackground = true;
            employeeThread.Start();
            scale = 0.05f;
            Position = position;
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
            if (taskPlace != null)
            {
                string tmp = employeeThread.ThreadState.ToString();
                if ((employeeThread.ThreadState & System.Threading.ThreadState.WaitSleepJoin) == System.Threading.ThreadState.WaitSleepJoin)
                {
                    employeeThread.Interrupt();
                }
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
                        Vector2 direction = new Vector2(taskPlace.Position.X - Position.X, taskPlace.Position.Y - Position.Y);
                        double test = Math.Atan2(direction.Y, direction.X);
                        float XDirection = (float)Math.Cos(test);
                        float YDirection = (float)Math.Sin(test);
                        direction = new Vector2(XDirection, YDirection);
                        velocity = (direction);


                        Vector2 change = ((velocity * speed) * GameWorld.DeltaTime);
                        Position += change;
                        velocity.Normalize();

                        //wait for update so the sprite can be drawn
                        try
                        {
                            Debug.WriteLine(this.ToString() + " is sleeping while walking");
                            Thread.Sleep(Timeout.Infinite);
                        }
                        //when recieving command
                        catch (ThreadInterruptedException)
                        {
                            Debug.WriteLine(this.ToString() + " started moving again");
                        }
                        catch (ThreadAbortException)
                        {
                            Debug.WriteLine(this.ToString() + " just got destroyed");
                        }
                    }
                    else
                    {
                        Position = taskPlace.EmployeePosition;
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
                            if (position == taskPlace.EmployeePosition)
                            {
                                //Workspace.(method);
                            }

                            break;
                        case Jobs.CutMeat:
                            if (position == taskPlace.EmployeePosition)
                            {
                                //Workspace.(method);
                            }
                            break;
                        case Jobs.SellMeat:
                            if (position == taskPlace.EmployeePosition)
                            {
                                CashRegister.Sell();
                            }
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
        /// <param name="newJob"></param>
        private void GiveTask(Jobs newJob)
        {
            //change task
            this.workingAt = newJob;
            Console.WriteLine("steve is working");
        }

        public void DoThing(Workspace task)
        {
            //stop sleeping/current task & assign new task
            employeeThread.Interrupt();
            workingAt = Jobs.None;
            this.taskPlace = task;
        }
    }
}
