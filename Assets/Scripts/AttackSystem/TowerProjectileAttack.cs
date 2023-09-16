//using Scripts.Controller;
//using UnityEngine;

//namespace Scripts.AttackSystem
//{
//    public class TowerProjectileAttack : AttackBase<EnemyController>
//    {
//        [SerializeField] private Transform _attackHead;
//        [SerializeField] private Transform _attackHeadVertical;
//        [SerializeField] private float _rotationSpeed = 15f;

//        protected override void Update()
//        {
//            base.Update();
//            if (_target == null) return;
//            var dir = new Vector3(_target.transform.position.x, _attackHead.position.y, _target.transform.position.z) - _attackHead.position;
//            _attackHead.rotation = Quaternion.Slerp(_attackHead.rotation, Quaternion.LookRotation(dir), _rotationSpeed * Time.deltaTime);

//            var angle = Vector3.Angle(Vector3.up, _target.transform.position - _attackHeadVertical.position) - 90f;
//            var vert = _attackHeadVertical.localEulerAngles;
//            vert.x = angle;
//            _attackHeadVertical.localEulerAngles = vert;
//        }
//    }
//}