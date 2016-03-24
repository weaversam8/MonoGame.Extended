using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Extended.Gui.Drawables
{
    public class GuiLayeredDrawable : List<IGuiDrawable>, IGuiDrawable
    {
        public GuiLayeredDrawable()
        {
        }

        public Size DesiredSize
        {
            get
            {
                if (this.Any())
                {
                    var width = this.Max(i => i.DesiredSize.Width);
                    var height = this.Max(i => i.DesiredSize.Height);
                    return new Size(width, height);
                }

                return Size.Empty;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            foreach (var drawable in this)
                drawable.Draw(spriteBatch, rectangle);
        }
    }
}