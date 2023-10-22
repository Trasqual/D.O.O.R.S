using GamePlay.Visuals;
using UnityEngine;

public class ZoneAttack : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _cooldown = 0.5f;

    private PlayerDetector _detector;
    private float _timer;
    private bool _isActive;
    private Projectile _visual;

    private void Start()
    {
        ActivateAttack();
    }

    public void ActivateAttack()
    {
        _isActive = true;
        _timer = _cooldown;
        _visual = Instantiate(_projectilePrefab, transform.position, transform.rotation, transform);
        _detector = _visual.GetComponentInChildren<PlayerDetector>();
    }

    private void Update()
    {
        if (!_isActive) return;

        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            Perform();
            _timer = _cooldown;
        }
    }

    private void Perform()
    {
        for (int i = 0; i < _detector.EnemyCount; i++)
        {
            if(_detector.Enemies[i].TryGetComponent(out HealthManager healthManager))
            {
                healthManager.TakeDamage(_visual.Damage);
            }
        }
    }
}
