using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.SceneGraphs;
using MonoGame.Extended.Shapes;

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

        public RectangleF DropRectangle => SceneNode.GetBoundingRectangle();

        protected abstract SceneNode CreateChildNode();

        public virtual void Place(Card card)
        {
            if (!SceneNode.Entities.Any())
            {
                SceneNode.Attach(card);
            }
            else
            {
                var children = SceneNode.Children;

                while (children.Any())
                    children = children[0].Children;

                var sceneNode = CreateChildNode();
                sceneNode.Attach(card);
                children.Add(sceneNode);
            }
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
                    return firstChild.Entities.FirstOrDefault() as Card;
                }
            }

            return null;
        }

        public Card FindCardAt(Vector2 position)
        {
            var node = SceneNode.FindNodeAt(position);
            return node?.Entities.FirstOrDefault() as Card;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SceneNode.Draw(spriteBatch);
        }
    }
}