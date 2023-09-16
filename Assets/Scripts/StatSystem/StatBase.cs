using GamePlay.UpgradeSystem;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.StatSystem
{
    [System.Serializable]
    public abstract class StatBase
    {
        [SerializeField] private string StatName;
        [SerializeField] private UpgradeTargetType Type;
        protected List<UpgradeBase> _upgrades;

        public StatBase(string statName)
        {
            StatName = statName;
            _upgrades = new();
        }

        public virtual void Init()
        {
            _upgrades = new();
        }

        public abstract void AddUpgrade(object upgrade);
        public abstract void RemoveUpgrade(object upgrade);

        public abstract void SubscribeToUpgrade();
        public abstract void UnsubscribeToUpgrade();
    }
}