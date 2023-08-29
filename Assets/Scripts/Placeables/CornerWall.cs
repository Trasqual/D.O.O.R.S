using GridSystem;
using UnityEngine;
using System.Collections.Generic;
using GamePlay.AnimationSystem;
using System;

namespace GamePlay.RoomSystem.Placeables
{
    public class CornerWall : MonoBehaviour, IPlaceable, IAnimateable
    {
        [SerializeField] private AnimationBase _animation;

        public List<GridCell> GridCells { get; set; } = new();

        public void Animate(Action OnComplete)
        {
            _animation.Animate(() =>
            {
                OnComplete?.Invoke();
                //PlayParticles
            });
        }

        public void PrepareForAnimation()
        {
            _animation.PrepareForAnimation();
        }
    }
}