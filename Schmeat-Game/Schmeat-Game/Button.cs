using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schmeat_Game
{
    public class Button : GameObject
    {
        private Action action;
        public Action Action { get => action; set => action = value; }

        public Button(Vector2 position, Vector2 size, Action action) : base()
        {
            Position = position;
            Hitbox = new Rectangle(0, 0, (int)size.X, (int)size.Y);
            Action = action;
        }


        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("hitbux");
            //base.LoadContent(content);
        }

        
    }
}
