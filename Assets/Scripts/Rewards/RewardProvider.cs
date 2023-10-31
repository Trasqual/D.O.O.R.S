using GamePlay.Rewards.AbilityRewards;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace GamePlay.Rewards.Management
{
    public class RewardProvider : Singleton<RewardProvider>
    {
        [SerializeField] private List<Reward> _rewards = new();

        private List<Reward> _rewardsProvided = new();

        public Reward GetRandomAbilityReward()
        {
            var abilityRewards = _rewards.FindAll((x) => x.GetType() == typeof(AbilityReward));

            var selected = abilityRewards[Random.Range(0, abilityRewards.Count)];
            //if (_rewardsProvided.Contains(selected))
            //{
            //    return GetRandomAbilityReward();
            //}

            _rewardsProvided.Add(selected);
            return selected;
        }
    }
}