using GamePlay.RoomSystem.Placeables.Doors;
using UnityEngine;

namespace GamePlay.EventSystem
{
    public class DoorSelectedEvent
    {
        public DoorData DoorData;
    }

    public class RoomsAreSlidingEvent
    {
        public Vector3 SlideAmount;
    }

    public class RoomSlidingEndedEvent
    {
    }
}
