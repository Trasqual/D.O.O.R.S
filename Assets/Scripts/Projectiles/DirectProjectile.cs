using GamePlay.EnemySystem;
using GamePlay.StatSystem;
using UnityEngine;

namespace GamePlay.Visuals.Projectiles
{
    public class DirectProjectile : Projectile
    {
        [SerializeField] private float _speed = 20f;
        [SerializeField] private float _maxDistance = 50f;

        private StatController _stats;
        private Vector3 _startPos;
        private Vector3 _direction;

        public void Init(Vector3 direction, StatController stats)
        {
            _direction = direction;
            _startPos = transform.position;
            _stats = stats;
        }

        private void Update()
        {
            transform.position += _speed * Time.deltaTime * _direction;
            transform.rotation = Quaternion.LookRotation(_direction);

            if (Vector3.Distance(transform.position, _startPos) >= _maxDistance)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyController enemy))
            {
                if (enemy.TryGetComponent(out HealthManager healthManager))
                {
                    healthManager.TakeDamage(_stats.GetStat<DamageStat>().CurrentValue);
                    Destroy(gameObject);
                }
            }
        }
    }
}