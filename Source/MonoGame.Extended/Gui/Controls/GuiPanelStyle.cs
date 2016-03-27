using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiPanelStyle : GuiControlStyle
    {
        public GuiPanelStyle(IGuiControlTemplate normal)
        {
            Normal = normal;
        }

        public IGuiControlTemplate Normal { get; }
    }
}