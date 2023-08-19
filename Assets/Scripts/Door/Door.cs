using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector2 _doorSide;
    private RoomType _roomType;

    public void Init(Vector2 doorSide, RoomType roomType)
    {
        _doorSide = doorSide;
        _roomType = roomType;
    }

    public void SelectDoor()
    {
        var doorData = new DoorData() { DoorSide = _doorSide, RoomType = _roomType };
        EventManager.Instance.TriggerEvent<DoorSelectedEvent>(new DoorSelectedEvent() { DoorData = doorData });
    }
}
