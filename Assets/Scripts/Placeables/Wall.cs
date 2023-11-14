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
        [SerializeField] protected Transform[] _wallVisuals;
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private Renderer[] _renderers;


        public List<GridCell> GridCells { get; set; } = new();

        public virtual void Initialize(float size)
        {
            foreach (var wall in _wallVisuals)
            {
                wall.localScale = new Vector3(wall.localScale.x * size, wall.localScale.y, wall.localScale.z);

            }
            foreach (var rend in _renderers)
            {
                rend.material.mainTextureScale = new Vector2(_wallVisuals[0].localScale.x / 3f, 1f);
            }
            var colSize = _collider.size;
            colSize.x *= size;
            _collider.size = colSize;
        }

        public void Animate(Action OnStart = null, Action OnComplete = null)
        {
            _animation.Animate(null, () =>
            {
                OnComplete?.Invoke();
                PlaySpawnParticles();
            });
        }

        public virtual void PrepareForAnimation()
        {
            _animation.PrepareForAnimation();
        }

        protected virtual void PlaySpawnParticles()
        {
            var size = _wallVisuals[0].localScale.x;

            var particle = ParticleManager.Instance.GetWallSpawnParticle();
            particle.transform.SetParent(transform);
            particle.transform.localPosition = Vector3.zero;
            particle.transform.rotation = transform.rotation;

            var childSystems = particle.GetComponentsInChildren<ParticleSystem>();
            foreach (var part in childSystems)
            {
                var t = Mathf.InverseLerp(1, 70, size);
                var emission = part.emission;
                var burst = emission.GetBurst(0);
                burst.count = Mathf.CeilToInt(Mathf.Lerp(30f, 200f, t));
                emission.SetBurst(0, burst);

                var shape = part.shape;
                shape.scale = new Vector3(size, 2f, 0f);
            }

            particle.Play();
        }
    }
}