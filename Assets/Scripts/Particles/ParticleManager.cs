using UnityEngine;
using Utilities;

namespace GamePlay.Particles
{
    public class ParticleManager : Singleton<ParticleManager>
    {
        [SerializeField] private ParticleSystem _wallSpawnParticlePrefab;

        public ParticleSystem GetWallSpawnParticle()
        {
            return Instantiate(_wallSpawnParticlePrefab);
        }
    }
}