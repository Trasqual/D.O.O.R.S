using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

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

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        CreateCreatureRoom();
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<DoorSelectedEvent>(OnDoorSelected);
    }

    private void OnDoorSelected(object data)
    {
        _selectedDoorData = ((DoorSelectedEvent)data).DoorData;
        _exitSide = _selectedDoorData.DoorSide;
        CreateRoom(_selectedDoorData.RoomType);
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

        var curRoom = spawnedRoom.GetComponent<Room>();
        curRoom.Init(roomWidth, roomLength, roomDoorCount, _exitSide, _prefabProvider);

        if (_lastRoom != null)
        {
            curRoom.transform.position = _lastRoom.transform.position +
                new Vector3(_selectedDoorData.DoorSide.x * _lastRoom.Size.x / 2f,
                0f,
                _selectedDoorData.DoorSide.y * _lastRoom.Size.y / 2f) + new Vector3(_selectedDoorData.DoorSide.x * curRoom.Size.x / 2f, 0f, _selectedDoorData.DoorSide.y * curRoom.Size.y / 2f);
        }

        _lastRoom = curRoom;

        _generatedRooms.Add(_lastRoom);

        StartCoroutine(SlideRooms());
    }

    private IEnumerator SlideRooms()
    {
        EventManager.Instance.TriggerEvent<RoomsAreSlidingEvent>(new RoomsAreSlidingEvent() { SlideAmount = -_lastRoom.transform.position });
        foreach (var room in _generatedRooms)
        {
            room.transform.DOMove(-_lastRoom.transform.position, 3f).SetRelative();
        }
        yield return new WaitForSeconds(3f);
        EventManager.Instance.TriggerEvent<RoomSlidingEndedEvent>(new RoomsAreSlidingEvent() { SlideAmount = -_lastRoom.transform.position });
    }
}
