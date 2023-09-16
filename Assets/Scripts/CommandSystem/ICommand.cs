using System;

namespace GamePlay.CommandSystem
{
    public interface ICommand
    {
        public RoomCreationPriority Priority { get; set; }
        public void Execute(Action OnComplete);
    }
}