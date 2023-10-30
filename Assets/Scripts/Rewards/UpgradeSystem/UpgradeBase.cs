using UnityEngine;

namespace GamePlay.Rewards.Upgrades
{
    public abstract class UpgradeBase : Reward
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
        [field: SerializeField] public UpgradeTargetType TargetType { get; private set; }
        [field: SerializeField] public UpgradeAdditionType AdditionType { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
    }
}