using DG.Tweening;
using GamePlay.DetectionSystem;
using GamePlay.Entities;
using GamePlay.Entities.Controllers;
using GamePlay.StatSystem;
using GamePlay.Visuals.Projectiles;
using Lean.Pool;

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
                var target = _detector.Detecteds[0];
                if (((EnemyController)_owner).Anim != null)
                {
                    ((EnemyController)_owner).Anim.Attack();
                }
                DOVirtual.DelayedCall(0.3f, () =>
                {
                    if (target == null) return;

                    var visual = LeanPool.Spawn(_statController.GetStat<VisualStat>().Prefab);
                    var projectile = visual.GetComponent<Projectile>();
                    projectile.transform.position = _owner.SpellSpawnRoot.position;
                    var dir = target.transform.position - transform.position;
                    ((DirectProjectile)projectile).Init(dir.normalized, _statController, EntityType.Player);
                });
            }
        }
    }
}