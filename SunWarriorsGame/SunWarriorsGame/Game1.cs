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
        GameEngine gameEngine;
        ConnectClient client;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        int screenWidth;
        int screenHeight;
        Texture2D backgroundTexture;
        Texture2D landTexture;
        Texture2D brickTexture;
        Texture2D stoneTexture;
        Texture2D waterTexture;
        Texture2D coinTexture;
        Texture2D lifepackTexture;
        Texture2D playerTexture;

        public Game1()
        {
            gameEngine = GameEngine.GetGameEngine();
            client = ConnectClient.GetClient();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public void setGrid(GridEntity[,] g)
        {
            //grid = gameEngine.getGrid();
        }

        
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1100;             
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
            landTexture = Content.Load<Texture2D>("landTexture");
            brickTexture = Content.Load<Texture2D>("brickTexture");
            waterTexture = Content.Load<Texture2D>("waterTexture");
            stoneTexture = Content.Load<Texture2D>("stoneTexture");
            coinTexture = Content.Load<Texture2D>("coin");
            lifepackTexture = Content.Load<Texture2D>("lifepack");
            playerTexture = Content.Load<Texture2D>("tank");
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
            //DrawGrid(getGrid());
            ProcessKeyboard();
            base.Update(gameTime);
        }

         private void ProcessKeyboard()  
         {      
             KeyboardState keyState = Keyboard.GetState();
             if (keyState.IsKeyDown(Keys.Left))
             {
                 client.SendData("LEFT#");
             }
             if (keyState.IsKeyDown(Keys.Right))
             {
                 client.SendData("RIGHT#");
             }
             if (keyState.IsKeyDown(Keys.Up))
             {
                 client.SendData("UP#");
             }
             if (keyState.IsKeyDown(Keys.Down))
             {
                 client.SendData("DOWN#");
             } 
         } 

        /// This is called when the game should draw itself.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            DrawBackground();
            DrawGrid(gameEngine.getGrid());
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawBackground()
        {
             Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
             spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White); 
        }

        private void DrawGrid(GridEntity[,] g)
        {
            if (g != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Vector2 position = new Vector2(350 + (j * 60), 50 + (i * 60));
                        string entity = g[i, j].getName();
                        switch (entity)
                        {
                            case "brick":
                                spriteBatch.Draw(brickTexture, position, Color.White);
                                break;
                            case "water":
                                spriteBatch.Draw(waterTexture, position, Color.White);
                                break;
                            case "stone":
                                spriteBatch.Draw(stoneTexture, position, Color.White);
                                break;
                            case "coin":
                                spriteBatch.Draw(coinTexture, position, Color.White);
                                break;
                            case "lifepack":
                                spriteBatch.Draw(lifepackTexture, position, Color.White);
                                break;
                            case "P0":
                                //spriteBatch.Draw(playerTexture, position, Color.White);
                                spriteBatch.Draw(playerTexture, new Vector2(position.X + 30, position.Y + 30), null, Color.White, MathHelper.ToRadians(g[i, j].getDirection()), new Vector2(30, 30), 1, SpriteEffects.None, 1);
                                break;
                            default:
                                spriteBatch.Draw(landTexture, position, Color.White);
                                break;
                        }
                    }
                }
            }
        }
    }
}
