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
    public class CashRegister : Workspace
    {
        //Fields

        //Properties

        //Constructor
        public CashRegister(Vector2 position)
        {
            this.position = position;
        }

        //Methods
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            sprite = content.Load<Texture2D>("temp_cashregister");
        }
    }
}
