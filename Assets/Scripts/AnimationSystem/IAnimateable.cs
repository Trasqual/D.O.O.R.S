using System;

namespace GamePlay.AnimationSystem
{
    public interface IAnimateable
    {
        public void PrepareForAnimation();
        public void Animate(Action OnComplete);
    }
}