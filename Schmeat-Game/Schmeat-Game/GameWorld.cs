﻿using Microsoft.Xna.Framework;
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

        public static List<GameObject> ActiveGameObjects { get => activeGameObjects; set => activeGameObjects = value; }

        //common resources go here
        private static int schmeatCoin;
        private static int meat;

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
            Employee steve = new Employee(new Vector2(200,400));
            AddGameObject(steve);
            CashRegister cashRegister = new CashRegister(new Vector2(1000, 800));
            AddGameObject(cashRegister);
            Storage storage = new Storage(new Vector2(50, 100));
            AddGameObject(storage);
            SchmeatCoin = 150;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed)
            {
                UIManager.ScreenClicked(new Vector2(state.Position.X, state.Position.Y));
            }


            foreach (var gameObject in ActiveGameObjects)
            {
                gameObject.Update(gameTime);
            }

            if(gameObjectsToBeAdded.Count > 0)
            {
                ActiveGameObjects.AddRange(gameObjectsToBeAdded);
                gameObjectsToBeAdded.Clear();
            }

            foreach (var gameObject in gameObjectsToBeRemoved)
            {
                ActiveGameObjects.Remove(gameObject);
                gameObjectsToBeRemoved.Remove(gameObject);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach (var gameObject in ActiveGameObjects) 
            {
                gameObject.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void AddGameObject(GameObject gameObject)
        {
            gameObject.LoadContent(Content);
            gameObjectsToBeAdded.Add(gameObject);
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            gameObjectsToBeRemoved.Add(gameObject);
        }
    }
}
