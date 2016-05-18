using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Animations.Tweens;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

namespace Demo.Solitare.Entities
{
    public class Card : IMovable
    {
        private readonly TextureRegion2D _frontRegion;
        private readonly TextureRegion2D _backRegion;
        private readonly Vector2 _positionOffset;

        public Card(Rank rank, Suit suit, TextureRegion2D frontRegion, TextureRegion2D backRegion)
        {
            _frontRegion = frontRegion;
            _backRegion = backRegion;
            _positionOffset = new Vector2(_frontRegion.Width / 2f, _frontRegion.Height / 2f);
            _sprite = new Sprite(backRegion);

            Suit = suit;
            Rank = rank;
            Facing = CardFacing.Down;
        }

        private readonly Sprite _sprite;

        public Rank Rank { get; }
        public Suit Suit { get; }
        public CardFacing Facing { get; private set; }

        public Vector2 Position
        {
            get { return _sprite.Position - _positionOffset; }
            set { _sprite.Position = value + _positionOffset; }
        }

        public void Flip()
        {
            const float duration = 0.1f;
            _sprite
                .CreateTweenChain()
                .ScaleTo(new Vector2(0.0f, 1.0f), duration, EasingFunctions.QuadraticEaseIn)
                .Run(() =>
                {
                    Facing = Facing == CardFacing.Down ? CardFacing.Up : CardFacing.Down;
                    _sprite.TextureRegion = Facing == CardFacing.Up ? _frontRegion : _backRegion;
                })
                .ScaleTo(Vector2.One, duration, EasingFunctions.QuadraticEaseOut);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite);
        }

        public override string ToString()
        {
            return $"{Rank} {Suit}";
        }

        public bool Contains(int x, int y)
        {
            return new Rectangle((int)Position.X, (int)Position.Y, _frontRegion.Size.Width, _frontRegion.Size.Height)
                .Contains(x, y);
        }
    }
}
