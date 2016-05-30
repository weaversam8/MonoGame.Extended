using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Demo.Solitare.Entities.Piles
{
    public class TableauPile : Pile
    {
        public TableauPile(Vector2 position)
            : base(position)
        {
        }

        public override void Add(Card card)
        {
            if(SceneNode.Children.Any())
                card.Position = new Vector2(0, 40);

            base.Add(card);
        }
    }
}