using GamePlay.EventSystem;
using GamePlay.UpgradeSystem;

namespace GamePlay.StatSystem
{
    public class ProjectileCountStat : FloatStat
    {
        public ProjectileCountStat(string statName) : base(statName) { }

        public override void SubscribeToUpgrade()
        {
            EventManager.Instance.AddListener<ProjectileCountUpgrade>(AddUpgrade);
        }

        public override void UnsubscribeToUpgrade()
        {
            EventManager.Instance.RemoveListener<ProjectileCountUpgrade>(AddUpgrade);
        }
    }
}