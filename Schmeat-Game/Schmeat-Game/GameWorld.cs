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
        private List<GameObject> activeGameObjects = new List<GameObject>();
        private List<GameObject> gameObjectsToBeAdded = new List<GameObject>();
        private List<GameObject> gameObjectsToBeRemoved = new List<GameObject>();
        //common resources go here

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
            Employee steve = new Employee();
            AddGameObject(steve);
            CashRegister cashRegister = new CashRegister(Vector2.Zero);
            AddGameObject(cashRegister);
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

            foreach (var gameObject in activeGameObjects)
            {
                gameObject.Update(gameTime);
            }

            if(gameObjectsToBeAdded.Count > 0)
            {
                activeGameObjects.AddRange(gameObjectsToBeAdded);
                gameObjectsToBeAdded.Clear();
            }

            foreach (var gameObject in gameObjectsToBeRemoved)
            {
                activeGameObjects.Remove(gameObject);
                gameObjectsToBeRemoved.Remove(gameObject);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach (var gameObject in activeGameObjects) 
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
