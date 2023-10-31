using GamePlay.EventSystem;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Indication
{
    public class DoorIndicationManager : MonoBehaviour
    {
        [SerializeField] private DoorIndicator _doorIndicatorPrefab;
        private List<DoorIndicator> _doorIndicators = new();
        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
            for (int i = 0; i < 4; i++)
            {
                _doorIndicators.Add(Instantiate(_doorIndicatorPrefab, transform));
            }
            EventManager.Instance.AddListener<RoomCreatedEvent>(OnRoomSpawned);
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener<RoomCreatedEvent>(OnRoomSpawned);
        }

        private void OnRoomSpawned(object data)
        {
            var doors = ((RoomCreatedEvent)data).CurrentRoom.Doors;

            for (int i = 0; i < _doorIndicators.Count; i++)
            {
                _doorIndicators[i].Clear();
            }

            for (int i = 0; i < doors.Count; i++)
            {
                _doorIndicators[i].Initialize(doors[i], _cam);
            }
        }

        private void FixedUpdate()
        {
            if (_doorIndicators.Count <= 0) return;

            for (int i = 0; i < _doorIndicators.Count; i++)
            {
                _doorIndicators[i].FixUpdate();
            }
        }

    }
}