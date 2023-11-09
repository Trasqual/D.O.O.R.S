
using GamePlay.EventSystem;

namespace GamePlay.RoomSystem.Rooms
{
    public class InitialRoom : Room
    {
        protected override void FillRoom()
        {
            base.FillRoom();
            EventManager.Instance.TriggerEvent<RoomSpawnAnimationFinishedEvent>();
        }
    }
}