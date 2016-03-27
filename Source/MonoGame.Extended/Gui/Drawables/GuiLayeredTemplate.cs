using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Gui.Controls;

namespace MonoGame.Extended.Gui.Drawables
{
    public class GuiLayeredTemplate : List<IGuiControlTemplate>, IGuiControlTemplate
    {
        public GuiLayeredTemplate()
        {
        }

        public Size CalculateDesiredSize(GuiControl control)
        {
            if (this.Any())
            {
                var width = this.Max(i => i.CalculateDesiredSize(control).Width);
                var height = this.Max(i => i.CalculateDesiredSize(control).Height);
                return new Size(width, height);
            }

            return Size.Empty;
        }

        public void Draw(SpriteBatch spriteBatch, GuiControl control)
        {
            foreach (var drawable in this)
                drawable.Draw(spriteBatch, control);
        }
    }
}