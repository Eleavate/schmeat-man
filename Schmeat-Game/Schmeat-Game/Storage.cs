using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Schmeat_Game
{
    public class Storage : Workspace
    {
        //Fields

        //Properties

        //Constructors
        public Storage(Vector2 position)
        {
            this.position = position;
            scale = 0.3f;
            EmployeePosition=new Vector2 (position.X, position.Y+50);
            layer = 0.9f;
        }

        //Methods

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("stolen storage door");
            base.LoadContent(content);
        }
        public static void Restock(Employee worker)
        {
            try
            {
                Thread.Sleep(500);
                GameWorld.Meat -= 1;
                Console.WriteLine("Employee got some meat from storage");
                worker.CurrentlyCarrying = Carrying.Meat;
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
