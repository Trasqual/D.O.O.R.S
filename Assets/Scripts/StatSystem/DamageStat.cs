using GamePlay.EventSystem;
using GamePlay.Rewards.Upgrades;

namespace GamePlay.StatSystem
{
    public class DamageStat : FloatStat
    {
        public DamageStat(string statName, UpgradeTargetType upgradeTargetType) : base(statName, upgradeTargetType) { }

        public override void SubscribeToUpgrade()
        {
            EventManager.Instance.AddListener<DamageUpgrade>(AddUpgrade);
        }

        public override void UnsubscribeToUpgrade()
        {
            EventManager.Instance.RemoveListener<DamageUpgrade>(AddUpgrade);
        }
    }
}