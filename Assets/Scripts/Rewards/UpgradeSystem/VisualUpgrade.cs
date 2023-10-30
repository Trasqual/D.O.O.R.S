using GamePlay.EventSystem;
using UnityEngine;

namespace GamePlay.Rewards.Upgrades
{
    [CreateAssetMenu(menuName = "Rewards/Upgrades/Projectile Upgrade")]
    public class VisualUpgrade : UpgradeBase
    {
        [field: SerializeField] public GameObject _visual { get; private set; }

        public override void GiveReward()
        {
            EventManager.Instance.TriggerEvent<VisualUpgrade>(this);
        }
    }
}