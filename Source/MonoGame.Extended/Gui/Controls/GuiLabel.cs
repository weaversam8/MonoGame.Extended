using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiLabel : GuiControl
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

        private GuiTextTemplate _template;
        private bool _propertyChanged;

        private GuiLabelStyle _style;
        public GuiLabelStyle Style
        {
            get { return _style; }
            set
            {
                _style = value;
                _propertyChanged = true;
            }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                _propertyChanged = true;
            }
        }

        protected override IGuiControlTemplate GetCurrentTemplate()
        {
            if (_propertyChanged)
            {
                _template = new GuiTextTemplate(Style.Font);
                _propertyChanged = false;
            }

            return _template;
        }
    }
}