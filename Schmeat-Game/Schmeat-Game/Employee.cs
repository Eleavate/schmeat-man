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
    public enum Carrying { Nothing, Meat, PreparedMeat };
    public delegate void WorkStationTask();
    public class Employee : GameObject
    {
        private Thread employeeThread = new Thread(ActiveThread);
        private Carrying currentlyCarrying = Carrying.Nothing;
        private Queue<WorkStationTask> tasks = new Queue<WorkStationTask>();

        public Employee()
        {
            employeeThread.IsBackground = true;
            employeeThread.Start();
            scale = 0.05f;
            Position = new Vector2(200, 400);
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

        public void doThing(WorkStationTask task)
        {

        }
    }
}
