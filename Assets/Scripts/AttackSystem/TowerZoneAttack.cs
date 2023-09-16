//using Scripts.Controller;
//using Scripts.Projectiles;
//using Scripts.StatSystem;

//namespace Scripts.AttackSystem
//{
//    public class TowerZoneAttack : AttackBase<EnemyController>
//    {
//        private ZoneSpell _zoneSpell;

//        protected override void InitializeStats()
//        {
//            base.InitializeStats();
//            _zoneSpell = Instantiate(_data.GetStat<TowerSpellStat>().Prefab, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint) as ZoneSpell;
//            _zoneSpell.Init(_detector, _data);
//        }

//        protected override void Attack()
//        {
//            _zoneSpell.Fire();
//        }
//    }
//}