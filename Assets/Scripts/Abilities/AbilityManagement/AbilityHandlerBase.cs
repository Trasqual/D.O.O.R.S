using GamePlay.Abilities.Attacks;
using GamePlay.Entities.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GamePlay.Abilities.Management
{
    public class AbilityHandlerBase : MonoBehaviour
    {
        [SerializeField] private ControllerBase _owner;

        private List<AbilityBase> _abilities = new();

        public void GainAbility(object data)
        {
            var ability = ((AbilityData)data);

            if (!_abilities.Any((x) => x.GetType() == ability.Prefab.GetType()))
            {
                var abilityInstance = Instantiate(ability.Prefab, transform.position, transform.rotation, transform);
                _abilities.Add(abilityInstance);
                abilityInstance.Init(_owner);
            }
        }

        public void RemoveAbility(Type ability)
        {
            var existingAbility = _abilities.FirstOrDefault((x) => x.GetType() == ability);
            if (existingAbility != null)
            {
                _abilities.Remove(existingAbility);
                Destroy(existingAbility.gameObject);
            }
        }

        public void StopAbilities(object data)
        {
            for (int i = 0; i < _abilities.Count; i++)
            {
                _abilities[i].DeactivateAbility();
            }
        }

        public void StartAbilities(object data)
        {
            for (int i = 0; i < _abilities.Count; i++)
            {
                _abilities[i].ActivateAbility();
            }
        }

        public void UpdateAbilities()
        {
            if (_abilities.Count <= 0) return;

            for (int i = 0; i < _abilities.Count; i++)
            {
                _abilities[i].UpdateAbility();
            }
        }
    }
}