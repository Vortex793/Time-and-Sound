using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Time_and_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D bombTexture;
        Texture2D explosionTexture;
        Texture2D pliersTexture;
        Rectangle bombRect;
        Rectangle resetButton;
        Rectangle wireRect;
        Rectangle pliersRect;

        SpriteFont timeFont;
        float seconds;
        MouseState mouseState;

        SoundEffect explode;
        SoundEffectInstance explodeInstance;

        bool exploded;
        bool defused = false;
      
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            _graphics.PreferredBackBufferWidth = 800;  
            _graphics.PreferredBackBufferHeight = 500;   
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

           
            bombRect = new Rectangle(50, 50, 700, 400);
            resetButton = new Rectangle(300, 350, 100, 50);
            wireRect = new Rectangle(300, 300, 100, 50);
            pliersRect = new Rectangle(0, 0, 50, 50);

            seconds = 0;
            exploded = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
       

            // TODO: use this.Content to load your game content here
            bombTexture = Content.Load<Texture2D>("bomb");
            timeFont = Content.Load<SpriteFont>("TimeFont");
            explosionTexture = Content.Load<Texture2D>("explosionTexture");
            explode = Content.Load<SoundEffect>("explosion");
            pliersTexture = Content.Load<Texture2D>("pliers");
            explodeInstance = explode.CreateInstance();
       
        

        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            pliersRect.Location = mouseState.Position;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (!exploded)
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (mouseState.LeftButton == ButtonState.Pressed && resetButton.Contains(mouseState.Position))
            {
                seconds = 0f;
                exploded = false;
            }

            if (seconds >= 15 && !exploded && !defused)
            {
                explodeInstance.Play();

                exploded = true;
           
            }

            if (exploded && explodeInstance.State == SoundState.Stopped)
                Exit();

            if (mouseState.LeftButton == ButtonState.Pressed && wireRect.Contains(mouseState.Position))
            {
                defused = true;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (defused)
            {
                _spriteBatch.Draw(bombTexture, bombRect, Color.Green);
                _spriteBatch.DrawString(timeFont, "DEFUSED", new Vector2(270, 200), Color.Green);
            }
            else if (!exploded)
            {
                _spriteBatch.Draw(bombTexture, bombRect, Color.White);
                _spriteBatch.DrawString(timeFont, (15 - seconds).ToString("00.0"), new Vector2(270, 200), Color.Black);
            }
            else
            {
                _spriteBatch.Draw(explosionTexture, bombRect, Color.White);
            }
            _spriteBatch.Draw(pliersTexture, pliersRect, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
