using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiCheckBoxStyle : GuiControlStyle
    {
        public GuiCheckBoxStyle(IGuiControlTemplate checkedOn, IGuiControlTemplate checkedOff)
           : this(checkedOn, checkedOff, checkedOn)
        {
        }

        public GuiCheckBoxStyle(IGuiControlTemplate checkedOn, IGuiControlTemplate checkedOff, IGuiControlTemplate hovered)
        {
            CheckedOn = checkedOn;
            CheckedOff = checkedOff;
            Hovered = hovered;
        }

        public IGuiControlTemplate CheckedOn { get; set; }
        public IGuiControlTemplate CheckedOff { get; set; }
        public IGuiControlTemplate Hovered { get; set; }
    }
}