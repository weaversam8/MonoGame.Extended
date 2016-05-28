using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Extended.SceneGraphs
{
    [Obsolete("Just create a SceneNode")]
    public class SceneGraph
    {
        public SceneGraph()
        {
            RootNode = new SceneNode();
        }

        public SceneNode RootNode { get; }

        public void Draw(SpriteBatch spriteBatch)
        {
            RootNode?.Draw(spriteBatch);
        }

        public SceneNode GetSceneNodeAt(float x, float y)
        {
            return RootNode.FindNodeAt(x, y);
        }

        public SceneNode GetSceneNodeAt(Vector2 position)
        {
            return GetSceneNodeAt(position.X, position.Y);
        }
    }
}