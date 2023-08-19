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

    private List<Room> _generatedRooms = new();

    private void Awake()
    {
        Random.InitState(randomSeed);
    }

    public void CreateRoom(RoomType type)
    {
        var roomWidth = Random.Range(minRoomWidth, maxRoomWidth);
        var roomLength = Random.Range(minRoomLength, maxRoomLength);

        var spawnedRoom = PrefabProvider.Instance.GetRoom(transform);

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
        room.Init(roomWidth, roomLength);

        _generatedRooms.Add(room);
    }
}
