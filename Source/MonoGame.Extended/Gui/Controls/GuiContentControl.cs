using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Controls
{
    public abstract class GuiContentControl : GuiControl
    {
        protected GuiContentControl()
        {
            Padding = new GuiThickness(2);
        }

        public GuiThickness Padding { get; set; }

        public abstract IGuiControlTemplate GetCurrentContentTemplate();

        public override Size DesiredSize
        {
            get
            {
                var contentTemplate = GetCurrentContentTemplate();
                var contentSize = contentTemplate.CalculateDesiredSize(this);
                var width = contentSize.Width + Padding.Left + Padding.Right;
                var height = contentSize.Height + Padding.Top + Padding.Bottom;
                return new Size(width, height);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            var contentTemplate = GetCurrentContentTemplate();
            contentTemplate?.Draw(spriteBatch, this);
        }
    }
}