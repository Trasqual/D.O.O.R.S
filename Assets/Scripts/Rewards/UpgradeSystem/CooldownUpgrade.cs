using GamePlay.EventSystem;
using UnityEngine;

namespace GamePlay.Rewards.Upgrades
{
    [CreateAssetMenu(menuName = "Rewards/Upgrades/Cooldown Upgrade")]
    public class CooldownUpgrade : UpgradeBase
    {
        public override void GiveReward()
        {
            EventManager.Instance.TriggerEvent<CooldownUpgrade>(this);
        }
    }
}