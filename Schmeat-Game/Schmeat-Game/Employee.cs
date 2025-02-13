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

        private Thread employeeThread;
        private float speed = 10;
        private Vector2 velocity;
        private float deltaTime;

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
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (workingAt != Jobs.None)
            {
                employeeThread.Interrupt();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteColor = (UIManager.HasPickedEmployee & UIManager.Employee == this) ? Color.DarkViolet : Color.White;
            base.Draw(spriteBatch);
        }

        /// <summary>
        /// The function where the active thread is running
        /// </summary>
        private void ActiveThread()
        {
            while (true)
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
        /// <param name="newJob"></param>
        private void GiveTask(Jobs newJob)
        {
            //change task
            this.workingAt = newJob;
        }

        public void DoThing(Workspace task)
        {
            //move to selected workstation
            /*
            while (!hitbox.Intersects(task.Hitbox))
                {
                velocity = Vector2.Zero;
                Vector2 direction = new Vector2(task.Position.X - position.X, task.Position.Y - position.Y);
                double test = Math.Atan2(direction.Y, direction.X);
                float XDirection = (float)Math.Cos(test);
                float YDirection = (float)Math.Sin(test);
                direction = new Vector2(XDirection, YDirection);
                velocity = (direction);
                velocity.Normalize();

                Vector2 change = ((velocity * speed) * deltaTime);
                Position += change;
            }*/

            switch (task)
            {
                case CashRegister:
                    GiveTask(Jobs.SellMeat);
                    break;
                case null:
                    GiveTask(Jobs.None);
                    break;
            }
        }
    }
}
