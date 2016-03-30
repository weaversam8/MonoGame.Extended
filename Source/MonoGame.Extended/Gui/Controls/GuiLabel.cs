using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiLabel : GuiControl, IGuiTextControl
    {
        public GuiLabel(GuiLabelStyle style)
            : this(style, string.Empty)
        {
        }

        public GuiLabel(GuiLabelStyle style, string text)
        {
            Style = style;
            Text = text;
        }

        public GuiLabelStyle Style { get; set; }
        public string Text { get; set; }

        protected override IGuiControlTemplate GetCurrentTemplate()
        {
            return Style.Template;
        }
    }
}