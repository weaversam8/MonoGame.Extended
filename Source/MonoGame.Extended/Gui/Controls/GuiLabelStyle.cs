using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiLabelStyle : GuiControlStyle
    {
        public GuiLabelStyle(GuiTextTemplate template)
        {
            Template = template;
        }

        public GuiTextTemplate Template { get; }
    }
}