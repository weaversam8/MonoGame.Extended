using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Animations.Tweens;
using MonoGame.Extended.SceneGraphs;
using MonoGame.Extended.Shapes;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

namespace Demo.Solitare.Entities
{
    public class Card : IMovable
    {
        private readonly TextureRegion2D _frontRegion;
        private readonly TextureRegion2D _backRegion;
        private readonly SceneNode _sceneNode;
        private readonly Sprite _sprite;

        public Card(SceneNode parentNode, Rank rank, Suit suit, TextureRegion2D frontRegion, TextureRegion2D backRegion)
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
            _sceneNode = new SceneNode($"{Rank} {Suit}");
            _sceneNode.Entities.Add(_sprite);
            parentNode.Children.Add(_sceneNode);
        }

        public Rank Rank { get; }
        public Suit Suit { get; }
        public CardFacing Facing { get; private set; }
        public SuitColor Color => Suit.Color;
        public int Value => Rank.Value;

        public Vector2 Position
        {
            get { return _sceneNode.Position; }
            set { _sceneNode.Position = value; }
        }

        public void Flip()
        {
            const float duration = 0.1f;
            _sceneNode
                .CreateTweenChain()
                .ScaleTo(new Vector2(0.0f, 1.0f), duration, EasingFunctions.QuadraticEaseIn)
                .Run(() =>
                {
                    Facing = Facing == CardFacing.Down ? CardFacing.Up : CardFacing.Down;
                    _sprite.TextureRegion = Facing == CardFacing.Up ? _frontRegion : _backRegion;
                })
                .ScaleTo(Vector2.One, duration, EasingFunctions.QuadraticEaseOut);
        }

        public override string ToString()
        {
            return _sceneNode.Name;
        }

        public bool Contains(Point point)
        {
            return new Rectangle((int)Position.X, (int)Position.Y, _frontRegion.Size.Width, _frontRegion.Size.Height)
                .Contains(point);
        }

        public RectangleF GetBoundingRectangle()
        {
            return new RectangleF(Position, new Size(_frontRegion.Width, _frontRegion.Height));
        }
    }
}
