using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Extended.Gui
{
    public static class SpriteBatchExtensions
    {
        public static void Draw(this SpriteBatch spriteBatch, GuiManager guiManager)
        {
            foreach (var control in guiManager.Controls)
                control.Draw(spriteBatch, Rectangle.Empty);
        }
    }
}