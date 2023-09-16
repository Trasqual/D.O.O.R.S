using GamePlay.EventSystem;
using UnityEngine;

namespace GamePlay.UpgradeSystem
{
    [CreateAssetMenu(menuName = "Upgrades/Health Upgrade")]
    public class HealthUpgrade : UpgradeBase
    {
        public override void BuyUpgrade()
        {
            base.BuyUpgrade();
            EventManager.Instance.TriggerEvent<HealthUpgrade>(this);
        }
    }
}