using System;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
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

        public override void Add(Card card)
        {
            if (card.Facing != CardFacing.Down)
                throw new InvalidOperationException("Cards in the stock pile must be face down");

            if(SceneNode.Children.Any())
                card.Position += new Vector2(0.25f, 0.25f);

            base.Add(card);
        }

        public RectangleF BoundingRectangle => new RectangleF(Position, _size);
        public bool IsEmpty => !SceneNode.Children.Any();
    }
}