using System;

namespace GamePlay.AnimationSystem
{
    public interface IAnimateable
    {
        public void PrepareForAnimation();
        public void Animate(Action OnStart = null, Action OnComplete = null);
    }
}