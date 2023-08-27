using GamePlay.AnimationSystem;
using GridSystem;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.RoomSystem.Placeables
{
    public class Wall : MonoBehaviour, IPlaceable, IAnimateable
    {
        public List<GridCell> GridCells { get; set; } = new();

        private AnimationBase _animation;

        public void Animate()
        {
            _animation.Animate();
        }
    }
}