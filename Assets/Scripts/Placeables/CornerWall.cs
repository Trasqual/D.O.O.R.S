using GridSystem;
using UnityEngine;
using System.Collections.Generic;
using GamePlay.AnimationSystem;
using System;
using GamePlay.Particles;

namespace GamePlay.RoomSystem.Placeables
{
    public class CornerWall : MonoBehaviour, IPlaceable, IAnimateable
    {
        [SerializeField] private AnimationBase _animation;

        public List<GridCell> GridCells { get; set; } = new();

        public void Animate(Action OnStart = null, Action OnComplete = null)
        {
            _animation.Animate(null, () =>
            {
                OnComplete?.Invoke();
                PlaySpawnParticles();
            });
        }

        public void PrepareForAnimation()
        {
            _animation.PrepareForAnimation();
        }

        private void PlaySpawnParticles()
        {
            var particle = ParticleManager.Instance.GetWallSpawnParticle();
            particle.transform.SetParent(transform);
            particle.transform.localPosition = Vector3.zero;
            particle.Play();
        }
    }
}