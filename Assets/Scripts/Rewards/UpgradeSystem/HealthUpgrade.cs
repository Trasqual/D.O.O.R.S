using GamePlay.EventSystem;
using UnityEngine;

namespace GamePlay.Rewards.Upgrades
{
    [CreateAssetMenu(menuName = "Rewards/Upgrades/Health Upgrade")]
    public class HealthUpgrade : UpgradeBase
    {
        public override void GiveReward()
        {
            EventManager.Instance.TriggerEvent<HealthUpgrade>(this);
        }
    }
}