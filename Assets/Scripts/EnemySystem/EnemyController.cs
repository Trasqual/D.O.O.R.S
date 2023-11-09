using GamePlay.Abilities.Management;
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
        [SerializeField] private AbilityHandlerBase _abilityHandler;
        [SerializeField] private AbilityData _attack;

        public Action<EnemyController> OnDeath;
        private Transform _target;

        public void Init(Transform target)
        {
            _target = target;
            _healthManager.OnDeath += OnDeathCallback;
            _abilityHandler.GainAbility(_attack);
            _abilityHandler.StartAbilities(null);
        }

        public void UpdateEnemy()
        {
            _agent.SetDestination(_target.position);
            _abilityHandler.UpdateAbilities();
        }

        private void OnDeathCallback()
        {
            _target = null;
            OnDeath?.Invoke(this);
            LeanPool.Despawn(this);
        }

        private void OnDestroy()
        {
            _healthManager.OnDeath -= OnDeathCallback;
        }
    }
}