using GamePlay.EventSystem;
using UnityEngine;

namespace GamePlay.UpgradeSystem
{
    [CreateAssetMenu(menuName = "Upgrades/Area Upgrade")]
    public class AreaUpgrade : UpgradeBase
    {
        public override void BuyUpgrade()
        {
            base.BuyUpgrade();
            EventManager.Instance.TriggerEvent<AreaUpgrade>(this);
        }
    }
}