using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Gui.Controls;

namespace MonoGame.Extended.Gui.Drawables
{
    public interface IGuiDrawable
    {
        Size CalculateDesiredSize(GuiControl control);
        void Draw(SpriteBatch spriteBatch, GuiControl control);
    }
}