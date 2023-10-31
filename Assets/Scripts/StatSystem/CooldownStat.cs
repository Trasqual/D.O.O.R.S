using GamePlay.EventSystem;
using GamePlay.Rewards.Upgrades;

namespace GamePlay.StatSystem
{
    public class CooldownStat : FloatStat
    {
        public CooldownStat(string statName, UpgradeTargetType upgradeTargetType) : base(statName, upgradeTargetType) { }

        public override void SubscribeToUpgrade()
        {
            EventManager.Instance.AddListener<CooldownUpgrade>(AddUpgrade);
        }

        public override void UnsubscribeToUpgrade()
        {
            EventManager.Instance.RemoveListener<CooldownUpgrade>(AddUpgrade);
        }
    }
}