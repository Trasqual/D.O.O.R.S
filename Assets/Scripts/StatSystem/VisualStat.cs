using GamePlay.EventSystem;
using GamePlay.Rewards.Upgrades;
using UnityEngine;

namespace GamePlay.StatSystem
{
    public class VisualStat : StatBase
    {
        public VisualStat(string statName) : base(statName) { }

        [field: SerializeField] public GameObject Prefab { get; private set; }

        public override void AddUpgrade(object upgrade)
        {
            UpgradeBase upgradeBase = upgrade as UpgradeBase;
            if (upgradeBase.TargetType != UpgradeType) return;
            //change projectile
        }

        public override void RemoveUpgrade(object upgrade)
        {
            UpgradeBase upgradeBase = upgrade as UpgradeBase;
            if (upgradeBase.TargetType != UpgradeType) return;
            //change projectile with default or previous projectile
        }

        public override void SubscribeToUpgrade()
        {
            EventManager.Instance.AddListener<VisualUpgrade>(AddUpgrade);
        }

        public override void UnsubscribeToUpgrade()
        {
            EventManager.Instance.RemoveListener<VisualUpgrade>(AddUpgrade);
        }
    }
}