using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;

namespace Demo.Solitare.Entities
{
    public class Card : IMovable
    {
        public Card(Rank rank, Suit suit, TextureRegion2D frontRegion, TextureRegion2D backRegion)
        {
            Suit = suit;
            Rank = rank;
            _frontRegion = frontRegion;
            _backRegion = backRegion;
        }

        private readonly TextureRegion2D _frontRegion;
        private readonly TextureRegion2D _backRegion;

        public Rank Rank { get; }
        public Suit Suit { get; }

        public Vector2 Position { get; set; }

        private bool _isFlipped;

        public void Flip()
        {
            _isFlipped = !_isFlipped;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_isFlipped ? _frontRegion : _backRegion, Position, Color.White);
        }

        public override string ToString()
        {
            return $"{Rank} {Suit}";
        }
    }
}
