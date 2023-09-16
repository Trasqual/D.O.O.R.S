using GamePlay.UpgradeSystem;
using System;
using UnityEngine;

namespace GamePlay.StatSystem
{
    public abstract class FloatStat : StatBase
    {
        public FloatStat(string statName) : base(statName) { }

        public Action<float> OnStatChanged;

        [SerializeField] protected float _baseValue;

        public virtual float CurrentValue { get; protected set; }

        public override void Init()
        {
            base.Init();
            CalculateValue();
        }

        public override void AddUpgrade(object upgrade)
        {
            UpgradeBase upgradeBase = upgrade as UpgradeBase;
            _upgrades.Add(upgradeBase);
            CalculateValue();
        }

        public override void RemoveUpgrade(object upgrade)
        {
            UpgradeBase upgradeBase = upgrade as UpgradeBase;
            if (_upgrades.Contains(upgradeBase))
            {
                _upgrades.Remove(upgradeBase);
                CalculateValue();
            }
        }

        public virtual void CalculateValue()
        {
            CurrentValue = _baseValue;

            var totalPercentage = 1f;
            var totalCumulative = 0f;
            for (int i = 0; i < _upgrades.Count; i++)
            {
                if (_upgrades[i].AdditionType == UpgradeAdditionType.Percentage)
                {
                    totalPercentage += _upgrades[i].Value;
                }
                else if (_upgrades[i].AdditionType == UpgradeAdditionType.Cumulative)
                {
                    totalCumulative += _upgrades[i].Value;
                }
            }
            CurrentValue *= totalPercentage;
            CurrentValue += totalCumulative;
            OnStatChanged?.Invoke(CurrentValue);
        }
    }
}