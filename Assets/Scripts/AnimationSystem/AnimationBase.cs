using UnityEngine;

namespace GamePlay.AnimationSystem
{
    public abstract class AnimationBase : MonoBehaviour, IAnimateable
    {
        public abstract void PrepareForAnimation();
        public abstract void Animate();
    }
}