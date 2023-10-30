using GamePlay.EventSystem;
using UnityEngine;

namespace GamePlay.Rewards.Upgrades
{
    [CreateAssetMenu(menuName = "Rewards/Upgrades/Range Upgrade")]
    public class RangeUpgrade : UpgradeBase
    {
        public override void GiveReward()
        {
            EventManager.Instance.TriggerEvent<RangeUpgrade>(this);
        }
    }
}