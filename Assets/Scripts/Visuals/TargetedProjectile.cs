using GamePlay.Visuals;
using UnityEngine;

public class TargetedProjectile : Projectile
{
    [SerializeField] private float _damage = 5;
    [SerializeField] private float _speed = 20f;

    private Transform _target;
    private Vector3 _targetsLastKnownPosition;

    public void Init(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
            transform.LookAt(_target.position);
            _targetsLastKnownPosition = _target.position;
        }
        else if (_targetsLastKnownPosition != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetsLastKnownPosition, _speed * Time.deltaTime);
            transform.LookAt(_targetsLastKnownPosition);

            if (Vector3.Distance(transform.position, _targetsLastKnownPosition) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthManager healthManager))
        {
            healthManager.TakeDamage(_damage);
        }
    }
}
