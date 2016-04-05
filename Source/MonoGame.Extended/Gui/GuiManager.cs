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
            foreach (var control in Controls)
            {
                PlaceControl(control, new Rectangle(0, 0, _viewportAdapter.VirtualWidth, _viewportAdapter.VirtualHeight));
            }
        }

        private static void PlaceControl(GuiControl control, Rectangle rectangle)
        {
            var margin = control.Margin;
            var x = rectangle.X + margin.Left;
            var y = rectangle.Y + margin.Top;
            var width = rectangle.Width - control.Left - margin.Right * 2;
            var height = rectangle.Height - control.Top - margin.Bottom * 2;
            control.Location = new Point(x, y);
            control.Size = new Size(width, height);
        }
    }
}