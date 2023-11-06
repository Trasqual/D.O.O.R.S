using UnityEngine;

public class ControllerBase : MonoBehaviour
{
    [SerializeField] protected HealthManager _healthManager;

    public virtual void TakeDamage(float damage)
    {
        _healthManager.TakeDamage(damage);
    }
}
