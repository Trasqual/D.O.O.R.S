using GamePlay.EventSystem;
using GridSystem;
using GamePlay.MovementSystem.PlayerMovements;
using GamePlay.RoomSystem.Creation;
using GamePlay.RoomSystem.Rooms;
using System.Collections.Generic;
using UnityEngine;
using GamePlay.AnimationSystem;
using System;

namespace GamePlay.RoomSystem.Placeables.Doors
{
    public class Door : MonoBehaviour, IPlaceable, IAnimateable
    {
        [SerializeField] private AnimationBase _animation;

        private Vector2 _doorSide;
        private RoomType _roomType;
        private Room _room;
        private bool _isActive;

        public List<GridCell> GridCells { get; set; } = new();

        public void Initialize(Vector2 doorSide, RoomType roomType, Room room, bool isActive)
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

        public void Animate(Action OnComplete)
        {
            _animation.Animate(() =>
            {
                OnComplete?.Invoke();
                //PlayParticles
            });
        }

        public void PrepareForAnimation()
        {
            _animation.PrepareForAnimation();
        }
    }
}
