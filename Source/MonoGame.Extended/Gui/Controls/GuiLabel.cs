using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiLabel : GuiContentControl, IGuiTextControl
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
            return null;
        }

        public override IGuiControlTemplate GetCurrentContentTemplate()
        {
            return Style.TextTemplate;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}