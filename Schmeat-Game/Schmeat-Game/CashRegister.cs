using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Schmeat_Game
{
    public class CashRegister : Workspace
    {
        //Fields

        //Properties

        //Constructor
        public CashRegister(Vector2 position)
        {
            Position = position;
            scale = 1.2f;
            layer = 0.5f;
        }

        //Methods
        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("stolen shop counter");
            EmployeePosition = new Vector2(position.X, position.Y - 20);
            base.LoadContent(content);
        }

        public static void Sell(Employee worker)
        {
            try
            {
                //need to keep track of threads
                if (worker.CurrentlyCarrying == Carrying.Meat)
                {
                    Debug.WriteLine("Employee started working at cash register");
                    Thread.Sleep(500);
                    GameWorld.SchmeatCoin += 50;
                    Debug.WriteLine("Employee got money");
                    worker.CurrentlyCarrying = Carrying.Nothing;
                }
            }
            //when recieving command
            catch (ThreadInterruptedException)
            {
                Debug.WriteLine("Employee stopped working at cash register");
            }
            catch (ThreadAbortException)
            {
                Debug.WriteLine("Employee stopped working at cash register");
            }

        }
    }
}
