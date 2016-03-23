using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Gui.Drawables;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;

namespace Demo.Gui
{
    public abstract class GuiControlBase
    {
        protected GuiControlBase()
        {
            Children = new List<GuiControlBase>();
        }

        public int Top => Location.Y;
        public int Left => Location.X;
        public int Right => Location.X + Width;
        public int Bottom => Location.Y + Height;
        public int Width => Size.Width;
        public int Height => Size.Height;
        public Rectangle Rectangle => new Rectangle(Location, Size);
        public Point Location { get; set; }
        public Size Size { get; set; }
        public bool IsHovered { get; set; }

        public List<GuiControlBase> Children { get; }

        public abstract void Draw(SpriteBatch spriteBatch);
    }

    public class GuiPanel : GuiControlBase
    {
        public GuiPanel(IGuiDrawable style)
        {
            Style = style;
        }

        public IGuiDrawable Style { get; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Style.Draw(spriteBatch, Rectangle);
        }
    }

    public class GuiButtonControl : GuiControlBase
    {
        public GuiButtonControl(IGuiDrawable upStyle, IGuiDrawable downStyle)
        {
            UpStyle = upStyle;
            DownStyle = downStyle;
        }

        public IGuiDrawable UpStyle { get; }
        public IGuiDrawable DownStyle { get; }

        

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsHovered)
                DownStyle.Draw(spriteBatch, Rectangle);
            else
                UpStyle.Draw(spriteBatch, Rectangle);
        }
    }


    public class Game1 : Game
    {
        // ReSharper disable once NotAccessedField.Local
        private GraphicsDeviceManager _graphicsDeviceManager;
        private SpriteBatch _spriteBatch;
        private ViewportAdapter _viewportAdapter;
        private Camera2D _camera;
        private GuiPanel _panel;

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

            var textureAtlas = Content.Load<TextureAtlas>("ui-skin-atlas");
            var panelDrawable = new GuiNinePatchDrawable(textureAtlas["grey_panel"], 16, 16, 16, 16);
            var buttonUpDrawable = new GuiNinePatchDrawable(textureAtlas["blue_button07"], 5, 5, 5, 9);
            var buttonDownDrawable = new GuiNinePatchDrawable(textureAtlas["blue_button08"], 5, 5, 5, 5);

            _panel = new GuiPanel(panelDrawable)
            {
                Location = new Point(100, 100),
                Size = new Size(600, 260),
                Children =
                {
                    new GuiButtonControl(buttonUpDrawable, buttonDownDrawable)
                    {
                        Location = new Point(520, 300),
                        Size = new Size(170, 50)
                    }
                }
            };
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

            foreach (var child in _panel.Children)
                child.IsHovered = child.Rectangle.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed;


            //_guiManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());

            _panel.Draw(_spriteBatch);

            foreach (var child in _panel.Children)
                child.Draw(_spriteBatch);

            _spriteBatch.End();
            //_guiManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
