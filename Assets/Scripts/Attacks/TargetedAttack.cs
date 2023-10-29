using GamePlay.Projectiles;
using System.Collections;
using UnityEngine;

namespace GamePlay.Attacks
{
    public class TargetedAttack : AbilityBase
    {
        [SerializeField] private PlayerDetector _detector;
        [SerializeField] private int _count = 1;
        [SerializeField] private float _delayBetweenProjectiles = 0.1f;

        protected override void Update()
        {
            base.Update();

            if (_detector.EnemyCount <= 0)
            {
                _timer = _cooldown;
                return;
            }

            base.Update();
        }

        protected override void Perform()
        {
            StartCoroutine(PerformCo());
        }

        private IEnumerator PerformCo()
        {
            for (int i = 0; i < _count; i++)
            {
                if (_detector.EnemyCount > 0)
                {
                    var projectile = Instantiate(_projectilePrefab);
                    projectile.transform.position = transform.position + Vector3.up * 2f;
                    ((TargetedProjectile)projectile).Init(_detector.GetClosestEnemy().transform);
                    yield return new WaitForSeconds(_delayBetweenProjectiles);
                }
            }
        }
    }
}