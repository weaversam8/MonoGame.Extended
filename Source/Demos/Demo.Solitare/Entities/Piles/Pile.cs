using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.SceneGraphs;

namespace Demo.Solitare.Entities.Piles
{
    public abstract class Pile
    {
        protected Pile(Vector2 position)
        {
            SceneNode = new SceneNode {Position = position};
        }

        protected SceneNode SceneNode { get; }
        public Vector2 Position => SceneNode.Position;

        public virtual void Add(Card card)
        {
            if (card.Parent != null)
                throw new InvalidOperationException("A card cannot be added to a pile until it's removed from another pile");

            var children = SceneNode.Children;

            while (children.Any())
                children = children[0].Children;

            children.Add(card);
        }

        public Card TakeTop()
        {
            var children = SceneNode.Children;

            while (children.Any())
            {
                var firstChild = children[0];

                if (firstChild.Children.Any())
                {
                    children = firstChild.Children;
                }
                else
                {
                    children.Remove(firstChild);
                    return firstChild as Card;
                }
            }

            return null;
        }

        public Card FindCardAt(Vector2 position)
        {
            return SceneNode.FindNodeAt(position) as Card;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SceneNode.Draw(spriteBatch);
        }
    }
}