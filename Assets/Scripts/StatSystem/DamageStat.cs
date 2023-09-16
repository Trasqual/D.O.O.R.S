using GamePlay.EventSystem;
using GamePlay.UpgradeSystem;

namespace GamePlay.StatSystem
{
    public class DamageStat : FloatStat
    {
        public DamageStat(string statName) : base(statName) { }

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