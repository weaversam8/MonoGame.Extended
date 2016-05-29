using Demo.Solitare.Entities.Piles;
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

        public Table(Size cardSize)
        {
            _offset = cardSize.Width + _margin;
            _cardSize = cardSize;

            FoundationPiles = SetupFoundationSlots(4);
            TableauPiles = SetupTableauSlots(7);
            StockPile = new StockPile(new Vector2(_margin, _margin));
            WastePile = new WastePile(new Vector2(_margin * 2 + _cardSize.Width, _margin));
        }

        public StockPile StockPile { get; }
        public FoundationPile[] FoundationPiles { get; }
        public TableauPile[] TableauPiles { get; }
        public WastePile WastePile { get; }

        //public Vector2 DrawSlot { get; }
        //public Vector2[] TableauSlots { get; }
        //public FoundationSlot[] FoundationSlots { get; }

        private TableauPile[] SetupTableauSlots(int count)
        {
            var slots = new TableauPile[count];

            for (var i = 0; i < count; i++)
            {
                var position = new Vector2(_margin + i*_offset, _margin*2 + _cardSize.Height);
                slots[i] = new TableauPile(position);
            }

            return slots;
        }

        private FoundationPile[] SetupFoundationSlots(int count)
        {
            var slots = new FoundationPile[count];

            for (var i = 0; i < count; i++)
            {
                var position = new Vector2(_margin + (i + 3)*_offset, _margin);
                var size = _cardSize;
                slots[i] = new FoundationPile(position); //, size);
            }

            return slots;
        }

        public RectangleF GetBoundingRectangle()
        {
            return RectangleF.Empty;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var foundationPile in FoundationPiles)
            {
                spriteBatch.DrawRectangle(foundationPile.Position, _cardSize, Color.DarkGoldenrod, 3);
                foundationPile.Draw(spriteBatch);
            }

            foreach (var tableauPile in TableauPiles)
            {
                spriteBatch.DrawRectangle(tableauPile.Position, _cardSize, Color.DarkGoldenrod);
                tableauPile.Draw(spriteBatch);
            }

            spriteBatch.DrawRectangle(StockPile.Position, _cardSize, Color.DarkGoldenrod);
            StockPile.Draw(spriteBatch);

            WastePile.Draw(spriteBatch);
        }
    }
}