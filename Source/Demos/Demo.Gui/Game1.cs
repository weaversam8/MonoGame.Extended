using System.Diagnostics;
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

            var panelStyle = new GuiPanelStyle(new GuiNinePatchTemplate(textureAtlas["grey_panel"], 5, 5, 5, 5));
            var panel = new GuiPanel(panelStyle)
            {
                Location = new Point(50, 50),
                Size = new Size(580, 400)
            };
            _guiManager.Controls.Add(panel);

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
                Location = new Point(5, 5),
                Size = new Size(150, 42),
                Text = "Hello"
            };
            panel.Controls.Add(button);

            var labelStyle = new GuiLabelStyle(new GuiTextTemplate(font) { Color = Color.Gray });
            var label = new GuiLabel(labelStyle, "World!")
            {
                Location = new Point(5, 50),
                Size = new Size(150, 42)
            };
            panel.Controls.Add(label);

            var toggleButtonStyle = new GuiToggleButtonStyle(
                checkedOn: new GuiTextureRegionTemplate(textureAtlas["blue_boxCheckmark"]),
                checkedOff: new GuiTextureRegionTemplate(textureAtlas["grey_box"]));
            var toggleButton = new GuiToggleButton(toggleButtonStyle)
            {
                Location = new Point(5, 100),
                Size = textureAtlas["grey_box"].Size
            };
            panel.Controls.Add(toggleButton);

            var roundButtonStyle = new GuiButtonStyle(
                normal: new GuiTextureRegionTemplate(textureAtlas["blue_circle"]),
                pressed: new GuiTextureRegionTemplate(textureAtlas["red_circle"]))
            {
                ContentTemplate = new GuiTextTemplate(font)
            };
            var roundPlusButton = new GuiButton(roundButtonStyle)
            {
                Location = new Point(5, 300),
                Size = textureAtlas["blue_circle"].Size,
                Text = "+"
            };
            panel.Controls.Add(roundPlusButton);

            var roundMinusButton = new GuiButton(roundButtonStyle)
            {
                Location = new Point(50, 300),
                Size = textureAtlas["blue_circle"].Size,
                Text = "-"
            };
            panel.Controls.Add(roundMinusButton);

            var textBoxStyle = new GuiTextBoxStyle(
                boxTemplate: new GuiNinePatchTemplate(textureAtlas["grey_button06"], 5, 5, 5, 5),
                textTemplate: new GuiTextTemplate(font, Color.Black));
            var textBox = new GuiTextBox(textBoxStyle)
            {
                Location = new Point(5, 100),
                Size = new Size(200, 40),
                Text = "edit me."
            };
            panel.Controls.Add(textBox);

            var secondPanel = new GuiPanel(panelStyle) {Location = new Point(300, 200), Size = new Size(200, 100)};
            var secondTextBox = new GuiTextBox(textBoxStyle) {Location = new Point(5, 5), Size = new Size(180, 42)};
            secondPanel.Controls.Add(secondTextBox);
            _guiManager.Controls.Add(secondPanel);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            //var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //var mouseState = Mouse.GetState();
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            _guiManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkBlue);

            _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_guiManager);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
