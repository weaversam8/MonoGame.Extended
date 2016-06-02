using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Animations.Tweens;
using MonoGame.Extended.SceneGraphs;
using MonoGame.Extended.Shapes;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

namespace Demo.Solitare.Entities
{
    public class Card : ISceneEntityDrawable, IMovable
    {
        private readonly TextureRegion2D _frontRegion;
        private readonly TextureRegion2D _backRegion;
        private readonly Sprite _sprite;

        public Card(Rank rank, Suit suit, TextureRegion2D frontRegion, TextureRegion2D backRegion)
        {
            Suit = suit;
            Rank = rank;
            Facing = CardFacing.Down;

            _frontRegion = frontRegion;
            _backRegion = backRegion;
            _sprite = new Sprite(backRegion)
            {
                Position = new Vector2(_frontRegion.Width / 2f, _frontRegion.Height / 2f),
                Tag = this
            };
        }

        public Rank Rank { get; }
        public Suit Suit { get; }
        public CardFacing Facing { get; private set; }
        public SuitColor Color => Suit.Color;
        public int Value => Rank.Value;

        public Vector2 Position
        {
            get { return _sprite.Position; }
            set { _sprite.Position = value; }
        }

        public Vector2 Center => _sprite.Position + GetBoundingRectangle().Size/2f;

        public void Flip()
        {
            const float duration = 0.1f;
            
            _sprite.CreateTweenChain()
                .ScaleTo(new Vector2(0.0f, 1.0f), duration, EasingFunctions.QuadraticEaseIn)
                .Run(() =>
                {
                    Facing = Facing == CardFacing.Down ? CardFacing.Up : CardFacing.Down;
                    _sprite.TextureRegion = Facing == CardFacing.Up ? _frontRegion : _backRegion;
                })
                .ScaleTo(Vector2.One, duration, EasingFunctions.QuadraticEaseOut);
        }

        public bool Contains(Point point)
        {
            return _sprite.GetBoundingRectangle()
                .Contains(point);
        }

        public RectangleF GetBoundingRectangle()
        {
            return _sprite.GetBoundingRectangle();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offsetPosition, float offsetRotation, Vector2 offsetScale)
        {
            _sprite.Draw(spriteBatch, offsetPosition, offsetRotation, offsetScale);
        }
    }
}
