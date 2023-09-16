using UnityEngine;

namespace GamePlay.UpgradeSystem
{
    public abstract class UpgradeBase : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
        [field: SerializeField] public UpgradeTargetType TargetType { get; private set; }
        [field: SerializeField] public UpgradeAdditionType AdditionType { get; private set; }
        [field: SerializeField] public float Value { get; private set; }

        public virtual void BuyUpgrade()
        {
            //drop coins from coin manager
        }
    }
}