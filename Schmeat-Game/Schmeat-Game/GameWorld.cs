using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Schmeat_Game
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static List<GameObject> activeGameObjects = new List<GameObject>();
        private static List<GameObject> gameObjectsToBeAdded = new List<GameObject>();
        private static List<GameObject> gameObjectsToBeRemoved = new List<GameObject>();
        private static Texture2D hitboxSprite;
        private Texture2D backgroundTexture;

        //common resources go here
        private static int schmeatCoin;
        private static int meat;

        private static object schmeatCoinKey;
        private static object meatKey;

        //temp
        private Employee steve;
        private CashRegister cashRegister;
        public static float DeltaTime {  get; private set; }

        public static List<GameObject> ActiveGameObjects { get => activeGameObjects; set => activeGameObjects = value; }
        public static int SchmeatCoin { get => schmeatCoin; set => schmeatCoin = value; }
        public static int Meat { get => meat; set => meat = value; }


        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();
            base.Initialize();
            steve = new Employee(new Vector2(900,1000));
            AddGameObject(steve);
            cashRegister = new CashRegister(new Vector2(1000,500));
            AddGameObject(cashRegister);
            Storage storage = new Storage(new Vector2(100, 180));
            AddGameObject(storage);
            SchmeatCoin = 150;
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            hitboxSprite = Content.Load<Texture2D>("hitbux");
            backgroundTexture = Content.Load<Texture2D>("basic room");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            DeltaTime=(float)gameTime.ElapsedGameTime.TotalSeconds;
            // TODO: Add your update logic here


            foreach (var gameObject in ActiveGameObjects)
            {
                gameObject.Update(gameTime);
            }

            if (gameObjectsToBeAdded.Count > 0)
            {
                ActiveGameObjects.AddRange(gameObjectsToBeAdded);
                gameObjectsToBeAdded.Clear();
                steve.DoThing(cashRegister);
            }

            foreach (var gameObject in gameObjectsToBeRemoved)
            {
                ActiveGameObjects.Remove(gameObject);
                gameObjectsToBeRemoved.Remove(gameObject);
            }

            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed)
            {
                UIManager.ScreenClicked(new Vector2(state.Position.X, state.Position.Y));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTexture,Vector2.Zero,Color.White);
            foreach (var gameObject in ActiveGameObjects)
            {
                gameObject.Draw(_spriteBatch);

#if DEBUG
                Rectangle hitBox = gameObject.Hitbox;
                Rectangle topline = new Rectangle(hitBox.X, hitBox.Y, hitBox.Width, 1);
                Rectangle bottomline = new Rectangle(hitBox.X, hitBox.Y + hitBox.Height, hitBox.Width, 1);
                Rectangle rightline = new Rectangle(hitBox.X + hitBox.Width, hitBox.Y, 1, hitBox.Height);
                Rectangle leftline = new Rectangle(hitBox.X, hitBox.Y, 1, hitBox.Height);

                _spriteBatch.Draw(hitboxSprite, topline, null, Color.White);
                _spriteBatch.Draw(hitboxSprite, bottomline, null, Color.White);
                _spriteBatch.Draw(hitboxSprite, rightline, null, Color.White);
                _spriteBatch.Draw(hitboxSprite, leftline, null, Color.White);

                Vector2 position = gameObject.Position;
                Rectangle centerDot = new Rectangle((int)position.X - 1, (int)position.Y - 1, 3, 3);

                _spriteBatch.Draw(hitboxSprite, centerDot, null, Color.White);
#endif
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Adds a GameObject to the gameworld, after the next update.
        /// </summary>
        /// <param name="gameObject">The GameObject to be added.</param>
        public void AddGameObject(GameObject gameObject)
        {
            gameObject.LoadContent(Content);
            gameObjectsToBeAdded.Add(gameObject);
        }

        /// <summary>
        /// Removes a GameObject from the gameworld, after the next update.
        /// </summary>
        /// <param name="gameObject">The GameObject to be removed.</param>
        public void RemoveGameObject(GameObject gameObject)
        {
            gameObjectsToBeRemoved.Add(gameObject);
        }
    }
}
