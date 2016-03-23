using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Extended.Gui.Drawables
{
    public interface IGuiDrawable
    {
        Size DesiredSize { get; }
        void Draw(SpriteBatch spriteBatch, Rectangle rectangle);
    }
}