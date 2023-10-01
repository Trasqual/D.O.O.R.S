using GamePlay.RoomSystem.Placeables.Doors;
using GamePlay.RoomSystem.Rooms;
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
        public Room ActiveRoom;
    }

    public class RoomSpawnAnimationFinishedEvent
    {
        public Room CurrentRoom;
    }

    public class CameraIsInPositionForRoomCreationEvent
    {
    }
}
