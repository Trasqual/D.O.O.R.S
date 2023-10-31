using GamePlay.EventSystem;
using GamePlay.Rewards.Upgrades;

namespace GamePlay.StatSystem
{
    public class ProjectileCountStat : FloatStat
    {
        public ProjectileCountStat(string statName, UpgradeTargetType upgradeTargetType) : base(statName, upgradeTargetType) { }

        public override void SubscribeToUpgrade()
        {
            EventManager.Instance.AddListener<ProjectileCountUpgrade>(AddUpgrade);
        }

        public override void UnsubscribeToUpgrade()
        {
            EventManager.Instance.RemoveListener<ProjectileCountUpgrade>(AddUpgrade);
        }
    }
}