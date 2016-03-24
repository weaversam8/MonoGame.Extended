using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace MonoGame.Extended.Gui.Drawables
{
    public class GuiTextureRegionDrawable : IGuiDrawable
    {
        private readonly TextureRegion2D _region;

        public GuiTextureRegionDrawable(TextureRegion2D region)
        {
            _region = region;

            DesiredSize = new Size(_region.Width, _region.Height);
            Color = Color.White;
        }

        public Color Color { get; set; }
        public Size DesiredSize { get; }

        public void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            var position = rectangle.Center.ToVector2() - new Vector2(_region.Width * 0.5f, _region.Height * 0.5f);
            spriteBatch.Draw(_region, position, Color);
        }
    }
}