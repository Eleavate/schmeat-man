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
    public class HUDObject : GameObject
    {
        private SpriteFont hudFont;
        private string text;
        private Color textColor;

        
        public string Text { get => text; set => text = value; }
        public Color TextColor { get => textColor; set => textColor = value; }

        public HUDObject(Vector2 position, string text, Color color)
        {
            Position = position;
            Text = text;
            TextColor = color;
        }

        public override void Update(GameTime gameTime)
        {
            Text = "Schmeat Coin: " + GameWorld.SchmeatCoin;
            base.Update(gameTime);
        }

        public override void LoadContent(ContentManager content)
        {
            hudFont = content.Load<SpriteFont>("hudfont");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(hudFont, Text, Position, TextColor);
        }
    }

}
