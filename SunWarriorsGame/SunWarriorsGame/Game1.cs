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
using System.Timers;
using Tank_Game;
using TankClient;

namespace SunWarriorsGame
{
    /// This is the main type for the game
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        enum BState
        {
            HOVER,
            UP,
            JUST_RELEASED,
            DOWN
        }
        const int NUMBER_OF_BUTTONS = 9,
            JOIN_BUTTON_INDEX = 0,
            AIMODE_BUTTON_INDEX = 1,
            UP_BUTTON_INDEX = 2,
            DOWN_BUTTON_INDEX = 3,
            LEFT_BUTTON_INDEX = 4,
            RIGHT_BUTTON_INDEX = 5,
            SHOOT_BUTTON_INDEX = 6,
            BLANK_BUTTON_INDEX = 7,
            EXIT_BUTTON_INDEX = 8,
            BUTTON_HEIGHT1 = 50,
            BUTTON_WIDTH1 = 130;
            //BUTTON_HEIGHT2 = 48,
            //BUTTON_WIDTH2 = 48;
        //Color background_color;
        Color[] button_color = new Color[NUMBER_OF_BUTTONS];
        Rectangle[] button_rectangle = new Rectangle[NUMBER_OF_BUTTONS];
        BState[] button_state = new BState[NUMBER_OF_BUTTONS];
        Texture2D[] button_texture = new Texture2D[NUMBER_OF_BUTTONS];
        double[] button_timer = new double[NUMBER_OF_BUTTONS];
        //mouse pressed and mouse just pressed
        bool mpressed, prev_mpressed = false;
        //mouse location in window
        int mx, my;
        double frame_time;

        GameEngine gameEngine;
        ConnectClient client;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        int screenWidth;
        int screenHeight;
        Texture2D backgroundTexture;
        Texture2D scoreTexture;
        Texture2D landTexture;
        Texture2D brickTexture;
        Texture2D brick75Texture;
        Texture2D brick50Texture;
        Texture2D brick25Texture;
        Texture2D stoneTexture;
        Texture2D waterTexture;
        Texture2D coinTexture;
        Texture2D lifepackTexture;
        Texture2D player1Texture;
        Texture2D player2Texture;
        Texture2D player3Texture;
        Texture2D player4Texture;
        Texture2D player5Texture;
        Texture2D rocketTexture;
        Texture2D smokeTexture;
        Texture2D messageboxTexture;
        SpriteFont font;

        bool rocketFlying = false;
        Vector2 rocketPosition;
        Vector2 rocketDirection;
        float rocketAngle;
        float rocketScaling = 0.1f;
        AIController aiController;
        bool aiModeOn = false;

        public Game1()
        {
            gameEngine = GameEngine.GetGameEngine();
            client = new ConnectClient();

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
            //IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1300;             
            graphics.PreferredBackBufferHeight = 680;              
            graphics.IsFullScreen = false;              
            graphics.ApplyChanges();              
            Window.Title = "The Sun Warriors";

            // starting x and y locations to stack buttons 
            // vertically in the middle of the screen
            int x = 50;
            int y = 50;
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {
                button_state[i] = BState.UP;
                button_color[i] = Color.White;
                button_timer[i] = 0.0;
                button_rectangle[i] = new Rectangle(x, y, BUTTON_WIDTH1, BUTTON_HEIGHT1);
                y += BUTTON_HEIGHT1;
            }
            IsMouseVisible = true;
            //background_color = Color.CornflowerBlue;
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
            scoreTexture = Content.Load<Texture2D>("scoreBoard");
            landTexture = Content.Load<Texture2D>("landTexture");
            brickTexture = Content.Load<Texture2D>("brickTexture");
            brick75Texture = Content.Load<Texture2D>("brick75Texture");
            brick50Texture = Content.Load<Texture2D>("brick50Texture");
            brick25Texture = Content.Load<Texture2D>("brick25Texture");
            waterTexture = Content.Load<Texture2D>("waterTexture");
            stoneTexture = Content.Load<Texture2D>("stoneTexture");
            coinTexture = Content.Load<Texture2D>("coin");
            lifepackTexture = Content.Load<Texture2D>("lifepack");
            player1Texture = Content.Load<Texture2D>("tank");
            player2Texture = Content.Load<Texture2D>("tank2");
            player3Texture = Content.Load<Texture2D>("tank3");
            player4Texture = Content.Load<Texture2D>("tank4");
            player5Texture = Content.Load<Texture2D>("tank5");
            rocketTexture = Content.Load<Texture2D>("bullet");
            smokeTexture = Content.Load<Texture2D>("smoke");
            messageboxTexture = Content.Load<Texture2D>("messageBox");
            font = Content.Load<SpriteFont>("myFont");

            button_texture[JOIN_BUTTON_INDEX] = Content.Load<Texture2D>("joinbutton");
            button_texture[AIMODE_BUTTON_INDEX] = Content.Load<Texture2D>("aimodebutton");
            button_texture[UP_BUTTON_INDEX] = Content.Load<Texture2D>("up");
            button_texture[DOWN_BUTTON_INDEX] = Content.Load<Texture2D>("down");
            button_texture[LEFT_BUTTON_INDEX] = Content.Load<Texture2D>("left");
            button_texture[RIGHT_BUTTON_INDEX] = Content.Load<Texture2D>("right");
            button_texture[SHOOT_BUTTON_INDEX] = Content.Load<Texture2D>("shoot");
            button_texture[BLANK_BUTTON_INDEX] = Content.Load<Texture2D>("blankButton");
            button_texture[EXIT_BUTTON_INDEX] = Content.Load<Texture2D>("exitButton");
            //button_texture[MEDIUM_BUTTON_INDEX] =Content.Load<Texture2D>(@"images/medium");
            //button_texture[HARD_BUTTON_INDEX] =Content.Load<Texture2D>(@"images/hard");
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

            // get elapsed frame time in seconds
            frame_time = gameTime.ElapsedGameTime.Milliseconds / 1000.0;

            // update mouse variables
            MouseState mouse_state = Mouse.GetState();
            mx = mouse_state.X;
            my = mouse_state.Y;
            prev_mpressed = mpressed;
            mpressed = mouse_state.LeftButton == ButtonState.Pressed;

            update_buttons();
            UpdateRocket();
            rocketFlying = false;
            //ProcessKeyboard();
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
             if (keyState.IsKeyDown(Keys.Down))
             {
                 client.SendData("SHOOT#");
             }
         } 

        /// This is called when the game should draw itself.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            DrawBackground();
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
                spriteBatch.Draw(button_texture[i], button_rectangle[i], button_color[i]);
            DrawGrid(gameEngine.getGrid());
            ShowOtherMessages();
            //DrawRocket();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawBackground()
        {
             Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
             spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
             spriteBatch.Draw(scoreTexture, new Rectangle(870, 50, 400, 300), Color.White);
             spriteBatch.Draw(messageboxTexture, new Rectangle(870, 450, 400, 100), Color.White);
        }

        private void DrawGrid(GridEntity[,] g)
        {
            if (g != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Vector2 position = new Vector2(230 + (j * 60), 50 + (i * 60));
                        string entity = g[i, j].getName();
                        switch (entity)
                        {
                            case "brick":
                                int d = g[i, j].getDamageLevel();
                                switch (d)
                                {
                                    case 0:
                                        spriteBatch.Draw(brickTexture, position, Color.White);
                                        break;
                                    case 1:
                                        spriteBatch.Draw(brick75Texture, position, Color.White);
                                        break;
                                    case 2:
                                        spriteBatch.Draw(brick50Texture, position, Color.White);
                                        break;
                                    case 3:
                                        spriteBatch.Draw(brick25Texture, position, Color.White);
                                        break;
                                    case 4:
                                        spriteBatch.Draw(landTexture, position, Color.White);
                                        break;
                                    default:
                                        spriteBatch.Draw(brickTexture, position, Color.White);
                                        break;
                                }
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
                                spriteBatch.Draw(player1Texture, new Vector2(position.X + 30, position.Y + 30), null, Color.White, MathHelper.ToRadians((g[i, j]).getDirection()), new Vector2(30, 30), 1, SpriteEffects.None, 1);
                                if(rocketFlying)
                                    DrawRocket(position, MathHelper.ToRadians((g[i, j]).getDirection()));
                                spriteBatch.DrawString(font, (g[i, j]).getCoins().ToString() + "$", new Vector2(980, 110), Color.Black);
                                spriteBatch.DrawString(font, (g[i, j]).getPoints().ToString() + "$", new Vector2(1080, 110), Color.Black);
                                spriteBatch.DrawString(font, (g[i, j]).getHealth().ToString() + "%", new Vector2(1180, 110), Color.Black);
                                break;
                            case "P1":
                                //spriteBatch.Draw(playerTexture, position, Color.White);
                                spriteBatch.Draw(player2Texture, new Vector2(position.X + 30, position.Y + 30), null, Color.White, MathHelper.ToRadians((g[i, j]).getDirection()), new Vector2(30, 30), 1, SpriteEffects.None, 1);
                                spriteBatch.DrawString(font, (g[i, j]).getCoins().ToString() + "$", new Vector2(980, 160), Color.Black);
                                spriteBatch.DrawString(font, (g[i, j]).getPoints().ToString() + "$", new Vector2(1080, 160), Color.Black);
                                spriteBatch.DrawString(font, (g[i, j]).getHealth().ToString() + "%", new Vector2(1180, 160), Color.Black);
                                break;
                            case "P2":
                                //spriteBatch.Draw(playerTexture, position, Color.White);
                                spriteBatch.Draw(player3Texture, new Vector2(position.X + 30, position.Y + 30), null, Color.White, MathHelper.ToRadians((g[i, j]).getDirection()), new Vector2(30, 30), 1, SpriteEffects.None, 1);
                                spriteBatch.DrawString(font, (g[i, j]).getCoins().ToString() + "$", new Vector2(980, 210), Color.Black);
                                spriteBatch.DrawString(font, (g[i, j]).getPoints().ToString() + "$", new Vector2(1080, 210), Color.Black);
                                spriteBatch.DrawString(font, (g[i, j]).getHealth().ToString() + "%", new Vector2(1180, 210), Color.Black);
                                break;
                            case "P3":
                                //spriteBatch.Draw(playerTexture, position, Color.White);
                                spriteBatch.Draw(player4Texture, new Vector2(position.X + 30, position.Y + 30), null, Color.White, MathHelper.ToRadians((g[i, j]).getDirection()), new Vector2(30, 30), 1, SpriteEffects.None, 1);
                                spriteBatch.DrawString(font, (g[i, j]).getCoins().ToString() + "$", new Vector2(980, 260), Color.Black);
                                spriteBatch.DrawString(font, (g[i, j]).getPoints().ToString() + "$", new Vector2(1080, 260), Color.Black);
                                spriteBatch.DrawString(font, (g[i, j]).getHealth().ToString() + "%", new Vector2(1180, 260), Color.Black);
                                break;
                            case "P4":
                                //spriteBatch.Draw(playerTexture, position, Color.White);
                                spriteBatch.Draw(player5Texture, new Vector2(position.X + 30, position.Y + 30), null, Color.White, MathHelper.ToRadians((g[i, j]).getDirection()), new Vector2(30, 30), 1, SpriteEffects.None, 1);
                                spriteBatch.DrawString(font, (g[i, j]).getCoins().ToString() + "$", new Vector2(980, 310), Color.Black);
                                spriteBatch.DrawString(font, (g[i, j]).getPoints().ToString() + "$", new Vector2(1080, 310), Color.Black);
                                spriteBatch.DrawString(font, (g[i, j]).getHealth().ToString() + "%", new Vector2(1180, 310), Color.Black);
                                break;
                            default:
                                spriteBatch.Draw(landTexture, position, Color.White);
                                break;
                        }
                    }
                }
            }
        }

        private void ShowOtherMessages()
        {
            spriteBatch.DrawString(font, gameEngine.getOtherMsg(), new Vector2(980, 500), Color.Black);
        }

        private void DrawRocket(Vector2 position, float angle)
        {
            rocketPosition = position;
            //rocketPosition = position + new Vector2((float)(30*Math.Sin(angle)), ((float)(30*Math.Cos(angle))));
            rocketPosition.X += 30;
            rocketPosition.Y -= 5;
            rocketAngle = angle;
            Vector2 up = new Vector2(0,-1);
            Matrix rotMatrix = Matrix.CreateRotationZ(rocketAngle);
            rocketDirection = Vector2.Transform(up, rotMatrix);
            spriteBatch.Draw(rocketTexture, rocketPosition, null, Color.White, rocketAngle, new Vector2(30, 160), 0, SpriteEffects.None, 1);
            //rocketFlying = false;
        }

        private void UpdateRocket()
        {
            if (rocketFlying)
            {
                rocketPosition += rocketDirection;
            }
        }

        // wrapper for hit_image_alpha taking Rectangle and Texture
        Boolean hit_image_alpha(Rectangle rect, Texture2D tex, int x, int y)
        {
            return hit_image_alpha(0, 0, tex, tex.Width * (x - rect.X) /
                rect.Width, tex.Height * (y - rect.Y) / rect.Height);
        }

        // wraps hit_image then determines if hit a transparent part of image 
        Boolean hit_image_alpha(float tx, float ty, Texture2D tex, int x, int y)
        {
            if (hit_image(tx, ty, tex, x, y))
            {
                uint[] data = new uint[tex.Width * tex.Height];
                tex.GetData<uint>(data);
                if ((x - (int)tx) + (y - (int)ty) *
                    tex.Width < tex.Width * tex.Height)
                {
                    return ((data[
                        (x - (int)tx) + (y - (int)ty) * tex.Width
                        ] &
                                0xFF000000) >> 24) > 20;
                }
            }
            return false;
        }

        // determine if x,y is within rectangle formed by texture located at tx,ty
        Boolean hit_image(float tx, float ty, Texture2D tex, int x, int y)
        {
            return (x >= tx &&
                x <= tx + tex.Width &&
                y >= ty &&
                y <= ty + tex.Height);
        }

        // determine state and color of button
        void update_buttons()
        {
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {

                if (hit_image_alpha(
                    button_rectangle[i], button_texture[i], mx, my))
                {
                    button_timer[i] = 0.0;
                    if (mpressed)
                    {
                        // mouse is currently down
                        button_state[i] = BState.DOWN;
                        button_color[i] = Color.Blue;
                    }
                    else if (!mpressed && prev_mpressed)
                    {
                        // mouse was just released
                        if (button_state[i] == BState.DOWN)
                        {
                            // button i was just down
                            button_state[i] = BState.JUST_RELEASED;
                        }
                    }
                    else
                    {
                        button_state[i] = BState.HOVER;
                        button_color[i] = Color.LightBlue;
                    }
                }
                else
                {
                    button_state[i] = BState.UP;
                    if (button_timer[i] > 0)
                    {
                        button_timer[i] = button_timer[i] - frame_time;
                    }
                    else
                    {
                        button_color[i] = Color.White;
                    }
                }

                if (button_state[i] == BState.JUST_RELEASED)
                {
                    take_action_on_button(i);
                }
            }
        }


        // Logic for each button click goes here
        void take_action_on_button(int i)
        {
            //take action corresponding to which button was clicked
            switch (i)
            {
                case JOIN_BUTTON_INDEX:
                    client.SendData("JOIN#");
                    break;
                case AIMODE_BUTTON_INDEX:
                    aiController = new AIController();
                    aiModeOn = true;
                    //aiController.startAI();
                    InitTimer();
                    break;
                case UP_BUTTON_INDEX:
                    client.SendData("UP#");
                    break;
                case DOWN_BUTTON_INDEX:
                    client.SendData("DOWN#");
                    break;
                case LEFT_BUTTON_INDEX:
                    client.SendData("LEFT#");
                    break;
                case RIGHT_BUTTON_INDEX:
                    client.SendData("RIGHT#");
                    break;
                case SHOOT_BUTTON_INDEX:
                    client.SendData("SHOOT#");
                    rocketFlying = true;
                    break;
                case EXIT_BUTTON_INDEX:
                    break;
                //case HARD_BUTTON_INDEX:
                //    background_color = Color.Red;
                //    break;
                default:
                    break;
            }
        }

        private void InitTimer()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 2500;
            aTimer.Start();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            String msg = aiController.getMsg();
            if (!msg.Equals("") || !msg.Equals(null))
                client.SendData(msg);
            //InitTimer();
        }
    }
}
