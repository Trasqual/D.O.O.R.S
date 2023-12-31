using GamePlay.Abilities.Attacks;
using GamePlay.EventSystem;
using UnityEngine;

namespace GamePlay.Rewards.AbilityRewards
{
    [CreateAssetMenu(menuName = "Rewards/AbilityReward")]
    public class AbilityReward : Reward
    {
        [field: SerializeField] public AbilityData AbilityData { get; protected set; }

        public override void GiveReward()
        {
            EventManager.Instance.TriggerEvent<AbilityReward>(AbilityData);
        }
    }
}