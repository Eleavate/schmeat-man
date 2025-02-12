using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
        }

        //Methods

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            sprite = content.Load<Texture2D>("temp_storage");
        }
        public static void Restock()
        {
            Thread.Sleep(500);
            GameWorld.Meat -= 1;
        }
    }
}
