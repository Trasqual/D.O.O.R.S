using System;
using UnityEngine;

namespace GamePlay.AnimationSystem
{
    public abstract class AnimationBase : MonoBehaviour, IAnimateable
    {
        public abstract void PrepareForAnimation();
        public abstract void Animate(Action OnStart = null, Action OnComplete = null);
    }
}