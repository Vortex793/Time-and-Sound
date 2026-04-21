using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Time_and_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D bombTexture;
        Rectangle bombRect;
        SpriteFont timeFont;
        float seconds;
        MouseState mouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 800;  
            _graphics.PreferredBackBufferHeight = 500;   
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            bombRect = new Rectangle(50, 50, 700, 400);

            seconds = 0;
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            timeFont = Content.Load<SpriteFont>("TimeFont");

            // TODO: use this.Content to load your game content here
            bombTexture = Content.Load<Texture2D>("bomb");

        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (seconds > 10)
                    seconds = 0f;

            if (mouseState.LeftButton == ButtonState.Pressed)
                seconds = 0f;



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(bombTexture, bombRect, Color.White);
            _spriteBatch.DrawString(timeFont, seconds.ToString("00.0"), new Vector2(270, 200), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
