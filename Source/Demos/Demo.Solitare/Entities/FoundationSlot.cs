using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;

namespace Demo.Solitare.Entities
{
    public class FoundationSlot
    {
        public FoundationSlot(Vector2 position, Size size)
        {
            _cards = new Stack<Card>();

            Position = position;
            Size = size;
        }

        private readonly Stack<Card> _cards;

        public Vector2 Position { get; }
        public Size Size { get; }
        public RectangleF BoundingRectangle => new RectangleF(Position, Size);
         
        public bool TryDrop(Card card)
        {
            if (!_cards.Any())
            {
                if (card.Rank == Rank.Ace)
                {
                    _cards.Push(card);
                    return true;
                }

                return false;
            }

            var topCard = _cards.Peek();

            if (card.Suit == topCard.Suit && topCard.Rank.Value + 1 == card.Rank.Value)
            {
                _cards.Push(card);
                return true;
            }

            return false;
        }
    }
}