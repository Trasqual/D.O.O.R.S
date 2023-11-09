using GamePlay.Visuals.Projectiles;
using GamePlay.StatSystem;
using GamePlay.Visuals;
using GamePlay.Abilities.Attacks;
using GamePlay.DetectionSystem;
using Lean.Pool;

namespace GamePlay.Attacks
{
    public class ZoneAttack : AbilityBase
    {
        private PlayerDetector _detector;
        private Visual _visual;

        public override void ActivateAbility()
        {
            base.ActivateAbility();
            if (_visual == null)
            {
                var visual = LeanPool.Spawn(_statController.GetStat<VisualStat>().Prefab, transform.position, transform.rotation, transform);
                _visual = visual.GetComponent<Visual>();
                _detector = _visual.GetComponentInChildren<PlayerDetector>();
                ((ZoneProjectile)_visual).UpdateSize(_statController.GetStat<RangeStat>().CurrentValue);
            }
            else
            {
                ((ZoneProjectile)_visual).Enable(true);
            }
        }

        public override void DeactivateAbility()
        {
            base.DeactivateAbility();

            if (_visual == null) return;

            ((ZoneProjectile)_visual).Enable(false);
        }

        protected override void Perform()
        {
            for (int i = 0; i < _detector.DetectedCount; i++)
            {
                if (_detector.Detecteds[i].TryGetComponent(out HealthManager healthManager))
                {
                    healthManager.TakeDamage(_statController.GetStat<DamageStat>().CurrentValue);
                }
            }
        }
    }
}