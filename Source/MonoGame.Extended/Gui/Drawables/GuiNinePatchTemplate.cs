using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

namespace MonoGame.Extended.Gui.Drawables
{
    public class GuiNinePatchTemplate : IGuiControlTemplate
    {
        public GuiNinePatchTemplate(TextureRegion2D textureRegion, int leftPadding, int topPadding, int rightPadding, int bottomPadding)
        {
            _ninePatch = new NinePatch(textureRegion, leftPadding, topPadding, rightPadding, bottomPadding);
            Color = Color.White;
        }

        private readonly NinePatch _ninePatch;

        public Color Color
        {
            get { return _ninePatch.Color; }
            set { _ninePatch.Color = value; }
        }

        public Size CalculateDesiredSize(GuiControl control)
        {
            return Size.MaxValue;
        }

        public void Draw(SpriteBatch spriteBatch, GuiControl control)
        {
            _ninePatch.Draw(spriteBatch, control.BoundingRectangle);
        }
    }
}