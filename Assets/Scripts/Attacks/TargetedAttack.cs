using GamePlay.Visuals;
using System.Threading.Tasks;
using UnityEngine;

public class TargetedAttack : MonoBehaviour
{
    [SerializeField] private PlayerDetector _detector;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _cooldown = 1f;
    [SerializeField] private int _count = 1;
    [SerializeField] private int _delayBetweenProjectilesInMiliseconds = 100;

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
            Perform();
            _timer = _cooldown;
        }
    }

    private async void Perform()
    {
        for (int i = 0; i < _count; i++)
        {
            var projectile = Instantiate(_projectilePrefab);
            projectile.transform.position = transform.position + Vector3.up * 2f;
            ((TargetedProjectile)projectile).Init(_detector.GetClosestEnemy().transform);
            await Task.Delay(_delayBetweenProjectilesInMiliseconds);
        }
    }
}
