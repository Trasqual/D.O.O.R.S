using GamePlay.Projectiles;
using UnityEngine;

namespace GamePlay.Attacks
{
    public class CircularAttack : AbilityBase
    {
        [SerializeField] private int _count = 5;


        protected override void Perform()
        {
            for (int i = 0; i < _count; i++)
            {
                var projectile = Instantiate(_projectilePrefab);
                projectile.transform.position = transform.position + Vector3.up * 2f;
                ((DirectProjectile)projectile).Init(Quaternion.AngleAxis(360f / _count * i, Vector3.up) * Vector3.forward);
            }
        }
    }
}