//using Scripts.EventSystem;
//using Scripts.UpgradeSystem;
//using UnityEngine;

//namespace Scripts.StatSystem
//{
//    public class TowerSpellStat : StatBase
//    {
//        public TowerSpellStat(string statName) : base(statName) { }

//        [field: SerializeField] public TowerSpell Prefab { get; private set; }

//        public override void AddUpgrade(object upgrade)
//        {
//            UpgradeBase upgradeBase = upgrade as UpgradeBase;
//            //change projectile
//        }

//        public override void RemoveUpgrade(object upgrade)
//        {
//            UpgradeBase upgradeBase = upgrade as UpgradeBase;
//            //change projectile with default or previous projectile
//        }

//        public override void SubscribeToUpgrade()
//        {
//            EventManager.Instance.AddListener<ProjectileUpgrade>(AddUpgrade);
//        }

//        public override void UnsubscribeToUpgrade()
//        {
//            EventManager.Instance.RemoveListener<ProjectileUpgrade>(AddUpgrade);
//        }
//    }
//}