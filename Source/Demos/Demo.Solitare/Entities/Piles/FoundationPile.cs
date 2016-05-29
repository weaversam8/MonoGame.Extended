using System;
using Microsoft.Xna.Framework;

namespace Demo.Solitare.Entities.Piles
{
    public class FoundationPile : Pile
    {
        public FoundationPile(Vector2 position) 
            : base(position)
        {
        }

        public override void Add(Card card)
        {
            //if (card.Facing != CardFacing.Up)
                //throw new InvalidOperationException("Cards in the foundation piles must be face up");

            base.Add(card);
        }
    }
}