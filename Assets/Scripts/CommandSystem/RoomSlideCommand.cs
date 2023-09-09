using DG.Tweening;
using GamePlay.EventSystem;
using GamePlay.RoomSystem.Rooms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RoomSlideCommand : ICommand
{
    public List<Room> _rooms = new();
    public Room _currentRoom;

    public RoomSlideCommand(List<Room> rooms, Room currentRoom)
    {
        _rooms = rooms;
        _currentRoom = currentRoom;
    }

    public void Execute(Action OnComplete)
    {
        StartRoomSlide(OnComplete);
    }

    private async void StartRoomSlide(Action OnComplete)
    {
        await Task.Delay(1000);
        EventManager.Instance.TriggerEvent<RoomsAreSlidingEvent>(new RoomsAreSlidingEvent() { SlideAmount = -_currentRoom.transform.position });
        foreach (var room in _rooms)
        {
            room.transform.DOMove(-_currentRoom.transform.position, 3f).SetRelative();
        }
        await Task.Delay(3000);
        EventManager.Instance.TriggerEvent<RoomSlidingEndedEvent>(new RoomsAreSlidingEvent() { SlideAmount = -_currentRoom.transform.position });
        OnComplete?.Invoke();
    }
}
