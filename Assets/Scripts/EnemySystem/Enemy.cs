using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.EnemySystem
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        private Transform _target;

        public void Init(Transform target)
        {
            _target = target;
        }

        public void UpdateEnemy()
        {
            _agent.SetDestination(_target.position);
        }
    }
}