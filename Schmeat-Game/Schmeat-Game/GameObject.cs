using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schmeat_Game
{
    public class GameObject
    {
        protected Texture2D sprite;
        protected float scale = 1;
        protected float layer = 0;
        protected Vector2 position;
        public virtual void LoadContent(ContentManager content)
        {
            
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0, new Vector2(sprite.Width / 2, sprite.Height / 2), scale, SpriteEffects.None, layer);
        }
    }
}
