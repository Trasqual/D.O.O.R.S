using GamePlay.EnemySystem;
using GamePlay.Visuals;
using UnityEngine;

public class DirectProjectile : Projectile
{
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _maxDistance = 50f;

    private Vector3 _startPos;
    private Vector3 _direction;

    public void Init(Vector3 direction)
    {
        _direction = direction;
        _startPos = transform.position;
    }

    private void Update()
    {
        transform.position += _speed * Time.deltaTime * _direction;
        transform.rotation = Quaternion.LookRotation(_direction);

        if (Vector3.Distance(transform.position, _startPos) >= _maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (enemy.TryGetComponent(out HealthManager healthManager))
            {
                healthManager.TakeDamage(Damage);
                Destroy(gameObject);
            }
        }
    }
}
