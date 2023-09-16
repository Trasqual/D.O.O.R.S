using GamePlay.EventSystem;
using UnityEngine;

namespace GamePlay.UpgradeSystem
{
    [CreateAssetMenu(menuName = "Upgrades/Projectile Upgrade")]
    public class ProjectileUpgrade : UpgradeBase
    {
        public override void BuyUpgrade()
        {
            base.BuyUpgrade();
            EventManager.Instance.TriggerEvent<ProjectileUpgrade>(this);
        }
    }
}