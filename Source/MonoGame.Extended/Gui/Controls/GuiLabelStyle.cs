using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiLabelStyle : GuiControlStyle
    {
        public GuiLabelStyle(GuiTextTemplate textTemplate)
        {
            TextTemplate = textTemplate;
        }

        public GuiTextTemplate TextTemplate { get; }
    }
}