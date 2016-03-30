using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Gui.Controls;

namespace MonoGame.Extended.Gui.Drawables
{
    public interface IGuiTextControl
    {
        string Text { get; }
        Size Size { get; }
        Point Location { get; }
    }

    public class GuiTextTemplate : IGuiControlTemplate
    {
        public GuiTextTemplate(BitmapFont font, Color color)
        {
            Font = font;
            Color = color;
        }

        public GuiTextTemplate(BitmapFont font)
            : this(font, Color.White)
        {
        }

        public BitmapFont Font { get; }
        public Color Color { get; set; }

        public Size CalculateDesiredSize(GuiControl control)
        {
            var textControl = control as IGuiTextControl;
            return textControl != null ? Font.GetSize(textControl.Text) : Size.Empty;
        }

        internal Vector2 GetTextLocation(IGuiTextControl control)
        {
            var halfSize = new Vector2(control.Size.Width, control.Size.Height) * 0.5f;
            var textSize = Font.MeasureString(control.Text);
            var position = control.Location.ToVector2() + halfSize - textSize * 0.5f;
            return position;
        }

        public void Draw(SpriteBatch spriteBatch, GuiControl control)
        {
            var textControl = control as IGuiTextControl;

            if (textControl != null)
            {
                var position = GetTextLocation(textControl);
                spriteBatch.DrawString(Font, textControl.Text, position, Color);
            }
        }
    }
}