using GamePlay.RoomSystem.Placeables.Doors;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Indication
{
    public class DoorIndicator : MonoBehaviour
    {
        [SerializeField] private Image _img;

        private Door _door;
        private Camera _cam;
        private bool _active = false;

        public bool IsInitialized => _door != null;
        public Vector3 DoorPosition => _door.transform.position;

        public void Initialize(Door door, Camera cam)
        {
            _door = door;
            _cam = cam;
        }

        public void Activate()
        {
            _img.enabled = true;
            _active = true;
        }

        public void Deactivate()
        {
            _img.enabled = false;
            _active = false;
        }

        public void FixUpdate()
        {
            if (_door == null) return;

            var doorPos = _cam.WorldToScreenPoint(_door.transform.position);

            if (!IsInScreen(doorPos) && !_active)
            {
                Activate();
            }
            else if (IsInScreen(doorPos) && _active)
            {
                Deactivate();
                return;
            }

            var doorPosOrigin = doorPos - new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            doorPosOrigin.Normalize();

            transform.localPosition = new Vector3(doorPosOrigin.x * Screen.width / 2f * 0.95f, doorPosOrigin.y * Screen.height / 2f * 0.95f, 0f);
        }

        private bool IsInScreen(Vector3 position)
        {
            return position.x > 0 && position.x < Screen.width && position.y > 0 && position.y < Screen.height;
        }

        public void Clear()
        {
            _door = null;
        }
    }
}