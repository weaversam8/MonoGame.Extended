using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace Demo.Gui
{
    public class Game1 : Game
    {
        // ReSharper disable once NotAccessedField.Local
        private GraphicsDeviceManager _graphicsDeviceManager;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        private ViewportAdapter _viewportAdapter;
        private Camera2D _camera;
        //private GuiManager _guiManager;
        //private GuiButton _button;

        public Game1()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void LoadContent()
        {
            _viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
            _camera = new Camera2D(_viewportAdapter);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = Content.Load<Texture2D>("ui-skin-texture");

            //_guiManager = new GuiManager(_viewportAdapter, GraphicsDevice);
            //var buttonStyle = new GuiButtonStyle(
            //    Content.Load<Texture2D>("button-normal").ToGuiDrawable(),
            //    Content.Load<Texture2D>("button-clicked").ToGuiDrawable(),
            //    Content.Load<Texture2D>("button-hover").ToGuiDrawable());
            //_button = new GuiButton(buttonStyle);
            //_guiManager.Layout.Children.Add(_button);
            //_guiManager.PerformLayout();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var mouseState = Mouse.GetState();
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();
            
            //_guiManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, Vector2.One, Color.White);
            _spriteBatch.End();
            //_guiManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
