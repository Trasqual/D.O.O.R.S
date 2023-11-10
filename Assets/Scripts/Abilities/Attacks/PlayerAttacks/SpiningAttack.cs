using GamePlay.Abilities.Attacks;
using GamePlay.Entities;
using GamePlay.StatSystem;
using GamePlay.Visuals;
using GamePlay.Visuals.Projectiles;
using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Attacks
{
    public class SpiningAttack : AbilityBase
    {
        private List<SpiningProjectile> _visuals = new();

        public override void ActivateAbility()
        {
            base.ActivateAbility();
            if (_visuals.Count < _statController.GetStat<ProjectileCountStat>().CurrentValue)
            {
                foreach (var visual in _visuals)
                {
                    visual.ResetVisual();
                }
                Perform();
            }
            else
            {
                foreach (var visual in _visuals)
                {
                    visual.Activate(true);
                }
            }
        }

        public override void DeactivateAbility()
        {
            base.DeactivateAbility();

            foreach (var visual in _visuals)
            {
                visual.Activate(false);
            }
        }

        public override void UpdateAbility()
        {
        }

        protected override void Perform()
        {
            var count = _statController.GetStat<ProjectileCountStat>().CurrentValue;
            for (int i = 0; i < count; i++)
            {
                var visual = LeanPool.Spawn(_statController.GetStat<VisualStat>().Prefab);
                var projectile = visual.GetComponent<Projectile>();
                projectile.transform.position = transform.position + Vector3.up * .5f;
                ((SpiningProjectile)projectile).Init(360f / count * i, transform, _statController, EntityType.Enemy);
                _visuals.Add(projectile as SpiningProjectile);
            }
        }
    }
}