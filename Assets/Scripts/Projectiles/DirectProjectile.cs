using GamePlay.Entities;
using GamePlay.Entities.Controllers;
using GamePlay.StatSystem;
using System;
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
        private EntityType _targetType;

        public void Init(Vector3 direction, StatController stats, EntityType targetType)
        {
            _direction = direction;
            _startPos = transform.position;
            _stats = stats;
            _targetType = targetType;
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
            if (other.TryGetComponent(out ControllerBase enemy))
            {
                if (enemy.EntityType != _targetType) return;

                if (enemy.TryGetComponent(out HealthManager healthManager))
                {
                    healthManager.TakeDamage(_stats.GetStat<DamageStat>().CurrentValue);
                    Destroy(gameObject);
                }
            }
        }
    }
}