using GamePlay.Projectiles;
using UnityEngine;

namespace GamePlay.Attacks
{
    public abstract class AbilityBase : MonoBehaviour
    {
        [SerializeField] protected Projectile _projectilePrefab;
        [SerializeField] protected float _cooldown = 0.5f;

        protected float _timer;
        protected bool _isActive;

        protected void Start()
        {
            ActivateAttack();
        }

        protected virtual void ActivateAttack()
        {
            _isActive = true;
            _timer = _cooldown;
        }

        protected virtual void Update()
        {
            if (!_isActive) return;

            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                Perform();
                _timer = _cooldown;
            }
        }

        protected abstract void Perform();
    }
}