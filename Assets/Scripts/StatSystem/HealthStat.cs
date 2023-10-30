using GamePlay.EventSystem;
using GamePlay.Rewards.Upgrades;

namespace GamePlay.StatSystem
{
    public class HealthStat : FloatStat
    {
        public HealthStat(string statName) : base(statName) { }

        public override void SubscribeToUpgrade()
        {
            EventManager.Instance.AddListener<HealthUpgrade>(AddUpgrade);
        }

        public override void UnsubscribeToUpgrade()
        {
            EventManager.Instance.RemoveListener<HealthUpgrade>(AddUpgrade);
        }
    }
}