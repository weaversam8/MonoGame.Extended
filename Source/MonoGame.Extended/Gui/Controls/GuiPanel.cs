using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiPanel : GuiContainerControl
    {
        public GuiPanel(GuiPanelStyle style)
        {
            Style = style;
        }

        public GuiPanelStyle Style { get; }

        protected override IGuiControlTemplate GetCurrentTemplate()
        {
            return Style.Normal;
        }
    }
}