using Microsoft.Xna.Framework;
using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiTextBoxStyle : GuiControlStyle
    {
        public GuiTextBoxStyle(IGuiControlTemplate boxTemplate, GuiTextTemplate textTemplate)
        {
            BoxTemplate = boxTemplate;
            TextTemplate = textTemplate;
            CursorColor = textTemplate.Color;
        }

        public IGuiControlTemplate BoxTemplate { get; set; }
        public GuiTextTemplate TextTemplate { get; set; }
        public Color CursorColor { get; set; }
    }
}