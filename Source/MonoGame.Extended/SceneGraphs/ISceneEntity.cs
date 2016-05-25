using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Shapes;

namespace MonoGame.Extended.SceneGraphs
{
    public interface ISceneEntity
    {
        RectangleF GetBoundingRectangle();
    }

    public interface ISceneEntityDrawable : ISceneEntity
    {
        /// <summary>
        /// Draws a scene entity as part of a scene graph transformed into world space.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to draw with</param>
        /// <param name="offsetPosition">Add the offset position to the local position</param>
        /// <param name="offsetRotation">Add the offset rotation to the local rotation</param>
        /// <param name="offsetScale">Multiply the offset scale by the local scale</param>
        void Draw(SpriteBatch spriteBatch, Vector2 offsetPosition, float offsetRotation, Vector2 offsetScale);
    }
}