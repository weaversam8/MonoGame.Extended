using Microsoft.Xna.Framework;

namespace Demo.Solitare.Entities.Piles
{
    public class WastePile : Pile
    {
        public WastePile(Vector2 position) 
            : base(position)
        {
        }

        public override void Add(Card card)
        {
            var count = SceneNode.Children.Count;
            card.Position += new Vector2(40 * count, 4 * count);

            base.Add(card);
        }
    }
}