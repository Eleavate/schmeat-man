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
        private Rectangle hitbox;

        public Vector2 Position
        {
            get => position;
            protected set
            {
                position = value;
                Hitbox = new Rectangle((int)value.X - (Hitbox.Width / 2), (int)value.Y - (Hitbox.Height / 2), Hitbox.Width, Hitbox.Height);
            }
        }
        public Rectangle Hitbox 
        {
            get => hitbox;
            protected set
            {
                hitbox = new Rectangle((int)(Position.X - (value.Width / 2)), (int)Position.Y - (value.Height / 2), value.Width, value.Height);
            }
        }


        /// <summary>
        /// Loads needed assets for this GameObject
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
            Hitbox = new Rectangle(Hitbox.X, Hitbox.Y, (int)(sprite.Width * scale), (int)(sprite.Height * scale));
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Position, null, Color.White, 0, new Vector2(sprite.Width / 2, sprite.Height / 2), scale, SpriteEffects.None, layer);
        }
    }
}
