using GamePlay.StatSystem;
using UnityEngine;

namespace GamePlay.Abilities.Attacks
{
    public abstract class AbilityBase : MonoBehaviour
    {
        [SerializeField] protected StatController _statController;

        protected ControllerBase _owner;
        protected float _timer;
        protected bool _isActive;

        public virtual void Init(ControllerBase owner)
        {
            _owner = owner;
        }

        public virtual void ActivateAbility()
        {
            _isActive = true;
            _timer = _statController.GetStat<CooldownStat>().CurrentValue;
        }

        public virtual void DeactivateAbility()
        {
            _isActive = false;
        }

        public virtual void UpdateAbility()
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