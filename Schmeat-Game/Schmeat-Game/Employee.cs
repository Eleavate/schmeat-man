using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Schmeat_Game
{
    public class Employee : GameObject
    {
        Thread employeeThread = new Thread(ActiveThread);
        public enum Carrying { Nothing,Meat,PreparedMeat};

        Carrying currentlyCarrying = Carrying.Nothing;

        public Employee() 
        {
            employeeThread.IsBackground = true;
            employeeThread.Start();
            scale=0.05f;
            position = new Vector2(200, 400);
        }
        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("temp sprite");
        }

        static void ActiveThread()
        {
            //get job 
            //do job (call)
        }

    }
}
