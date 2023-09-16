using GamePlay.StatSystem;
using UnityEngine;

namespace GamePlay.AttackSystem
{
    public abstract class AttackBase<T> : MonoBehaviour where T : Component
    {
        [SerializeField] protected AttackData _data;
        //[SerializeField] protected Detector<T> _detector;
        [SerializeField] protected Transform _spawnPoint;

        //protected TowerSpell _projectilePrefab;
        protected float _fireRate;
        protected float _fireTimer;
        protected T _target;

        protected virtual void Awake()
        {
            //_detector.Targets.ItemAdded += (ObservableList<T> sender, ListChangedEventArgs<T> e) => OnTargetAdded(e.item);
            //_detector.Targets.ItemRemoved += (ObservableList<T> sender, ListChangedEventArgs<T> e) => OnTargetRemoved(e.item);
            _data.OnInitialized += InitializeStats;
        }

        private void OnDisable()
        {
            _data.OnInitialized -= InitializeStats;
        }

        protected virtual void InitializeStats()
        {
            _fireRate = _data.GetStat<FireRateStat>().CurrentValue;
            //_projectilePrefab = _data.GetStat<TowerSpellStat>().Prefab;
        }

        // TODO: Make a tower manager to update all towers from
        protected virtual void Update()
        {
            if (_target == null)
            {
                _fireTimer = 0f;
                return;
            }

            _fireTimer += Time.deltaTime;
            if (_fireTimer >= _fireRate)
            {
                Attack();
                _fireTimer = 0f;
            }
        }

        protected virtual void Attack()
        {
            //var spell = Instantiate(_projectilePrefab, _spawnPoint.position, _spawnPoint.rotation);
            //spell.Init(_detector, _data);
        }

        protected virtual void OnTargetAdded(T target)
        {
            if (_target == null)
                _target = target;
        }

        protected virtual void OnTargetRemoved(T target)
        {
            //if (_detector.Targets.Count <= 0)
            //{
            //    _target = null;
            //}

            //if (target == _target)
            //{
            //    _target = _detector.Targets[0];
            //}
        }
    }
}