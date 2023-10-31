using GamePlay.EventSystem;
using GamePlay.Rewards.Upgrades;

namespace GamePlay.StatSystem
{
    public class AreaStat : FloatStat
    {
        public AreaStat(string statName, UpgradeTargetType upgradeTargetType) : base(statName, upgradeTargetType) { }

        public override void SubscribeToUpgrade()
        {
            EventManager.Instance.AddListener<AreaUpgrade>(AddUpgrade);
        }

        public override void UnsubscribeToUpgrade()
        {
            EventManager.Instance.RemoveListener<AreaUpgrade>(AddUpgrade);
        }
    }
}