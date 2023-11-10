using GamePlay.Entities.Controllers;
using GamePlay.Entities;
using GamePlay.StatSystem;
using Lean.Pool;
using UnityEngine;

namespace GamePlay.Visuals.Projectiles
{
    public class SpiningProjectile : Projectile
    {
        [SerializeField] private float _speed = 20f;

        private StatController _stats;
        private EntityType _targetType;
        private Transform _centerPos;
        private float _angle;
        private float _radius;

        public void Init(float angle, Transform center, StatController stats, EntityType targetType)
        {
            _centerPos = center;
            _angle = angle * Mathf.Deg2Rad;
            _stats = stats;
            _targetType = targetType;
            _radius = stats.GetStat<RangeStat>().CurrentValue;
        }

        private void Update()
        {
            var dir = new Vector3(Mathf.Cos(_angle) * _radius, 0f, Mathf.Sin(_angle) * _radius);
            transform.position = _centerPos.position + Vector3.up * 0.5f + dir;
            transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.position - _centerPos.position, Vector3.up));

            _angle += Time.deltaTime * _speed;
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

        public void Activate(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void ResetVisual()
        {
            _radius = 0f;
            _angle = 0f;
            _targetType = EntityType.None;
            LeanPool.Despawn(this);
        }
    }
}