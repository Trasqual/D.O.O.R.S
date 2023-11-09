using GamePlay.Entities;
using GamePlay.Entities.Controllers;
using GamePlay.StatSystem;
using Lean.Pool;
using UnityEngine;

namespace GamePlay.Visuals.Projectiles
{
    public class SpiralProjectile : Projectile
    {
        [SerializeField] private float _speed = 20f;
        [SerializeField] private float _lifeTime = 4f;

        private StatController _stats;
        private EntityType _targetType;
        private Transform _centerPos;
        private float _angle;
        private float _radius;
        private float _timePassed;

        public void Init(float angle, Transform center, StatController stats, EntityType targetType)
        {
            _centerPos = center;
            _angle = angle * Mathf.Deg2Rad;
            _stats = stats;
            _targetType = targetType;
        }

        private void Update()
        {
            var dir = new Vector3(_speed * Mathf.Cos(_angle) * _radius, 0f, _speed * Mathf.Sin(_angle) * _radius);
            transform.position = _centerPos.position + Vector3.up * 0.5f + dir;
            transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.position - _centerPos.position, Vector3.up));

            _angle += Time.deltaTime * 3f;
            _radius += Time.deltaTime * 0.2f;

            _timePassed += Time.deltaTime;
            if (_timePassed >= _lifeTime)
            {
                ResetVisual();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ControllerBase enemy))
            {
                if (enemy.EntityType != _targetType) return;

                if (enemy.TryGetComponent(out HealthManager healthManager))
                {
                    healthManager.TakeDamage(_stats.GetStat<DamageStat>().CurrentValue);
                }
            }
        }

        private void ResetVisual()
        {
            _timePassed = 0f;
            _radius = 0f;
            _angle = 0f;
            _targetType = EntityType.None;
            LeanPool.Despawn(this);
        }
    }
}