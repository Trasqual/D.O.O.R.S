using GamePlay.Visuals;
using UnityEngine;

public class CircularAttack : MonoBehaviour
{
    [SerializeField] private PlayerDetector _detector;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _cooldown = 1f;
    [SerializeField] private int _count = 5;

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

        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            Perform();
            _timer = _cooldown;
        }
    }

    private void Perform()
    {
        for (int i = 0; i < _count; i++)
        {
            var projectile = Instantiate(_projectilePrefab);
            projectile.transform.position = transform.position + Vector3.up * 2f;
            ((DirectProjectile)projectile).Init(Quaternion.AngleAxis(360f / (_count) * i, Vector3.up) * Vector3.forward);
        }
    }
}
