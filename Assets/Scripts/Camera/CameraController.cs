using Cinemachine;
using DG.Tweening;
using GamePlay.CommandSystem;
using GamePlay.EventSystem;
using UnityEngine;

namespace GamePlay.CameraSystem
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _playerCam;
        [SerializeField] private CinemachineVirtualCamera _roomCam;

        private void Start()
        {
            EventManager.Instance.AddListener<DoorSelectedEvent>(OnDoorSelected);
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener<DoorSelectedEvent>(OnDoorSelected);
        }

        private void OnDoorSelected(object data)
        {
            var cameraToRoomCreationCommand = new ChangeCameraCommand(_playerCam.gameObject, _roomCam.gameObject, () =>
            {
                DOVirtual.DelayedCall(1.5f, () =>
                {
                    EventManager.Instance.TriggerEvent<CameraIsInPositionForRoomCreationEvent>();
                });
            }, 0);
            CommandManager.Instance.AddCommand(cameraToRoomCreationCommand);


            var cameraToPlayerCommand = new ChangeCameraCommand(_roomCam.gameObject, _playerCam.gameObject, null, 0);
            CommandManager.Instance.AddCommand(cameraToPlayerCommand);
        }
    }
}
