using GamePlay.StatSystem;
using UnityEngine;

namespace GamePlay.Attacks
{
    public abstract class AbilityBase : MonoBehaviour
    {
        [SerializeField] protected StatController _statController;

        protected float _timer;
        protected bool _isActive;

        protected void Start()
        {
            ActivateAttack();
        }

        protected virtual void ActivateAttack()
        {
            _isActive = true;
            _timer = _statController.GetStat<CooldownStat>().CurrentValue;
        }

        protected virtual void Update()
        {
            if (!_isActive) return;

            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                Perform();
                _timer = _statController.GetStat<CooldownStat>().CurrentValue;
            }
        }

        protected abstract void Perform();
    }
}