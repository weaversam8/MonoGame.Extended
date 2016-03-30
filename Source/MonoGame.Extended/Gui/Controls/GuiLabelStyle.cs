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