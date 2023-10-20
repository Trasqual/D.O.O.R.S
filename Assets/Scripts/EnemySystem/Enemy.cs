using System;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.EnemySystem
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private HealthManager _healthManager;

        private Action<Enemy> _onDeath;
        private Transform _target;

        public void Init(Transform target, Action<Enemy> deathAction)
        {
            _onDeath = deathAction;
            _healthManager.OnDeath += OnDeath;
            _target = target;
        }

        public void UpdateEnemy()
        {
            _agent.SetDestination(_target.position);
        }

        private void OnDeath()
        {
            _onDeath?.Invoke(this);
        }

        private void OnDestroy()
        {
            _healthManager.OnDeath -= OnDeath;
        }
    }
}