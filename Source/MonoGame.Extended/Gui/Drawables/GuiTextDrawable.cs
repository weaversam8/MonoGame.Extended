using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

namespace MonoGame.Extended.Gui.Drawables
{
    public class GuiTextDrawable : IGuiDrawable
    {
        public GuiTextDrawable(BitmapFont font, string text, Color color)
        {
            Font = font;
            Text = text;
            Color = color;
        }

        public BitmapFont Font { get; }
        public string Text { get; }
        public Color Color { get; }

        public Size DesiredSize => Font.GetSize(Text);

        public void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            spriteBatch.DrawString(Font, Text, rectangle.Location.ToVector2(), Color);
        }
    }
}