using GamePlay.Projectiles;
using GamePlay.StatSystem;

namespace GamePlay.Attacks
{
    public class ZoneAttack : AbilityBase
    {
        private PlayerDetector _detector;
        private Projectile _visual;

        protected override void ActivateAttack()
        {
            base.ActivateAttack();
            var visual = Instantiate(_statController.GetStat<VisualStat>().Prefab, transform.position, transform.rotation, transform);
            _visual = visual.GetComponent<Projectile>();
            _detector = _visual.GetComponentInChildren<PlayerDetector>();
        }

        protected override void Perform()
        {
            for (int i = 0; i < _detector.EnemyCount; i++)
            {
                if (_detector.Enemies[i].TryGetComponent(out HealthManager healthManager))
                {
                    healthManager.TakeDamage(_visual.Damage);
                }
            }
        }
    }
}