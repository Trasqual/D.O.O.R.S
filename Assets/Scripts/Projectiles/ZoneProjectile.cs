using UnityEngine;

namespace GamePlay.Visuals.Projectiles
{
    public class ZoneProjectile : Visual
    {
        [SerializeField] private PlayerDetector _detector;
        [SerializeField] private Transform _visualParticle;

        public void UpdateSize(float range)
        {
            _detector.UpdateSize(range);
            _visualParticle.localScale = Vector3.one * range;
        }
    }
}