using GamePlay.Visuals.Projectiles;
using GamePlay.StatSystem;
using GamePlay.Visuals;
using GamePlay.Abilities.Attacks;

namespace GamePlay.Attacks
{
    public class ZoneAttack : AbilityBase
    {
        private PlayerDetector _detector;
        private Visual _visual;

        protected override void ActivateAttack()
        {
            base.ActivateAttack();
            var visual = Instantiate(_statController.GetStat<VisualStat>().Prefab, transform.position, transform.rotation, transform);
            _visual = visual.GetComponent<Visual>();
            _detector = _visual.GetComponentInChildren<PlayerDetector>();
            ((ZoneProjectile)_visual).UpdateSize(_statController.GetStat<RangeStat>().CurrentValue);
        }

        protected override void Perform()
        {
            for (int i = 0; i < _detector.EnemyCount; i++)
            {
                if (_detector.Enemies[i].TryGetComponent(out HealthManager healthManager))
                {
                    healthManager.TakeDamage(_statController.GetStat<DamageStat>().CurrentValue);
                }
            }
        }
    }
}