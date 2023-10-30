using GamePlay.EventSystem;
using UnityEngine;

namespace GamePlay.Rewards.Upgrades
{
    [CreateAssetMenu(menuName = "Rewards/Upgrades/Armor Upgrade")]
    public class ArmorUpgrade : UpgradeBase
    {
        public override void GiveReward()
        {
            EventManager.Instance.TriggerEvent<ArmorUpgrade>(this);
        }
    }
}