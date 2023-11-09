using GamePlay.Abilities.Management;
using GamePlay.AnimationSystem.CharacterAnimations;
using GamePlay.DetectionSystem;
using Lean.Pool;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Entities.Controllers
{
    public class EnemyController : ControllerBase
    {
        [SerializeField] private NavMeshAgent _agent;
        [field: SerializeField] public EnemyDetector EnemyDetector { get; private set; }
        [field: SerializeField] public PlayerAnimationManager Anim { get; private set; }
        [SerializeField] private AbilityHandlerBase _abilityHandler;
        [SerializeField] private AbilityData _attack;

        public Action<EnemyController> OnDeath;
        private Transform _target;

        public void Init(Transform target)
        {
            _target = target;
            _agent.enabled = true;
            _healthManager.OnDeath += OnDeathCallback;
            _abilityHandler.GainAbility(_attack);
            _abilityHandler.StartAbilities(null);
        }

        public void UpdateEnemy()
        {
            if (_target == null) return;
            _agent.SetDestination(_target.position);
            transform.LookAt(_target.position);
            _abilityHandler.UpdateAbilities();
            if (Anim != null)
                Anim.SetMovement(Mathf.Clamp01(_agent.velocity.magnitude));
        }

        private void OnDeathCallback()
        {
            _target = null;
            OnDeath?.Invoke(this);
            _abilityHandler.StopAbilities(null);
            LeanPool.Despawn(this);
        }

        private void OnDisable()
        {
            _healthManager.OnDeath -= OnDeathCallback;
        }
    }
}