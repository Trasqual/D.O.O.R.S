using GamePlay.EventSystem;
using GamePlay.Rewards.Upgrades;

namespace GamePlay.StatSystem
{
    public class RangeStat : FloatStat
    {
        public RangeStat(string statName) : base(statName) { }

        public override void SubscribeToUpgrade()
        {
            EventManager.Instance.AddListener<RangeUpgrade>(AddUpgrade);
        }

        public override void UnsubscribeToUpgrade()
        {
            EventManager.Instance.RemoveListener<RangeUpgrade>(AddUpgrade);
        }
    }
}