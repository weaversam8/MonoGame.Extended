using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.TextureAtlases;

namespace MonoGame.Extended.Gui.Drawables
{
    public class GuiTextureRegionTemplate : IGuiControlTemplate
    {
        private readonly TextureRegion2D _region;

        public GuiTextureRegionTemplate(TextureRegion2D region)
        {
            _region = region;

            Color = Color.White;
        }

        public Color Color { get; set; }

        public void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
        {

        }

        public Size CalculateDesiredSize(GuiControl control)
        {
            return new Size(_region.Width, _region.Height);
        }

        public void Draw(SpriteBatch spriteBatch, GuiControl control)
        {
            var position = control.Center - new Vector2(_region.Width * 0.5f, _region.Height * 0.5f);
            spriteBatch.Draw(_region, position, Color);
        }
    }
}