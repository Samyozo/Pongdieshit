using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class BetaPong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D LeftPaddle;   //Declaration of the texture of the left paddle yet to come
        Texture2D RightPaddle;   //Declaration of the texture of the right paddle yet to come
        Texture2D Ball;   //Declaration of the texture of the ball yet to come
        Texture2D RedHP1, RedHP2, RedHP3, BlueHP1, BlueHP2, BlueHP3, HPBar; //Declaration of parts of the HP Bar
        int LeftPaddleYPosition = 192;
        int RightPaddleYPosition = 192;
        int ballYPosition = 232;
        int ballXPosition = 392;
        int ballXSpeed = 5;
        int ballYSpeed = 3;
        int RightPlayerLives = 3;
        int LeftPlayerLives = 3;
        

        public BetaPong()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LeftPaddle = Content.Load<Texture2D>("rodeSpeler");
            RightPaddle = Content.Load<Texture2D>("blauweSpeler");
            Ball = Content.Load<Texture2D>("PokeBall");
            RedHP1 = Content.Load<Texture2D>("PongRedLifeBar");
            RedHP2 = Content.Load<Texture2D>("PongRedLifeBar");
            RedHP3 = Content.Load<Texture2D>("PongRedLifeBar");
            BlueHP1 = Content.Load<Texture2D>("PongBlueLifeBar");
            BlueHP2 = Content.Load<Texture2D>("PongBlueLifeBar");
            BlueHP3 = Content.Load<Texture2D>("PongBlueLifeBar");
            HPBar = Content.Load<Texture2D>("HealthMiddle");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState currentKBState = Keyboard.GetState();
            //GamePadState currentGPState = GamePad.GetState();

            if (currentKBState.IsKeyDown(Keys.W) && LeftPaddleYPosition > 0)   //Reads if w is being pressed and it does not escape the boundaries
            {
                LeftPaddleYPosition = LeftPaddleYPosition - 5;
            }

            if (currentKBState.IsKeyDown(Keys.S) && LeftPaddleYPosition < 384)   //Reads if s is being pressed and it does not escape the boundaries
            {
                LeftPaddleYPosition = LeftPaddleYPosition + 5;
            }
            if (currentKBState.IsKeyDown(Keys.Up) && RightPaddleYPosition > 0)   //Reads if up is being pressed and it does not escape the boundaries
            {
                RightPaddleYPosition = RightPaddleYPosition - 5;
            }
            if (currentKBState.IsKeyDown(Keys.Down) && RightPaddleYPosition < 384)   //Reads if down is being pressed and it does not escape the boundaries
            {
                RightPaddleYPosition = RightPaddleYPosition + 5;
            }
            ballXPosition = ballXPosition + ballXSpeed;
            ballYPosition = ballYPosition + ballYSpeed;
            if (ballYPosition <= 0)
            {
                ballYSpeed = ballYSpeed * -1;
            }
            if (ballYPosition >= 464)
            {
                ballYSpeed = ballYSpeed * -1;
            }
            if (ballXPosition >= 768 && ballYPosition > RightPaddleYPosition - 16 && ballYPosition < RightPaddleYPosition + 80 && ballXSpeed > 0) //collision detection
            {
                ballXSpeed = ballXSpeed * -1 - 1;
                ballYSpeed++;
            }
            if (ballXPosition <= 16 && ballYPosition > LeftPaddleYPosition - 16 && ballYPosition < LeftPaddleYPosition + 80 && ballXSpeed < 0) //collision detection
            {
                ballXSpeed = ballXSpeed * -1 + 1;
                ballYSpeed++;
            }
            if (ballXPosition <= 0)
            {
                LeftPlayerLives--;
                if (LeftPlayerLives == 0)
                {
                    Exit();
                }
                ballYPosition = 232;
                ballXPosition = 392;
                ballXSpeed = -5;
                ballYSpeed = 3;
                LeftPaddleYPosition = 192;
                RightPaddleYPosition = 192;
            }
            if (ballXPosition >= 784)
            {
                RightPlayerLives--;
                if (RightPlayerLives == 0)
                {
                    Exit();
                }
                ballYPosition = 232;
                ballXPosition = 392;
                ballXSpeed = 5;
                ballYSpeed = 3;
                LeftPaddleYPosition = 192;
                RightPaddleYPosition = 192;
            }
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkCyan);
            spriteBatch.Begin();
            Vector2 VHPBar = new Vector2(364,12);
            Vector2 VRedHP1 = new Vector2(298, 12);
            Vector2 VRedHP2 = new Vector2(232, 12);
            Vector2 VRedHP3 = new Vector2(166, 12);
            Vector2 VBlueHP1 = new Vector2(438, 12);
            Vector2 VBlueHP2 = new Vector2(504, 12);
            Vector2 VBlueHP3 = new Vector2(570, 12);
            Vector2 RightPaddleVector = new Vector2(784, RightPaddleYPosition);
            Vector2 LeftPaddleVector = new Vector2(0, LeftPaddleYPosition);
            Vector2 BallVector = new Vector2(ballXPosition, ballYPosition);
            Vector2 SpritePos = new Vector2(400, 10);
            //string pointstring = (LeftPlayerPoints + " " + RightPlayerPoints);
            //SpriteFont punten = Content.Load<SpriteFont>("good_neighbors_xna_0");

            spriteBatch.Draw(RedHP1, VRedHP1);
            if (LeftPlayerLives >= 2)
            {
                spriteBatch.Draw(RedHP2, VRedHP2);
            }
            if (LeftPlayerLives == 3)
            {
                spriteBatch.Draw(RedHP3, VRedHP3);
            }
            spriteBatch.Draw(BlueHP1, VBlueHP1);
            if (RightPlayerLives >= 2)
            {
                spriteBatch.Draw(BlueHP2, VBlueHP2);
            }
            if (RightPlayerLives == 3)
            {
                spriteBatch.Draw(BlueHP3, VBlueHP3);
            }
            spriteBatch.Draw(HPBar, VHPBar);

            spriteBatch.Draw(RightPaddle, RightPaddleVector);
            spriteBatch.Draw(LeftPaddle, LeftPaddleVector);
            spriteBatch.Draw(Ball, BallVector);
            //spriteBatch.DrawString(punten, pointstring, SpritePos, Color.Red);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
