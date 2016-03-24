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

    public class GuiTextDrawable : IGuiDrawable
    {
        public GuiTextDrawable(BitmapFont font)
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
                spriteBatch.DrawString(Font, textControl.Text, control.Location.ToVector2(), textControl.TextColor);
        }
    }
}