using GamePlay.Projectiles;
using GamePlay.StatSystem;
using UnityEngine;

namespace GamePlay.AttackSystem
{
    public abstract class AttackBase : MonoBehaviour
    {
        [SerializeField] protected AttackData _data;
        [SerializeField] protected Transform _spawnPoint;

        protected Projectile _projectilePrefab;

        protected virtual void Awake()
        {
            _data.OnInitialized += InitializeStats;
        }

        private void OnDisable()
        {
            _data.OnInitialized -= InitializeStats;
        }

        protected virtual void InitializeStats()
        {
            _projectilePrefab = _data.GetStat<ProjectileStat>().Prefab;
        }

        protected virtual void Attack()
        {
        }
    }
}