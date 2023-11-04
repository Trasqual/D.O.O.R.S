using System;
using UnityEngine;

namespace GamePlay.AnimationSystem.DoorAnimations
{
    public class DoorOpenCloseAnimations : MonoBehaviour, IAnimateable
    {
        [SerializeField] private Animator _anim;

        private bool IsOpen = false;
        private static int _isOpen = Animator.StringToHash("IsOpen");

        public void Animate(Action OnStart = null, Action OnComplete = null)
        {
            _anim.SetBool(_isOpen, IsOpen);
        }

        public void PrepareForAnimation()
        {
            _anim.SetBool(_isOpen, !IsOpen);
        }
    }
}