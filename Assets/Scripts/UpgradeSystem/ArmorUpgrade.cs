using GamePlay.EventSystem;
using UnityEngine;

namespace GamePlay.UpgradeSystem
{
    [CreateAssetMenu(menuName = "Upgrades/Armor Upgrade")]
    public class ArmorUpgrade : UpgradeBase
    {
        public override void BuyUpgrade()
        {
            base.BuyUpgrade();
            EventManager.Instance.TriggerEvent<ArmorUpgrade>(this);
        }
    }
}