using System;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.Gui.Drawables;

namespace MonoGame.Extended.Gui.Layouts
{
    public class GuiStackLayout : GuiLayoutControl
    {
        public GuiStackLayout()
        {
            HorizontalAlignment = GuiHorizontalAlignment.Center;
            VerticalAlignment = GuiVerticalAlignment.Center;
            Orientation = GuiOrientation.Vertical;
        }

        public GuiOrientation Orientation { get; set; }

        protected override IGuiControlTemplate GetCurrentTemplate()
        {
            return null;
        }

        public override Size DesiredSize
        { 
            get
            {
                var width = Orientation == GuiOrientation.Vertical
                    ? Controls.Max(i => i.DesiredSize.Width)
                    : Controls.Sum(i => i.DesiredSize.Width);
                var height = Orientation == GuiOrientation.Horizontal
                    ? Controls.Max(i => i.DesiredSize.Height)
                    : Controls.Sum(i => i.DesiredSize.Height);
                return new Size(width, height);
            }
        }

        public override void PerformLayout()
        {
            switch (Orientation)
            {
                case GuiOrientation.Vertical:
                    LayoutVertical();
                    break;
                case GuiOrientation.Horizontal:
                    LayoutHorizontal();
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"{Orientation}");
            }
        }

        private void LayoutHorizontal()
        {
            var x = 0;

            foreach (var control in Controls)
            {
                var desiredSize = control.DesiredSize;
                control.Location = new Point(x, 0);
                control.Size = desiredSize;
                x += desiredSize.Width;
            }
        }

        private void LayoutVertical()
        {
            var y = 0;

            foreach (var control in Controls)
            {
                var desiredSize = control.DesiredSize;
                control.Location = new Point(0, y);
                control.Size = desiredSize;
                y += desiredSize.Height;
            }
        }
    }
}