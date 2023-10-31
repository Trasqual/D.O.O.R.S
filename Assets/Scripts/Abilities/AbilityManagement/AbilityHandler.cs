using GamePlay.Abilities.Attacks;
using GamePlay.EventSystem;
using GamePlay.Rewards.AbilityRewards;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GamePlay.Abilities.Management
{
    public class AbilityHandler : MonoBehaviour
    {
        private List<AbilityBase> _abilities = new();

        private void Awake()
        {
            EventManager.Instance.AddListener<AbilityReward>(GainAbility);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<AbilityReward>(GainAbility);
        }

        private void GainAbility(object data)
        {
            var ability = ((AbilityReward)data);

            if (!_abilities.Any((x) => x.GetType() == ability.AbilityPrefab.GetType()))
            {
                var abilityInstance = Instantiate(ability.AbilityPrefab, transform.position, transform.rotation, transform);
                _abilities.Add(abilityInstance);
            }
        }

        private void RemoveAbility(Type ability)
        {
            var existingAbility = _abilities.FirstOrDefault((x) => x.GetType() == ability);
            if (existingAbility != null)
            {
                _abilities.Remove(existingAbility);
                Destroy(existingAbility.gameObject);
            }
        }
    }
}