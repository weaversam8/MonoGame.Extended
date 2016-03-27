using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.InputListeners;
using MonoGame.Extended.ViewportAdapters;

namespace MonoGame.Extended.Gui
{
    public class GuiManager : IUpdate
    {
        private readonly InputListenerManager _inputManager;
        private GuiControl _focusedControl;

        public GuiManager(ViewportAdapter viewportAdapter)
        {
            Controls = new List<GuiControl>();

            _inputManager = new InputListenerManager(viewportAdapter);

            var mouseListener = _inputManager.AddListener<MouseListener>();
            mouseListener.MouseMoved += OnMouseMoved;
            mouseListener.MouseDown += (sender, args) => _focusedControl?.OnMouseDown(sender, args);
            mouseListener.MouseUp += (sender, args) => _focusedControl?.OnMouseUp(sender, args);
        }

        public List<GuiControl> Controls { get; }

        private void OnMouseMoved(object sender, MouseEventArgs args)
        {
            var currentControl = FindControlAtPoint(Controls, args.Position);

            if (_focusedControl != currentControl)
            {
                _focusedControl?.OnMouseLeave(this, args);
                _focusedControl = currentControl;
                _focusedControl?.OnMouseEnter(this, args);
            }

            //ForEachChildAtPoint(args.Position, c => c.OnMouseMoved(this, args));
        }

        public void Update(GameTime gameTime)
        {
            _inputManager.Update(gameTime);

            foreach (var control in Controls)
                control.Update(gameTime);
        }

        private void ForEachChildAtPoint(Point point, Action<GuiControl> action)
        {
            foreach (var control in Controls.Where(c => c.Contains(point)))
                action(control);
        }

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
    }
}