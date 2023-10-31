using GamePlay.StatSystem;
using UnityEngine;

namespace GamePlay.Visuals.Projectiles
{
    public class TargetedProjectile : Projectile
    {
        [SerializeField] private float _speed = 20f;

        private StatController _stats;
        private Transform _target;
        private Vector3 _targetsLastKnownPosition;
        private float _lifeTime = 3f;
        private float _timePassed = 0f;

        public void Init(Transform target, StatController stats)
        {
            _target = target;
            _stats = stats;
        }

        private void Update()
        {
            if (_target != null)
            {
                var targetPos = _target.position + Vector3.up * 0.25f;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, _speed * Time.deltaTime);
                transform.LookAt(targetPos);
                _targetsLastKnownPosition = _target.position;
                if (Vector3.Distance(transform.position, targetPos) < 0.1f)
                {
                    if (_target.TryGetComponent(out HealthManager healthManager))
                    {
                        healthManager.TakeDamage(_stats.GetStat<DamageStat>().CurrentValue);
                    }
                    Destroy(gameObject);
                }

            }
            else if (_targetsLastKnownPosition != Vector3.zero)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetsLastKnownPosition, _speed * Time.deltaTime);
                transform.LookAt(_targetsLastKnownPosition);

                if (Vector3.Distance(transform.position, _targetsLastKnownPosition) < 0.1f)
                {
                    Destroy(gameObject);
                }
            }

            _timePassed += Time.deltaTime;
            if (_timePassed >= _lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }
}