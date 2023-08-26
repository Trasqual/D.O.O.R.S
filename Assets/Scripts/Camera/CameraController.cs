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
            EventManager.Instance.AddListener<RoomSlidingEndedEvent>(OnRoomSlidingEnded);
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener<DoorSelectedEvent>(OnDoorSelected);
            EventManager.Instance.RemoveListener<RoomSlidingEndedEvent>(OnRoomSlidingEnded);
        }

        private void OnDoorSelected(object data)
        {
            _playerCam.gameObject.SetActive(false);
            _roomCam.gameObject.SetActive(true);
        }

        private void OnRoomSlidingEnded(object data)
        {
            _roomCam.gameObject.SetActive(false);
            _playerCam.gameObject.SetActive(true);
        }
    }
}
