using GamePlay.EventSystem;
using UnityEngine;

namespace GamePlay.Rewards.Upgrades
{
    [CreateAssetMenu(menuName = "Rewards/Upgrades/Damage Upgrade")]
    public class DamageUpgrade : UpgradeBase
    {
        public override void GiveReward()
        {
            EventManager.Instance.TriggerEvent<DamageUpgrade>(this);
        }
    }
}