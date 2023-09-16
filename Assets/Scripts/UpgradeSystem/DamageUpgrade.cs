using GamePlay.EventSystem;
using UnityEngine;

namespace GamePlay.UpgradeSystem
{
    [CreateAssetMenu(menuName = "Upgrades/Damage Upgrade")]
    public class DamageUpgrade : UpgradeBase
    {
        public override void BuyUpgrade()
        {
            base.BuyUpgrade();
            EventManager.Instance.TriggerEvent<DamageUpgrade>(this);
        }
    }
}