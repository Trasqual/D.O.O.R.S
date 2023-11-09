using GamePlay.DetectionSystem;
using GamePlay.Entities;
using GamePlay.Entities.Controllers;
using GamePlay.StatSystem;
using GamePlay.Visuals.Projectiles;
using UnityEngine;

namespace GamePlay.Abilities.Attacks.EnemyAttacks
{
    public class EnemyRangedAttack : AbilityBase
    {
        private EnemyDetector _detector;

        public override void Init(ControllerBase owner)
        {
            base.Init(owner);
            _detector = ((EnemyController)owner).EnemyDetector;
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
            if (_detector.DetectedCount > 0)
            {
                var visual = Instantiate(_statController.GetStat<VisualStat>().Prefab);
                var projectile = visual.GetComponent<Projectile>();
                projectile.transform.position = transform.position + Vector3.up;
                var dir = _detector.Detecteds[0].transform.position - transform.position;
                ((DirectProjectile)projectile).Init(dir.normalized, _statController, EntityType.Player);
            }
        }
    }
}