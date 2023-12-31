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
        [SerializeField] protected Transform _wallVisual;
        [SerializeField] private BoxCollider _collider;


        public List<GridCell> GridCells { get; set; } = new();

        public virtual void Initialize(float size)
        {
            var scale = _wallVisual.transform.localScale;
            scale.x *= size;
            _wallVisual.transform.localScale = scale;
            var colSize = _collider.size;
            colSize.x *= size;
            _collider.size = colSize;

            MeshRenderer wallRenderer = _wallVisual.GetComponent<MeshRenderer>();

            MaterialPropertyBlock newBlock = new MaterialPropertyBlock();
            wallRenderer.GetPropertyBlock(newBlock);

            newBlock.SetVector("_BaseMap_ST", new Vector4(size / 6f, 1f, 0.32f, 0f));

            wallRenderer.SetPropertyBlock(newBlock);
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
            var size = _wallVisual.transform.localScale.x;

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