using Microsoft.Xna.Framework;

namespace MonoGame.Extended.InputListeners
{
    public abstract class InputListenerComponent : GameComponent
    {
        protected InputListenerComponent(Game game)
            : base(game)
        {
        }
    }
}