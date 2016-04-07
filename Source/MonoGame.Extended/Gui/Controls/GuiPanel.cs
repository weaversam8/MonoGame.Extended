using System.Linq;
using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiPanel : GuiLayoutControl
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

        public override Size DesiredSize
        {
            get
            {
                var width = Controls.Max(c => c.Width);
                var height = Controls.Max(c => c.Height);
                return new Size(width, height);
            }
        }

        public override void PerformLayout()
        {
        }
    }
}