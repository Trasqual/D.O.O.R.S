using GridSystem;
using UnityEngine;
using System.Collections.Generic;
using GamePlay.AnimationSystem;

namespace GamePlay.RoomSystem.Placeables
{
    public class CornerWall : MonoBehaviour, IPlaceable, IAnimateable
    {
        [SerializeField] private AnimationBase _animation;

        public List<GridCell> GridCells { get; set; } = new();

        public void Animate()
        {
            _animation.Animate();
        }

        public void PrepareForAnimation()
        {
            _animation.PrepareForAnimation();
        }
    }
}