using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson__6_keyboard_and_mouse_events
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D pacTexture, pacUpTexture, pacDownTexture, pacRightTexture, pacLeftTexture, pacSleepTexture;
        Rectangle pacLocation;
        Vector2 pacSpeed;
        KeyboardState keyboardState;
        Rectangle window;
        MouseState mouseboardState;
        int speed;
        SpriteFont font;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            window = new Rectangle(0,0,800,600);
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.ApplyChanges();
            pacLocation = new Rectangle(10, 10, 75, 75);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            pacUpTexture = Content.Load<Texture2D>("PacUp");
            pacDownTexture = Content.Load<Texture2D>("PacDown");
            pacLeftTexture = Content.Load<Texture2D>("PacLeft");
            pacRightTexture = Content.Load<Texture2D>("PacRight");
            pacSleepTexture = Content.Load<Texture2D>("PacSleep");
            pacTexture = pacSleepTexture;
            font = Content.Load<SpriteFont>("info");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            keyboardState = Keyboard.GetState();
            pacSpeed = new Vector2();
            mouseboardState = Mouse.GetState();
            speed = 2;

            if (keyboardState.IsKeyDown(Keys.G))
            {
                speed -= 1;
            }
            if (keyboardState.IsKeyDown(Keys.H))
            {
                speed += 1;
            }
            if (speed <= 0)
            {
                speed = 1;
            }
            pacSpeed = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                pacTexture = pacUpTexture;
                pacSpeed.Y -= speed;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                pacTexture = pacDownTexture;
                pacSpeed.Y += speed;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                pacTexture = pacRightTexture;
                pacSpeed.X += speed;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                pacTexture = pacLeftTexture;
                pacSpeed.X -= speed;
            }
            if (!keyboardState.IsKeyDown(Keys.Up) && !keyboardState.IsKeyDown(Keys.Right) && !keyboardState.IsKeyDown(Keys.Left) && !keyboardState.IsKeyDown(Keys.Down))
            {
                pacTexture = pacSleepTexture;
            }
            if (pacLocation.Bottom < 0)
            {
                pacLocation.Y = window.Height;
            }
            if (pacLocation.Bottom > window.Height + 76)
            {
                pacLocation.Y = -74;
            }
            if (pacLocation.Right < 0)
            {
                pacLocation.X = window.Width;
            }
            if (pacLocation.Right > window.Width + 76)
            {
                pacLocation.X = -74;
            }
            

            if (mouseboardState.LeftButton == ButtonState.Pressed)
            {
                pacLocation.X = mouseboardState.X - 37;
                pacLocation.Y = mouseboardState.Y - 37;
            }
            pacLocation.X += (int)pacSpeed.X;
            pacLocation.Y += (int)pacSpeed.Y;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(pacTexture, pacLocation, Color.White);
            _spriteBatch.DrawString(font, "Hold 'G' to go slower and 'H' to speed up", new Vector2(5,5), Color.White);
            _spriteBatch.DrawString(font, "Arrow keys to move and click to teleport", new Vector2(5, 23), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
