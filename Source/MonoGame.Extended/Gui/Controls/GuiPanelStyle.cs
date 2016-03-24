using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiPanelStyle : GuiControlStyle
    {
        public GuiPanelStyle(IGuiDrawable normal)
        {
            Normal = normal;
        }

        public IGuiDrawable Normal { get; }
    }
}