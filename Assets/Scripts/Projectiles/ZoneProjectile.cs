using UnityEngine;

namespace GamePlay.Visuals.Projectiles
{
    public class ZoneProjectile : Visual
    {
        [SerializeField] private PlayerDetector _detector;
        [SerializeField] private ParticleSystem _visualParticle;

        public void UpdateSize(float range)
        {
            _detector.UpdateSize(range);
            _visualParticle.transform.localScale = Vector3.one * range;
        }

        public void Enable(bool isActive)
        {
            _detector.enabled = isActive;
            if (isActive)
            {
                _visualParticle.Play();
            }
            else
            {
                _visualParticle.Stop();
            }
        }
    }
}