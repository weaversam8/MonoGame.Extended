using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Layouts
{
    public class GuiContainerLayout : GuiLayoutControl
    {
        protected override IGuiControlTemplate GetCurrentTemplate()
        {
            return null;
        }

        public override Size DesiredSize
        {
            get
            {
                var width = Controls.Max(c => c.DesiredSize.Width);
                var height = Controls.Max(c => c.DesiredSize.Height);
                return new Size(width, height);
            }
        }

        public override void PerformLayout()
        {
            PlaceControlCollection(Controls, ContentRectangle);
        }

        private static void PlaceControlCollection(IEnumerable<GuiControl> controls, Rectangle rectangle)
        {
            foreach (var control in controls)
            {
                PlaceControl(control, rectangle);

                var layoutControl = control as GuiLayoutControl;

                if (layoutControl != null)
                {
                    PlaceControlCollection(layoutControl.Controls, layoutControl.ContentRectangle);
                    layoutControl.PerformLayout();
                }
            }
        }
    }
}