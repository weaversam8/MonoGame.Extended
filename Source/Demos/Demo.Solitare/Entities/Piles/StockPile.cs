using System;
using Microsoft.Xna.Framework;

namespace Demo.Solitare.Entities.Piles
{
    public class StockPile : Pile
    {
        public StockPile(Vector2 position) 
            : base(position)
        {
        }

        public override void Add(Card card)
        {
            if (card.Facing != CardFacing.Down)
                throw new InvalidOperationException("Cards in the stock pile must be face down");

            var offset = 0.25f * SceneNode.Children.Count;
            card.Position += new Vector2(offset, offset);

            base.Add(card);
        }
    }
}