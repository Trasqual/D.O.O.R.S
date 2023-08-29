using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GamePlay.AnimationSystem
{
    public class AppearAnimation : AnimationBase
    {
        [SerializeField] private float _fadeInTime = 0.25f;
        [SerializeField] private float _fallDuration = 0.5f;
        [SerializeField] private float _fallStartPosition = 5f;
        [SerializeField] private float _fallEndPosition = 1f;
        [SerializeField] private Ease _fallEase = Ease.OutBounce;
        [SerializeField] private ParticleSystem _wallAppearParticlePrefab;
        [SerializeField] private float _minParticleDistance = 3f;
        [SerializeField] private MeshRenderer[] _meshes;

        private List<ParticleSystem> _particles = new();
        private Sequence _sequence;

        private void Awake()
        {
            var wallSize = transform.localScale.x;
            var particleCount = Mathf.CeilToInt(wallSize / _minParticleDistance);

            for (int i = 0; i < particleCount; i++)
            {
                var particle = Instantiate(_wallAppearParticlePrefab, transform.position, Quaternion.identity, transform);
                var pos = particle.transform.localPosition;
                pos.x = -wallSize / 2f + i * _minParticleDistance;
                particle.transform.localPosition = pos;
                _particles.Add(particle);
            }
        }

        public override void PrepareForAnimation()
        {
            for (int i = 0; i < _meshes.Length; i++)
            {
                _meshes[i].material.DOFade(0f, 0f);
                _meshes[i].transform.localPosition += new Vector3(0f, _fallStartPosition, 0f);
            }
        }

        public override void Animate(Action OnComplete)
        {
            _sequence = DOTween.Sequence();
            for (int i = 0; i < _meshes.Length; i++)
            {
                _sequence.Append(_meshes[i].material.DOFade(1f, _fadeInTime));
                _sequence.Join(_meshes[i].transform.DOLocalMoveY(_fallEndPosition, _fallDuration).SetEase(_fallEase));
            }
            _sequence.OnComplete(PlayLandingParticles);
        }

        private void PlayLandingParticles()
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                _particles[i].Play();
            }
        }
    }
}