using System;
using UnityEngine;

namespace GamePlay.CommandSystem
{
    public class ChangeCameraCommand : ICommand
    {
        public RoomCreationPriority Priority { get; set; }

        private readonly GameObject _camera1;
        private readonly GameObject _camera2;
        private readonly Action _onComplete;

        public ChangeCameraCommand(GameObject camera1, GameObject camera2, Action onComplete, RoomCreationPriority priority)
        {
            _camera1 = camera1;
            _camera2 = camera2;
            _onComplete = onComplete;
            Priority = priority;
        }

        public void Execute(Action OnComplete)
        {
            if (OnComplete != null && _onComplete != null)
                OnComplete += _onComplete;

            _camera1?.SetActive(false);
            _camera2?.SetActive(true);

            OnComplete?.Invoke();
        }
    }
}