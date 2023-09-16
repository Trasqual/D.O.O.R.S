using GamePlay.EventSystem;
using GamePlay.UpgradeSystem;

namespace GamePlay.StatSystem
{
    public class FireRateStat : FloatStat
    {
        public FireRateStat(string statName) : base(statName) { }

        public override void SubscribeToUpgrade()
        {
            EventManager.Instance.AddListener<FireRateUpgrade>(AddUpgrade);
        }

        public override void UnsubscribeToUpgrade()
        {
            EventManager.Instance.RemoveListener<FireRateUpgrade>(AddUpgrade);
        }
    }
}