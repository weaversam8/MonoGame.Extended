using System.Collections.Generic;
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
        private GuiPanel _panel;
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

            var font = Content.Load<BitmapFont>("montserrat-32");
            var textureAtlas = Content.Load<TextureAtlas>("ui-skin-atlas");
            var panelTemplate = new GuiNinePatchTemplate(textureAtlas["grey_panel"], 16, 16, 16, 16);
            var buttonUpTemplate = new GuiLayeredTemplate
            {
                new GuiNinePatchTemplate(textureAtlas["blue_button07"], 5, 5, 5, 9),
                new GuiTextureRegionTemplate(textureAtlas["grey_crossWhite"]),
                new GuiTextTemplate(font)
            };
            var buttonDownTemplate = new GuiLayeredTemplate
            {
                new GuiNinePatchTemplate(textureAtlas["blue_button08"], 5, 5, 5, 5),
                new GuiTextureRegionTemplate(textureAtlas["grey_box"]),
                new GuiTextTemplate(font)
            };
            var panelStyle = new GuiPanelStyle(panelTemplate);
            var buttonStyle = new GuiButtonStyle(buttonUpTemplate, buttonDownTemplate, buttonDownTemplate);

            _panel = new GuiPanel(panelStyle)
            {
                Location = new Point(100, 100),
                Size = new Size(600, 320),
                Controls =
                {
                     new GuiButton(buttonStyle) { Location = new Point(200, 200), Size = new Size(220, 148), Text = "Hello" }
                }
            };

            _guiManager.Controls.Add(_panel);
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

            _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());
            _spriteBatch.Draw(_guiManager);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
