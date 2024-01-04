using DG.Tweening;
using System;
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
        [SerializeField] private MeshRenderer[] _meshes;

        private Sequence _sequence;

        public override void PrepareForAnimation()
        {
            Debug.Log("Preparing For Animation: " + gameObject.name, gameObject);
            for (int i = 0; i < _meshes.Length; i++)
            {
                _meshes[i].material.DOFade(0f, 0f);
                _meshes[i].transform.localPosition += new Vector3(0f, _fallStartPosition, 0f);
            }
        }

        public override void Animate(Action OnStart = null, Action OnComplete = null)
        {
            _sequence = DOTween.Sequence();
            for (int i = 0; i < _meshes.Length; i++)
            {
                _sequence.Insert(0f, _meshes[i].material.DOFade(1f, _fadeInTime));
                _sequence.Insert(0f, _meshes[i].transform.DOLocalMoveY(_fallEndPosition, _fallDuration).SetEase(_fallEase));
            }
            _sequence.OnStart(() => OnStart?.Invoke()).OnComplete(() => OnComplete?.Invoke());
        }
    }
}