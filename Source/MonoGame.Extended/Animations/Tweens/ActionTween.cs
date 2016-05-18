using System;

namespace MonoGame.Extended.Animations.Tweens
{
    public class ActionTween : Animation
    {
        public ActionTween(Action action)
            : base(action, true)
        {
        }

        protected override bool OnUpdate(float deltaTime)
        {
            return true;
        }
    }
}