using System;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended.SceneGraphs;

namespace Demo.Solitare.Entities.Piles
{
    public class TableauPile : Pile
    {
        public TableauPile(Vector2 position)
            : base(position)
        {
        }

        protected override SceneNode CreateChildNode(bool isFirstChild)
        {
            return new SceneNode {Position = new Vector2(0, isFirstChild ? 0 : 40)};
        }
    }
}