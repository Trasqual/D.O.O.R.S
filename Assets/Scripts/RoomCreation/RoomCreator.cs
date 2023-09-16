using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using GamePlay.RoomSystem.Rooms;
using GamePlay.RoomSystem.Placeables.Doors;
using GamePlay.RoomSystem.Placeables;
using GamePlay.EventSystem;

namespace GamePlay.RoomSystem.Creation
{
    public class RoomCreator : MonoBehaviour
    {
        [SerializeField] private int _minRoomWidth;
        [SerializeField] private int _maxRoomWidth;

        [SerializeField] private int _minRoomLength;
        [SerializeField] private int _maxRoomLength;

        [SerializeField] private int _minRoomDoorCount;
        [SerializeField] private int _maxRoomDoorCount;

        [SerializeField] private int _randomSeed;
        [SerializeField] private PropFactory _prefabProvider;

        [SerializeField] private Vector2 _exitSide;

        private List<Room> _generatedRooms = new();

        private DoorData _selectedDoorData;
        private Room _lastRoom;
        private Room _currentRoom;

        private void Awake()
        {
            //Random.InitState(_randomSeed);
            EventManager.Instance.AddListener<DoorSelectedEvent>(OnDoorSelected);
            EventManager.Instance.AddListener<CameraIsInPositionForRoomCreationEvent>(OnCameraInPositionForCreation);
        }

        private void Start()
        {
            CreateInitialRoom();
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<DoorSelectedEvent>(OnDoorSelected);
            EventManager.Instance.RemoveListener<CameraIsInPositionForRoomCreationEvent>(OnCameraInPositionForCreation);
        }

        private void OnDoorSelected(object data)
        {
            _selectedDoorData = ((DoorSelectedEvent)data).DoorData;
            _exitSide = _selectedDoorData.DoorSide;
            _lastRoom = _selectedDoorData.Room;
            CreateRoom(_selectedDoorData.RoomType);
            _currentRoom.PrepareForAnimation();
        }

        private void OnCameraInPositionForCreation(object data)
        {
            var roomSlideCommand = new RoomSlideCommand(_generatedRooms, _currentRoom);
            CommandManager.Instance.AddCommand(roomSlideCommand);
            CommandManager.Instance.StartExecution();
        }

        public void CreateInitialRoom()
        {
            CreateRoom(RoomType.Initial);
            _currentRoom.GenerateNavMesh();
        }

        public void CreateRoom(RoomType type)
        {
            var roomWidth = Random.Range(_minRoomWidth, _maxRoomWidth);
            var roomLength = Random.Range(_minRoomLength, _maxRoomLength);
            var roomDoorCount = Random.Range(_minRoomDoorCount, _maxRoomDoorCount + 1);

            var spawnedRoom = _prefabProvider.GetRoom(transform);

            switch (type)
            {
                case RoomType.Creature:
                    spawnedRoom.AddComponent<CreatureRoom>();
                    break;
                case RoomType.Boss:
                    spawnedRoom.AddComponent<BossRoom>();
                    break;
                case RoomType.Treasure:
                    spawnedRoom.AddComponent<TreasureRoom>();
                    break;
                case RoomType.Initial:
                    spawnedRoom.AddComponent<InitialRoom>();
                    break;
            };

            _currentRoom = spawnedRoom.GetComponent<Room>();
            _currentRoom.Init(roomWidth, roomLength, roomDoorCount, _exitSide, _prefabProvider);

            if (_lastRoom != null)
            {
                _currentRoom.transform.position = _lastRoom.transform.position +
                    new Vector3(_selectedDoorData.DoorSide.x * _lastRoom.Size.x / 2f,
                    0f,
                    _selectedDoorData.DoorSide.y * _lastRoom.Size.y / 2f) + new Vector3(_selectedDoorData.DoorSide.x * _currentRoom.Size.x / 2f, 0f, _selectedDoorData.DoorSide.y * _currentRoom.Size.y / 2f);
            }

            _generatedRooms.Add(_currentRoom);
        }
    }
}