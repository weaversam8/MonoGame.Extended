using Microsoft.Xna.Framework;
using MonoGame.Extended.SceneGraphs;

namespace Demo.Solitare.Entities.Piles
{
    public class WastePile : Pile
    {
        public WastePile(Vector2 position) 
            : base(position)
        {
        }

        protected override SceneNode CreateChildNode(bool isFirstChild)
        {
            return new SceneNode();
        }
    }
}