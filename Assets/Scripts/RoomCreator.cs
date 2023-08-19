using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomCreator : MonoBehaviour
{
    [SerializeField] private int minRoomWidth;
    [SerializeField] private int maxRoomWidth;

    [SerializeField] private int minRoomLength;
    [SerializeField] private int maxRoomLength;

    [SerializeField] private int randomSeed;
    [SerializeField] private PrefabProvider _prefabProvider;

    private List<Room> _generatedRooms = new();

    private void Awake()
    {
        Random.InitState(randomSeed);
    }

    [ContextMenu("CreateRoom")]
    public void CreateCreatureRoom()
    {
        CreateRoom(RoomType.Creature);
    }

    public void CreateRoom(RoomType type)
    {
        var roomWidth = Random.Range(minRoomWidth, maxRoomWidth);
        var roomLength = Random.Range(minRoomLength, maxRoomLength);

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
        room.Init(roomWidth, roomLength, _prefabProvider);

        _generatedRooms.Add(room);
    }
}
