using GamePlay.Visuals.Projectiles;
using GamePlay.StatSystem;
using System.Collections;
using UnityEngine;
using GamePlay.Abilities.Attacks;

namespace GamePlay.Attacks
{
    public class TargetedAttack : AbilityBase
    {
        [SerializeField] private PlayerDetector _detector;
        [SerializeField] private float _delayBetweenProjectiles = 0.1f;

        private WaitForSeconds wait;

        private void Awake()
        {
            wait = new WaitForSeconds(_delayBetweenProjectiles);
        }

        protected override void Update()
        {
            base.Update();

            if (_detector.EnemyCount <= 0)
            {
                _timer = _statController.GetStat<CooldownStat>().CurrentValue;
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
            for (int i = 0; i < _statController.GetStat<ProjectileCountStat>().CurrentValue; i++)
            {
                if (_detector.EnemyCount > 0)
                {
                    var visual = Instantiate(_statController.GetStat<VisualStat>().Prefab);
                    var projectile = visual.GetComponent<Projectile>();
                    projectile.transform.position = transform.position + Vector3.up * 2f;
                    ((TargetedProjectile)projectile).Init(_detector.GetClosestEnemy().transform, _statController);
                    yield return wait;
                }
            }
        }
    }
}