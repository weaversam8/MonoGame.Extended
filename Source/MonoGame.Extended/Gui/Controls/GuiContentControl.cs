using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Controls
{
    public abstract class GuiContentControl : GuiControl
    {
        protected GuiContentControl()
        {
            Padding = new GuiThickness(16);
        }

        public GuiThickness Padding { get; set; }

        public abstract IGuiControlTemplate GetCurrentContentTemplate();

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            var contentTemplate = GetCurrentContentTemplate();
            contentTemplate?.Draw(spriteBatch, this);
        }
    }
}