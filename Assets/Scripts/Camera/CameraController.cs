using Cinemachine;
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
            EventManager.Instance.AddListener<RoomSpawnAnimationFinishedEvent>(OnRoomSpawnAnimationFinished);
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener<DoorSelectedEvent>(OnDoorSelected);
            EventManager.Instance.RemoveListener<RoomSpawnAnimationFinishedEvent>(OnRoomSpawnAnimationFinished);
        }

        private void OnDoorSelected(object data)
        {
            _playerCam.gameObject.SetActive(false);
            _roomCam.gameObject.SetActive(true);
        }

        private void OnRoomSpawnAnimationFinished(object data)
        {
            _roomCam.gameObject.SetActive(false);
            _playerCam.gameObject.SetActive(true);
        }
    }
}
