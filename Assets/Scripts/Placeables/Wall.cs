using GamePlay.AnimationSystem;
using GridSystem;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.RoomSystem.Placeables
{
    public class Wall : MonoBehaviour, IPlaceable, IAnimateable
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