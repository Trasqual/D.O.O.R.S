using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        Random.InitState(_randomSeed);

        EventManager.Instance.AddListener<DoorSelectedEvent>(OnDoorSelected);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<DoorSelectedEvent>(OnDoorSelected);
    }

    private void OnDoorSelected(object data)
    {
        _selectedDoorData = (DoorData)data;
        _exitSide = _selectedDoorData.DoorSide;
    }

    [ContextMenu("CreateRoom")]
    public void CreateCreatureRoom()
    {
        CreateRoom(RoomType.Creature);
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
        };

        _lastRoom = spawnedRoom.GetComponent<Room>();
        _lastRoom.Init(roomWidth, roomLength, roomDoorCount, _exitSide, _prefabProvider);

        _generatedRooms.Add(_lastRoom);
    }
}
