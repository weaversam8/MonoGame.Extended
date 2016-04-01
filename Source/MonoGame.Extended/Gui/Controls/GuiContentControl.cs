using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Controls
{
    public abstract class GuiContentControl : GuiControl
    {
        public abstract IGuiControlTemplate GetCurrentContentTemplate();

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            var contentTemplate = GetCurrentContentTemplate();
            contentTemplate?.Draw(spriteBatch, this);
        }
    }
}