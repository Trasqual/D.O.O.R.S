using GamePlay.EventSystem;
using GamePlay.Rewards.Upgrades;

namespace GamePlay.StatSystem
{
    public class ArmorStat : FloatStat
    {
        public ArmorStat(string statName) : base(statName) { }

        public override void SubscribeToUpgrade()
        {
            EventManager.Instance.AddListener<ArmorUpgrade>(AddUpgrade);
        }

        public override void UnsubscribeToUpgrade()
        {
            EventManager.Instance.RemoveListener<ArmorUpgrade>(AddUpgrade);
        }
    }
}