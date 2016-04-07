using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Extended.Gui.Controls
{
    public abstract class GuiLayoutControl : GuiControl
    {
        protected GuiLayoutControl()
        {
            HorizontalAlignment = GuiHorizontalAlignment.Stretch;
            VerticalAlignment = GuiVerticalAlignment.Stretch;
            Controls = new GuiControlCollection(this);
        }

        public GuiControlCollection Controls { get; }
        public GuiThickness Padding { get; set; }

        public Rectangle ContentRectangle
        {
            get
            {
                var x = Padding.Left;
                var y = Padding.Top;
                var width = Width - Padding.Right - Padding.Left;
                var height = Height - Padding.Bottom - Padding.Top;
                return new Rectangle(x, y, width, height);
            }
        }

        public abstract void PerformLayout();

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var control in Controls)
                control.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (var control in Controls)
                control.Draw(spriteBatch);
        }

        protected static int GetHorizontalAlignment(GuiHorizontalAlignment alignment, Size size, Rectangle rectangle)
        {
            switch (alignment)
            {
                case GuiHorizontalAlignment.Stretch:
                case GuiHorizontalAlignment.Left:
                    return rectangle.Left;
                case GuiHorizontalAlignment.Right:
                    return rectangle.Right - size.Width;
                case GuiHorizontalAlignment.Center:
                    return rectangle.Left + rectangle.Width / 2 - size.Width / 2;
            }

            throw new NotSupportedException($"{alignment} is not supported");
        }

        protected static Rectangle GetMarginRectangle(GuiControl control, Rectangle clientRectangle)
        {
            var margin = control.Margin;
            var x = clientRectangle.X + margin.Left;
            var y = clientRectangle.Y + margin.Top;
            var width = clientRectangle.Width - control.Left - margin.Right * 2;
            var height = clientRectangle.Height - control.Top - margin.Bottom * 2;
            return new Rectangle(x, y, width, height);
        }

        protected static Size GetDesiredSize(GuiControl control, Rectangle targetRectangle)
        {
            var desiredSize = control.DesiredSize;
            var width = control.HorizontalAlignment == GuiHorizontalAlignment.Stretch ? targetRectangle.Size.X : desiredSize.Width;
            var height = control.VerticalAlignment == GuiVerticalAlignment.Stretch ? targetRectangle.Size.Y : desiredSize.Height;
            return new Size(width, height);
        }

        protected static void PlaceControl(GuiControl control, Rectangle rectangle)
        {
            var targetRectangle = GetMarginRectangle(control, rectangle);
            var desiredSize = GetDesiredSize(control, targetRectangle);
            var x = GetHorizontalAlignment(control.HorizontalAlignment, desiredSize, targetRectangle);
            var y = GetVerticalAlignment(control.VerticalAlignment, desiredSize, targetRectangle);

            control.Location = new Point(x, y);
            control.Size = desiredSize;
        }

        protected static int GetVerticalAlignment(GuiVerticalAlignment alignment, Size size, Rectangle rectangle)
        {
            switch (alignment)
            {
                case GuiVerticalAlignment.Stretch:
                case GuiVerticalAlignment.Top:
                    return rectangle.Top;
                case GuiVerticalAlignment.Bottom:
                    return rectangle.Bottom - size.Height;
                case GuiVerticalAlignment.Center:
                    return rectangle.Top + rectangle.Height / 2 - size.Height / 2;
            }

            throw new NotSupportedException($"{alignment} is not supported");
        }
    }
}