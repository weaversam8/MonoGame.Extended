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
    public class Card : ISceneEntityDrawable//, IMovable
    {
        private readonly TextureRegion2D _frontRegion;
        private readonly TextureRegion2D _backRegion;
        private TextureRegion2D _currentRegion;

        public Card(SceneNode sceneNode, Rank rank, Suit suit, TextureRegion2D frontRegion, TextureRegion2D backRegion)
        {
            Suit = suit;
            Rank = rank;
            Facing = CardFacing.Down;

            _frontRegion = frontRegion;
            _backRegion = backRegion;
            _currentRegion = _backRegion;
        }
        
        public Rank Rank { get; }
        public Suit Suit { get; }
        public CardFacing Facing { get; private set; }
        public SuitColor Color => Suit.Color;
        public int Value => Rank.Value;

        public void Flip()
        {
            const float duration = 0.1f;

            Facing = Facing == CardFacing.Down ? CardFacing.Up : CardFacing.Down;
            _currentRegion = Facing == CardFacing.Up ? _frontRegion : _backRegion;

            //_sceneNode.CreateTweenChain()
            //    .ScaleTo(new Vector2(0.0f, 1.0f), duration, EasingFunctions.QuadraticEaseIn)
            //    .Run(() =>
            //    {
            //        Facing = Facing == CardFacing.Down ? CardFacing.Up : CardFacing.Down;
            //        _currentRegion = Facing == CardFacing.Up ? _frontRegion : _backRegion;
            //    })
            //    .ScaleTo(Vector2.One, duration, EasingFunctions.QuadraticEaseOut);
        }

        //public bool Contains(Point point)
        //{
        //    return _sceneNode.GetBoundingRectangle()
        //        .Contains(point);
        //}

        public RectangleF GetBoundingRectangle()
        {
            return new RectangleF(0, 0, _currentRegion.Width, _currentRegion.Height);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offsetPosition, float offsetRotation, Vector2 offsetScale)
        {
            spriteBatch.Draw(texture: _currentRegion.Texture, sourceRectangle: _currentRegion.Bounds,
                position: offsetPosition, rotation: offsetRotation, scale: offsetScale);
        }
    }
}
