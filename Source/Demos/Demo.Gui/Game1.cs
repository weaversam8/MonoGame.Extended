using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.Gui.Drawables;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;

namespace Demo.Gui
{
    public class Game1 : Game
    {
        // ReSharper disable once NotAccessedField.Local
        private GraphicsDeviceManager _graphicsDeviceManager;
        private SpriteBatch _spriteBatch;
        private ViewportAdapter _viewportAdapter;
        private Camera2D _camera;
        //private GuiPanel _panel;
        private GuiManager _guiManager;

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

            _guiManager = new GuiManager(_viewportAdapter);

            var font = Content.Load<BitmapFont>("kenney-future-12");
            var textureAtlas = Content.Load<TextureAtlas>("ui-skin-atlas");

            var buttonStyle = new GuiButtonStyle(
                normal: new GuiNinePatchTemplate(textureAtlas["blue_button07"], 5, 5, 5, 9),
                pressed: new GuiNinePatchTemplate(textureAtlas["red_button04"], 5, 5, 5, 9),
                hovered: new GuiNinePatchTemplate(textureAtlas["blue_button07"], 5, 5, 5, 9) { Color = Color.LightBlue })
            {
                ContentTemplate = new GuiTextTemplate(font),
                HoveredContentTemplate = new GuiTextTemplate(font) { Color = Color.Orange },
                PressedContentTemplate = new GuiTextTemplate(font) { Color = Color.White }
            };
            var button = new GuiButton(buttonStyle)
            {
                Location = new Point(100, 100),
                Size = new Size(150, 42),
                Text = "Hello"
            };
            _guiManager.Controls.Add(button);

            var labelStyle = new GuiLabelStyle(font);
            var label = new GuiLabel(labelStyle, "World!")
            {
                Location = new Point(10, 10),
                Size = new Size(100, 100)
            };
            _guiManager.Controls.Add(label);
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

            _guiManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_guiManager);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
