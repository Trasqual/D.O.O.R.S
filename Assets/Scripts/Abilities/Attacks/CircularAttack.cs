using GamePlay.Visuals.Projectiles;
using GamePlay.StatSystem;
using UnityEngine;
using GamePlay.Abilities.Attacks;

namespace GamePlay.Attacks
{
    public class CircularAttack : AbilityBase
    {
        protected override void Perform()
        {
            var count = _statController.GetStat<ProjectileCountStat>().CurrentValue;
            for (int i = 0; i < count; i++)
            {
                var visual = Instantiate(_statController.GetStat<VisualStat>().Prefab);
                var projectile = visual.GetComponent<Projectile>();
                projectile.transform.position = transform.position + Vector3.up * .5f;
                ((DirectProjectile)projectile).Init(Quaternion.AngleAxis(360f / count * i, Vector3.up) * Vector3.forward, _statController);
            }
        }
    }
}