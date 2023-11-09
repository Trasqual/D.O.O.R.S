using GamePlay.Abilities.Attacks;
using GamePlay.Entities;
using GamePlay.StatSystem;
using GamePlay.Visuals.Projectiles;
using Lean.Pool;
using UnityEngine;

namespace GamePlay.Abilities
{
    public class SpiralAttack : AbilityBase
    {
        protected override void Perform()
        {
            var count = _statController.GetStat<ProjectileCountStat>().CurrentValue;
            for (int i = 0; i < count; i++)
            {
                var visual = LeanPool.Spawn(_statController.GetStat<VisualStat>().Prefab);
                var projectile = visual.GetComponent<Projectile>();
                projectile.transform.position = transform.position + Vector3.up * .5f;
                ((SpiralProjectile)projectile).Init(360f / count * i, transform, _statController, EntityType.Enemy);
            }
        }
    }
}