using GamePlay.AnimationSystem;
using GamePlay.Particles;
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
        }

        public void Animate(Action OnComplete)
        {
            _animation.Animate(() =>
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
            var size = _wallVisual.transform.localScale.x;
            var count = Mathf.CeilToInt(size / 3f);
            var particlePositions = ObjectSpreader.GetLineSpreadPosition(size, count);

            for (int i = 0; i < count; i++)
            {
                var particle = ParticleManager.Instance.GetWallSpawnParticle();
                particle.transform.SetParent(transform);
                particle.transform.localPosition = new Vector3(particlePositions[i], 0f, 0f);
                particle.Play();
            }
        }
    }
}