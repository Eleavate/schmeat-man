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
    public abstract class Workspace : GameObject
    {
        public Vector2 EmployeePosition;
        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("temp_cashregister");
            base.LoadContent(content);
        }
    }
}
