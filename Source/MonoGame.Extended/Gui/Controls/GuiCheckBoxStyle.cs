using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiCheckBoxStyle : GuiControlStyle
    {
        public GuiCheckBoxStyle(IGuiDrawable checkedOn, IGuiDrawable checkedOff)
           : this(checkedOn, checkedOff, checkedOn)
        {
        }

        public GuiCheckBoxStyle(IGuiDrawable checkedOn, IGuiDrawable checkedOff, IGuiDrawable hovered)
        {
            CheckedOn = checkedOn;
            CheckedOff = checkedOff;
            Hovered = hovered;
        }

        public IGuiDrawable CheckedOn { get; set; }
        public IGuiDrawable CheckedOff { get; set; }
        public IGuiDrawable Hovered { get; set; }
    }
}