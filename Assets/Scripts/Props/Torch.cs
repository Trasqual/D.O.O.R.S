using DG.Tweening;
using GamePlay.AnimationSystem;
using System;
using UnityEngine;

namespace GamePlay.RoomSystem.Props
{
    public class Torch : MonoBehaviour, IAnimateable
    {
        [SerializeField] private ParticleSystem _flameParticle;
        [SerializeField] private Light _flameLight;

        public void Animate(Action OnStart = null, Action OnComplete = null)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                _flameParticle.Play();
                _flameLight.enabled = true;
            });
        }

        public void PrepareForAnimation()
        {
            _flameParticle.Stop();
            _flameLight.enabled = false;
            transform.localScale = Vector3.zero;
        }
    }
}