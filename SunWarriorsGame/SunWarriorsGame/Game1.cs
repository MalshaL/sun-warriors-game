using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Tank_Game;
using TankClient;

namespace SunWarriorsGame
{
    /// This is the main type for the game
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GridEntity[,] grid;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        int screenWidth;
        int screenHeight;
        Texture2D backgroundTexture;
        Texture2D defaultGridTexture;

        public Game1(GridEntity[,] g)
        {
            this.grid = g;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1200;             
            graphics.PreferredBackBufferHeight = 680;              
            graphics.IsFullScreen = false;              
            graphics.ApplyChanges();              
            Window.Title = "The Sun Warriors"; 
            base.Initialize();
        }

        /// LoadContent will be called once per game and is the place to load all of the content.
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;
            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;
            backgroundTexture = Content.Load<Texture2D>("background");
            defaultGridTexture = Content.Load<Texture2D>("default");
        }

        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        protected override void UnloadContent()
        {
           
        }

        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            base.Update(gameTime);
        }

        /// This is called when the game should draw itself.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            DrawBackground();
            DrawGrid();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawBackground()
        {
             Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
             spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White); 
        }

        private void DrawGrid()
        {
            spriteBatch.Draw(defaultGridTexture, new Vector2(200,100), Color.White);
        }
    }
}
