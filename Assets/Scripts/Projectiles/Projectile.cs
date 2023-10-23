using UnityEngine;

namespace GamePlay.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [field: SerializeField] public float Damage { get; protected set; }
    }
}