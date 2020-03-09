using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlkalineThunder.DevConsole.Demo
{
    public class DevConsoleGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        DevConsole MyConsole = null;
        
        public DevConsoleGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            MyConsole = new DevConsole(this, new SpriteBatchConsoleRenderer(spriteBatch));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            MyConsole.ConsoleFont = Content.Load<SpriteFont>("ConsoleFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue * 0.25F);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
