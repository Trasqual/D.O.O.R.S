using System;
using UnityEngine;

namespace GamePlay.AnimationSystem.DoorAnimations
{
    public class DoorOpenCloseAnimations : MonoBehaviour
    {
        [SerializeField] private Animator _anim;

        private static int _isOpen = Animator.StringToHash("IsOpen"); 
        private static int _setOpen = Animator.StringToHash("SetOpen");

        public void Animate(bool isOpen)
        {
            _anim.SetBool(_isOpen, isOpen);
        }
        
        public void SetState(bool isOpen)
        {
            _anim.SetBool(_isOpen, isOpen);
            _anim.SetBool(_setOpen, isOpen);
        }
    }
}