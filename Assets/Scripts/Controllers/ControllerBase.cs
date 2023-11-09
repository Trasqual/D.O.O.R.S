using UnityEngine;

namespace GamePlay.Entities.Controllers
{
    public class ControllerBase : Entity
    {
        [SerializeField] protected HealthManager _healthManager;
        [field: SerializeField] public Transform SpellSpawnRoot { get; private set; }

        public virtual void TakeDamage(float damage)
        {
            _healthManager.TakeDamage(damage);
        }
    }
}