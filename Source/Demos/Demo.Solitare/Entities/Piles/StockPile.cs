using System;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.SceneGraphs;
using MonoGame.Extended.Shapes;

namespace Demo.Solitare.Entities.Piles
{
    public class StockPile : Pile
    {
        private readonly Size _size;

        public StockPile(Vector2 position, Size size) 
            : base(position)
        {
            _size = size;
        }

        protected override SceneNode CreateChildNode(bool isFirstChild)
        {
            return new SceneNode {Position = new Vector2(0.25f, 0.25f)};
        }

        public RectangleF BoundingRectangle => new RectangleF(Position, _size);
        public bool IsEmpty => !SceneNode.Children.Any();
    }
}