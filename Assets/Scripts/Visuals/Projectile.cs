using UnityEngine;

namespace GamePlay.Visuals
{
    public class Projectile : Visual
    {
        [field: SerializeField] public float Damage { get; protected set; }
    }
}