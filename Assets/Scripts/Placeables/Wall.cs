using GamePlay.AnimationSystem;
using GridSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.RoomSystem.Placeables
{
    public class Wall : MonoBehaviour, IPlaceable, IAnimateable
    {
        [SerializeField] private AnimationBase _animation;
        [SerializeField] private Transform _wallVisual;
        [SerializeField] private BoxCollider _collider;

        public List<GridCell> GridCells { get; set; } = new();

        public void Initialize(float size)
        {
            var scale = _wallVisual.transform.localScale;
            scale.x *= size;
            _wallVisual.transform.localScale = scale;
            var colSize = _collider.size;
            colSize.x *= size;
            _collider.size = colSize;
            PrepareForAnimation();
        }

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