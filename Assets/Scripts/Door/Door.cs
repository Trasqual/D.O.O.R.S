using MovementSystem.PlayerMovements;
using RoomSystem.Creation;
using RoomSystem.Rooms;
using UnityEngine;

namespace RoomSystem.Doors
{
    public class Door : MonoBehaviour
    {
        private Vector2 _doorSide;
        private RoomType _roomType;
        private Room _room;
        private bool _isActive;

        public void Init(Vector2 doorSide, RoomType roomType, Room room, bool isActive)
        {
            _doorSide = doorSide;
            _roomType = roomType;
            _room = room;
            _isActive = isActive;
        }

        private void SelectDoor()
        {
            var doorData = new DoorData() { DoorSide = _doorSide, RoomType = _roomType, Room = _room };
            _isActive = false;
            EventManager.Instance.TriggerEvent<DoorSelectedEvent>(new DoorSelectedEvent() { DoorData = doorData });
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isActive) return;
            if (other.TryGetComponent(out PlayerMovement player))
            {
                SelectDoor();
            }
        }
    }
}
