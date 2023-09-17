using UnityEngine;

namespace GamePlay.AnimationSystem.CharacterAnimations
{
    public class PlayerAnimationManager : CharacterAnimationManagerBase
    {
        private readonly int _movement = Animator.StringToHash("movement");
        private readonly int _attack = Animator.StringToHash("attack");

        public void SetMovement(float value)
        {
            _anim.SetFloat(_movement, value);
        }

        public void Attack()
        {
            _anim.SetTrigger(_attack);
        }
    }
}