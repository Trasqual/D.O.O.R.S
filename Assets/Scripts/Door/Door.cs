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

    private void SelectDoor()
    {
        var doorData = new DoorData() { DoorSide = _doorSide, RoomType = _roomType };
        GetComponent<Collider>().enabled = false;
        EventManager.Instance.TriggerEvent<DoorSelectedEvent>(new DoorSelectedEvent() { DoorData = doorData });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            SelectDoor();
        }
    }
}
