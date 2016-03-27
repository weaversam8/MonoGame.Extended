using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.InputListeners;

namespace MonoGame.Extended.Gui.Controls
{
    public abstract class GuiContainerControl : GuiControl
    {
        protected GuiContainerControl()
        {
            Controls = new List<GuiControl>();
        }

        public List<GuiControl> Controls { get; }

        public override void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            base.Draw(spriteBatch, rectangle);

            foreach (var control in Controls)
                control.Draw(spriteBatch, rectangle);
        }
    }
}