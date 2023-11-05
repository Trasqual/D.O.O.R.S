using GamePlay.EnemySystem;

namespace GamePlay.DetectionSystem
{
    public class PlayerDetector : DetectorBase<Enemy>
    {
        protected override void OnDetected(Enemy detected)
        {
            base.OnDetected(detected);
            detected.OnDeath += RemoveDetected;
        }

        protected override void OnDetectedExit(Enemy detected)
        {
            base.OnDetectedExit(detected);
            detected.OnDeath -= RemoveDetected;
        }
    }
}