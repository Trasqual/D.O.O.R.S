using System;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.EnemySystem
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private HealthManager _healthManager;

        public Action<Enemy> OnDeath;
        private Transform _target;

        public void Init(Transform target)
        {
            _target = target;
            _healthManager.OnDeath += OnDeathCallback;
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