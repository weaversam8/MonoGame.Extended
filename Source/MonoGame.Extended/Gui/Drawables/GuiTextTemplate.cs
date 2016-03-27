using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Gui.Controls;

namespace MonoGame.Extended.Gui.Drawables
{
    public interface IGuiTextControl
    {
        string Text { get; }
        Color TextColor { get; }
    }

    public class GuiTextTemplate : IGuiControlTemplate
    {
        public GuiTextTemplate(BitmapFont font)
        {
            Font = font;
        }

        public BitmapFont Font { get; }

        public Size CalculateDesiredSize(GuiControl control)
        {
            var textControl = control as IGuiTextControl;
            return textControl != null ? Font.GetSize(textControl.Text) : Size.Empty;
        }

        public void Draw(SpriteBatch spriteBatch, GuiControl control)
        {
            var textControl = control as IGuiTextControl;

            if (textControl != null)
            {
                var halfSize = new Vector2(control.Size.Width, control.Size.Height) * 0.5f;
                var textSize = Font.MeasureString(textControl.Text);
                var position = control.Location.ToVector2() + halfSize - textSize * 0.5f;
                spriteBatch.DrawString(Font, textControl.Text, position, textControl.TextColor);
            }
        }
    }
}