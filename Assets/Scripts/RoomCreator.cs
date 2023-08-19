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
    [SerializeField] private PrefabProvider _prefabProvider;

    [SerializeField] private Vector2 _exitSide;

    private List<Room> _generatedRooms = new();

    private void Awake()
    {
        Random.InitState(_randomSeed);
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

        var room = spawnedRoom.GetComponent<Room>();
        room.Init(roomWidth, roomLength, roomDoorCount, _exitSide, _prefabProvider);

        _generatedRooms.Add(room);
    }
}
