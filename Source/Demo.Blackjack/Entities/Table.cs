using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;

namespace Demo.Solitare.Entities
{
    public class Table
    {
        private const int _margin = 30;
        private readonly int _offset;
        private readonly Size _cardSize;
        private readonly Vector2[] _foundationSlots;
        private readonly Vector2[] _tableauSlots;
        private readonly Vector2 _drawSlot;

        public Table(int width, int height, Size cardSize)
        {
            _offset = cardSize.Width + _margin;
            _cardSize = cardSize;
            _foundationSlots = SetupFoundationSlots(4);
            _tableauSlots = SetupTableauSlots(7);
            _drawSlot = new Vector2(_margin, _margin);
        }

        public Vector2 DrawSlot => _drawSlot;

        private Vector2[] SetupTableauSlots(int count)
        {
            var slots = new Vector2[count];

            for (var i = 0; i < count; i++)
                slots[i] = new Vector2(_margin + i * _offset, _margin * 2 + _cardSize.Height);

            return slots;
        }

        private Vector2[] SetupFoundationSlots(int count)
        {
            var slots = new Vector2[count];

            for (var i = 0; i < count; i++)
                slots[i] = new Vector2(_margin + (i + 3) * _offset, _margin);

            return slots;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var foundationSlot in _foundationSlots)
                spriteBatch.DrawRectangle(foundationSlot, _cardSize, Color.DarkGoldenrod);

            foreach (var tableauSlot in _tableauSlots)
                spriteBatch.DrawRectangle(tableauSlot, _cardSize, Color.DarkGoldenrod);

            spriteBatch.DrawRectangle(_drawSlot, _cardSize, Color.DarkGoldenrod);
        }
    }
}