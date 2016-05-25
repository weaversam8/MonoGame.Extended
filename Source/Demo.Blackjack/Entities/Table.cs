using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.SceneGraphs;
using MonoGame.Extended.Shapes;

namespace Demo.Solitare.Entities
{
    public class Table : ISceneEntityDrawable
    {
        private const int _margin = 30;
        private readonly int _offset;
        private readonly Size _cardSize;

        public Table(Size cardSize)
        {
            _offset = cardSize.Width + _margin;
            _cardSize = cardSize;
            FoundationSlots = SetupFoundationSlots(4);
            TableauSlots = SetupTableauSlots(7);
            DrawSlot = new Vector2(_margin, _margin);
        }

        public Vector2 DrawSlot { get; }
        public Vector2[] TableauSlots { get; }
        public FoundationSlot[] FoundationSlots { get; }

        private Vector2[] SetupTableauSlots(int count)
        {
            var slots = new Vector2[count];

            for (var i = 0; i < count; i++)
                slots[i] = new Vector2(_margin + i * _offset, _margin * 2 + _cardSize.Height);

            return slots;
        }

        private FoundationSlot[] SetupFoundationSlots(int count)
        {
            var slots = new FoundationSlot[count];

            for (var i = 0; i < count; i++)
            {
                var position = new Vector2(_margin + (i + 3)*_offset, _margin);
                var size = _cardSize;
                slots[i] = new FoundationSlot(position, size);
            }

            return slots;
        }

        public RectangleF GetBoundingRectangle()
        {
            return RectangleF.Empty;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offsetPosition, float offsetRotation, Vector2 offsetScale)
        {
            foreach (var foundationSlot in FoundationSlots)
                spriteBatch.DrawRectangle(foundationSlot.Position, foundationSlot.Size, Color.DarkGoldenrod, 3);

            foreach (var tableauSlot in TableauSlots)
                spriteBatch.DrawRectangle(tableauSlot, _cardSize, Color.DarkGoldenrod);

            spriteBatch.DrawRectangle(DrawSlot, _cardSize, Color.DarkGoldenrod);
        }
    }
}