using GamePlay.DetectionSystem;
using GamePlay.Entities.Controllers;
using GamePlay.StatSystem;

namespace GamePlay.Abilities.Attacks.EnemyAttacks
{
    public class EnemyMeleeAttack : AbilityBase
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
                ((EnemyController)_owner).Anim.Attack();
                _detector.Detecteds[0].TakeDamage(_statController.GetStat<DamageStat>().CurrentValue);
            }
        }
    }
}