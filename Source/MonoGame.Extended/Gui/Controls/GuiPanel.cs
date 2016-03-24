using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiPanel : GuiControl
    {
        public GuiPanel(GuiPanelStyle style)
        {
            Style = style;
        }

        public GuiPanelStyle Style { get; }

        protected override IGuiDrawable GetCurrentDrawable()
        {
            return Style.Normal;
        }
    }
}