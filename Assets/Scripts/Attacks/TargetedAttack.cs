using GamePlay.Visuals;
using System.Collections;
using UnityEngine;

public class TargetedAttack : MonoBehaviour
{
    [SerializeField] private PlayerDetector _detector;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _cooldown = 1f;
    [SerializeField] private int _count = 1;
    [SerializeField] private float _delayBetweenProjectiles = 0.1f;

    private float _timer;
    private bool _isActive;

    private void Start()
    {
        ActivateAttack();
    }

    public void ActivateAttack()
    {
        _isActive = true;
        _timer = _cooldown;
    }

    private void Update()
    {
        if (!_isActive) return;

        if (_detector.EnemyCount <= 0)
        {
            _timer = _cooldown;
            return;
        }
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            StartCoroutine(Perform());
            _timer = _cooldown;
        }
    }

    private IEnumerator Perform()
    {
        for (int i = 0; i < _count; i++)
        {
            var projectile = Instantiate(_projectilePrefab);
            projectile.transform.position = transform.position + Vector3.up * 2f;
            ((TargetedProjectile)projectile).Init(_detector.GetClosestEnemy().transform);
            yield return new WaitForSeconds(_delayBetweenProjectiles);
        }
    }
}
