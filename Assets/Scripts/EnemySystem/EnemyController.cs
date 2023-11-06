using GamePlay.Abilities.Attacks;
using GamePlay.Abilities.Management;
using GamePlay.DetectionSystem;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.EnemySystem
{
    public class EnemyController : ControllerBase
    {
        [SerializeField] private NavMeshAgent _agent;
        [field: SerializeField] public EnemyDetector EnemyDetector { get; private set; }
        [SerializeField] private AbilityHandler _abilityHandler;
        [SerializeField] private AbilityData _attack;

        public Action<EnemyController> OnDeath;
        private Transform _target;

        public void Init(Transform target)
        {
            _target = target;
            _healthManager.OnDeath += OnDeathCallback;
            _abilityHandler.GainAbility(_attack);
        }

        public void UpdateEnemy()
        {
            _agent.SetDestination(_target.position);
        }

        private void OnDeathCallback()
        {
            OnDeath?.Invoke(this);
        }

        private void OnDestroy()
        {
            _healthManager.OnDeath -= OnDeathCallback;
        }
    }
}