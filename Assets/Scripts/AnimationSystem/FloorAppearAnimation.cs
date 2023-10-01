using DG.Tweening;
using System;
using UnityEngine;

namespace GamePlay.AnimationSystem
{
    public class FloorAppearAnimation : AnimationBase
    {
        private Vector3 _originalScale;

        public override void Animate(Action OnComplete)
        {
            transform.DOScale(_originalScale, 0.5f).SetEase(Ease.OutBack).OnComplete(() => OnComplete?.Invoke());
        }

        public override void PrepareForAnimation()
        {
            _originalScale = transform.localScale;
            transform.localScale = Vector3.zero;
        }
    }
}