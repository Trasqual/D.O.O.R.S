using GamePlay.Visuals.Projectiles;
using GamePlay.StatSystem;
using System.Collections;
using UnityEngine;
using GamePlay.Abilities.Attacks;
using GamePlay.DetectionSystem;
using GamePlay.Entities.Controllers;

namespace GamePlay.Attacks
{
    public class TargetedAttack : AbilityBase
    {
        [SerializeField] private float _delayBetweenProjectiles = 0.1f;

        private PlayerDetector _detector;
        private WaitForSeconds wait;

        private void Awake()
        {
            wait = new WaitForSeconds(_delayBetweenProjectiles);
        }

        public override void Init(ControllerBase owner)
        {
            base.Init(owner);
            _detector = ((PlayerController)owner).PlayerDetector;
        }

        public override void UpdateAbility()
        {
            if (_detector.DetectedCount <= 0)
            {
                _timer = _statController.GetStat<CooldownStat>().CurrentValue;
                return;
            }

            base.UpdateAbility();
        }

        protected override void Perform()
        {
            StartCoroutine(PerformCo());
        }

        private IEnumerator PerformCo()
        {
            for (int i = 0; i < _statController.GetStat<ProjectileCountStat>().CurrentValue; i++)
            {
                if (_detector.DetectedCount > 0)
                {
                    var visual = Instantiate(_statController.GetStat<VisualStat>().Prefab);
                    var projectile = visual.GetComponent<Projectile>();
                    projectile.transform.position = transform.position + Vector3.up * 2f;
                    ((TargetedProjectile)projectile).Init(_detector.GetClosestDetected().transform, _statController);
                    yield return wait;
                }
            }
        }
    }
}