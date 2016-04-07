using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiButtonStyle : GuiControlStyle
    {
        public GuiButtonStyle(IGuiControlTemplate normal)
            : this(normal, normal, normal)
        {
        }

        public GuiButtonStyle(IGuiControlTemplate normal, IGuiControlTemplate pressed)
            : this(normal, pressed, normal)
        {
        }

        public GuiButtonStyle(IGuiControlTemplate normal, IGuiControlTemplate pressed, IGuiControlTemplate hovered)
        {
            Normal = normal;
            Pressed = pressed;
            Hovered = hovered;
            Padding = new GuiThickness(15, 10);
        }

        public IGuiControlTemplate Normal { get; set; }
        public IGuiControlTemplate Pressed { get; set; }
        public IGuiControlTemplate Hovered { get; set; }
        public IGuiControlTemplate ContentTemplate { get; set; }
        public IGuiControlTemplate HoveredContentTemplate { get; set; }
        public IGuiControlTemplate PressedContentTemplate { get; set; }
        public GuiThickness Padding { get; set; }
    }
}