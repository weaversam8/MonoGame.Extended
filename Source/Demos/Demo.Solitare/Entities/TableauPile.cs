using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.SceneGraphs;

namespace Demo.Solitare.Entities
{
    public class TableauPile
    {
        private readonly SceneNode _sceneNode = new SceneNode();

        public TableauPile(Vector2 position)
        {
            _sceneNode.Position = position;
        }

        public void Add(Card card)
        {
            if (card.Parent != null)
                throw new InvalidOperationException("A card cannot be added to a pile until it's removed from another pile");

            card.Position = new Vector2(0, 40* _sceneNode.Children.Count);

            var children = _sceneNode.Children;

            while (children.Any())
            {
                children = children[0].Children;
            }

            children.Add(card);
        }

        public Card FindCardAt(Vector2 position)
        {
            return _sceneNode.FindNodeAt(position) as Card;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _sceneNode.Draw(spriteBatch);
        }
    }
}