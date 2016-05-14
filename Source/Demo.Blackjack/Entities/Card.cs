using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.SceneGraphs;
using MonoGame.Extended.TextureAtlases;

namespace Demo.Blackjack.Entities
{
    public class Card 
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_frontRegion, Vector2.Zero, Color.White);
        }

        public override string ToString()
        {
            return $"{Rank} {Suit}";
        }
    }
}
