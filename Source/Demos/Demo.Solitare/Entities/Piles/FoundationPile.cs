using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.SceneGraphs;
using MonoGame.Extended.Shapes;

namespace Demo.Solitare.Entities.Piles
{
    public class FoundationPile : Pile
    {
        public FoundationPile(Vector2 position) 
            : base(position)
        {
        }

        protected override SceneNode CreateChildNode()
        {
            return new SceneNode();
        }
    }
}