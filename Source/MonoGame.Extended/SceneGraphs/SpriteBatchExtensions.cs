using System;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Extended.SceneGraphs
{
    public static class SpriteBatchExtensions
    {
        [Obsolete("SceneGraph can be replaced by a SceneNode")]
        public static void Draw(this SpriteBatch spriteBatch, SceneGraph sceneGraph)
        {
            sceneGraph.Draw(spriteBatch);
        }

        public static void Draw(this SpriteBatch spriteBatch, SceneNode sceneNode)
        {
            sceneNode.Draw(spriteBatch);
        }
    }
}