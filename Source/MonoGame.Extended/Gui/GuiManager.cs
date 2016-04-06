using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.InputListeners;
using MonoGame.Extended.ViewportAdapters;

namespace MonoGame.Extended.Gui
{
    public class GuiManager : IUpdate
    {
        private readonly ViewportAdapter _viewportAdapter;
        private readonly InputListenerManager _inputManager;
        private GuiControl _hoveredControl;
        private GuiControl _focusedControl;

        public GuiManager(ViewportAdapter viewportAdapter)
        {
            _viewportAdapter = viewportAdapter;
            Controls = new List<GuiControl>();

            _inputManager = new InputListenerManager(viewportAdapter);

            var mouseListener = _inputManager.AddListener<MouseListener>();
            mouseListener.MouseClicked += OnMouseClicked;
            mouseListener.MouseMoved += OnMouseMoved;
            mouseListener.MouseDown += (sender, args) => _hoveredControl?.OnMouseDown(sender, args);
            mouseListener.MouseUp += (sender, args) => _hoveredControl?.OnMouseUp(sender, args);

            var keyboardListener = _inputManager.AddListener<KeyboardListener>();
            keyboardListener.KeyTyped += (sender, args) => _focusedControl?.OnKeyTyped(sender, args);
        }

        public List<GuiControl> Controls { get; }

        private void OnMouseMoved(object sender, MouseEventArgs args)
        {
            var hoveredControl = FindControlAtPoint(Controls, args.Position);

            if (_hoveredControl != hoveredControl)
            {
                _hoveredControl?.OnMouseLeave(this, args);
                _hoveredControl = hoveredControl;
                _hoveredControl?.OnMouseEnter(this, args);
            }

            //ForEachChildAtPoint(args.Position, c => c.OnMouseMoved(this, args));
        }

        private void OnMouseClicked(object sender, MouseEventArgs mouseEventArgs)
        {
            var focusedControl = FindControlAtPoint(Controls, mouseEventArgs.Position);

            if (_focusedControl != focusedControl)
            {
                if (_focusedControl != null)
                    _focusedControl.IsFocused = false;

                _focusedControl = focusedControl;

                if (_focusedControl != null)
                    _focusedControl.IsFocused = true;
            }
        }

        public void Update(GameTime gameTime)
        {
            _inputManager.Update(gameTime);

            foreach (var control in Controls)
                control.Update(gameTime);
        }

        //private void ForEachChildAtPoint(Point point, Action<GuiControl> action)
        //{
        //    foreach (var control in Controls.Where(c => c.Contains(point)))
        //        action(control);
        //}

        private static GuiControl FindControlAtPoint(IList<GuiControl> controls, Point point)
        {
            for (var i = controls.Count - 1; i >= 0; i--)
            {
                var child = controls[i];

                if (child.Contains(point))
                {
                    var containerControl = child as GuiContainerControl;

                    if (containerControl != null)
                    {
                        var c = FindControlAtPoint(containerControl.Controls, point);

                        if (c != null)
                            return c;
                    }

                    return child;
                }
            }

            return null;
        }

        public void PerformLayout()
        {
            var screenRectangle = new Rectangle(0, 0, _viewportAdapter.VirtualWidth, _viewportAdapter.VirtualHeight);
            PlaceControlCollection(Controls, screenRectangle);
        }

        private static void PlaceControlCollection(IEnumerable<GuiControl> controls, Rectangle rectangle)
        {
            foreach (var control in controls)
            {
                PlaceControl(control, rectangle);

                var containerControl = control as GuiContainerControl;

                if (containerControl != null)
                {
                    var padding = containerControl.Padding;
                    var x = padding.Left;
                    var y = padding.Top;
                    var width = containerControl.Width - padding.Right - padding.Left;
                    var height = containerControl.Height - padding.Bottom - padding.Top;
                    var childRectangle = new Rectangle(x, y, width, height);
                    PlaceControlCollection(containerControl.Controls, childRectangle);
                }
            }
        }

        private static void PlaceControl(GuiControl control, Rectangle rectangle)
        {
            var margin = control.Margin;
            var x = rectangle.X + margin.Left;
            var y = rectangle.Y + margin.Top;
            var width = rectangle.Width - control.Left - margin.Right * 2;
            var height = rectangle.Height - control.Top - margin.Bottom * 2;

            //if (control.HorizontalAlignment != GuiHorizontalAlignment.Stretch)
            //    width = control.Width;

            control.Location = new Point(x, y);
            control.Size = new Size(width, height);
        }

        protected int GetHorizontalAlignment(GuiControl control, Rectangle rectangle)
        {
            switch (control.HorizontalAlignment)
            {
                case GuiHorizontalAlignment.Stretch:
                case GuiHorizontalAlignment.Left:
                    return rectangle.Left;
                case GuiHorizontalAlignment.Right:
                    return rectangle.Right - control.Width;
                case GuiHorizontalAlignment.Center:
                    return rectangle.Left + rectangle.Width / 2 - control.Width / 2;
            }

            throw new NotSupportedException($"{control.HorizontalAlignment} is not supported");
        }

        protected int GetVerticalAlignment(GuiControl control, Rectangle rectangle)
        {
            switch (control.VerticalAlignment)
            {
                case GuiVerticalAlignment.Stretch:
                case GuiVerticalAlignment.Top:
                    return rectangle.Top;
                case GuiVerticalAlignment.Bottom:
                    return rectangle.Bottom - control.Height;
                case GuiVerticalAlignment.Center:
                    return rectangle.Top + rectangle.Height / 2 - control.Height / 2;
            }

            throw new NotSupportedException($"{control.VerticalAlignment} is not supported");
        }
    }
}