using GamePlay.Projectiles;

namespace GamePlay.Attacks
{
    public class ZoneAttack : AttackBase
    {
        private PlayerDetector _detector;
        private Projectile _visual;

        protected override void ActivateAttack()
        {
            _visual = Instantiate(_projectilePrefab, transform.position, transform.rotation, transform);
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