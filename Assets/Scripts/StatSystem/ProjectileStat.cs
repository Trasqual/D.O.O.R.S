using GamePlay.Projectiles;
using GamePlay.EventSystem;
using GamePlay.UpgradeSystem;
using UnityEngine;

namespace GamePlay.StatSystem
{
    public class ProjectileStat : StatBase
    {
        public ProjectileStat(string statName) : base(statName) { }

        [field: SerializeField] public Projectile Prefab { get; private set; }

        public override void AddUpgrade(object upgrade)
        {
            UpgradeBase upgradeBase = upgrade as UpgradeBase;
            //change projectile
        }

        public override void RemoveUpgrade(object upgrade)
        {
            UpgradeBase upgradeBase = upgrade as UpgradeBase;
            //change projectile with default or previous projectile
        }

        public override void SubscribeToUpgrade()
        {
            EventManager.Instance.AddListener<ProjectileUpgrade>(AddUpgrade);
        }

        public override void UnsubscribeToUpgrade()
        {
            EventManager.Instance.RemoveListener<ProjectileUpgrade>(AddUpgrade);
        }
    }
}