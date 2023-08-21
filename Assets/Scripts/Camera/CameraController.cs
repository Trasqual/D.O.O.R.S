using Cinemachine;
using UnityEngine;

namespace CameraSystem
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _playerCam;
        [SerializeField] private CinemachineVirtualCamera _roomCam;

        private void Start()
        {
            EventManager.Instance.AddListener<RoomsAreSlidingEvent>(OnRoomsAreSliding);
            EventManager.Instance.AddListener<RoomSlidingEndedEvent>(OnRoomSlidingEnded);
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener<RoomsAreSlidingEvent>(OnRoomsAreSliding);
            EventManager.Instance.RemoveListener<RoomSlidingEndedEvent>(OnRoomSlidingEnded);
        }

        private void OnRoomsAreSliding(object data)
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
