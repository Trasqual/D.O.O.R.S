using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public Action<float> OnDamageTaken;
    public Action OnDeath;

    [field: SerializeField] public float MaxHealth { get; private set; }

    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        OnDamageTaken?.Invoke(damage);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDeath?.Invoke();
        _currentHealth = MaxHealth;
    }
}
