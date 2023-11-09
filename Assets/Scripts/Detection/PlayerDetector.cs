using GamePlay.Entities.Controllers;

namespace GamePlay.DetectionSystem
{
    public class PlayerDetector : DetectorBase<EnemyController>
    {
        protected override void OnDetected(EnemyController detected)
        {
            base.OnDetected(detected);
            detected.OnDeath += RemoveDetected;
        }

        protected override void OnDetectedExit(EnemyController detected)
        {
            base.OnDetectedExit(detected);
            detected.OnDeath -= RemoveDetected;
        }
    }
}