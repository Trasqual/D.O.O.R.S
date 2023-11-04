using GamePlay.EventSystem;
using GridSystem;
using GamePlay.MovementSystem.PlayerMovements;
using GamePlay.RoomSystem.Creation;
using GamePlay.RoomSystem.Rooms;
using System.Collections.Generic;
using UnityEngine;
using GamePlay.AnimationSystem;
using System;
using GamePlay.Particles;
using GamePlay.Rewards;
using TMPro;
using GamePlay.AnimationSystem.DoorAnimations;

namespace GamePlay.RoomSystem.Placeables.Doors
{
    public class Door : MonoBehaviour, IPlaceable, IAnimateable
    {
        [SerializeField] private AnimationBase _animation;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private DoorOpenCloseAnimations _doorAnims;


        private Vector2 _doorSide;
        private RoomType _roomType;
        private Room _room;
        public bool IsActive { get; private set; }
        public Reward Reward { get; private set; }

        public List<GridCell> GridCells { get; set; } = new();

        public void Initialize(Vector2 doorSide, RoomType roomType, Room room, bool isActive, Reward reward)
        {
            _doorSide = doorSide;
            _roomType = roomType;
            _room = room;
            IsActive = isActive;
            if (IsActive)
            {
                Reward = reward;
                _text.SetText(Reward.name);
            }
        }

        private void SelectDoor()
        {
            if (Reward != null) Reward.GiveReward();
            _doorAnims.Animate();
            var doorData = new DoorData() { DoorSide = _doorSide, RoomType = _roomType, Room = _room };
            IsActive = false;
            EventManager.Instance.TriggerEvent<DoorSelectedEvent>(new DoorSelectedEvent() { DoorData = doorData });
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsActive) return;
            if (other.TryGetComponent(out PlayerMovement player))
            {
                SelectDoor();
            }
        }

        public void Animate(Action OnStart = null, Action OnComplete = null)
        {
            _animation.Animate(null, () =>
            {
                OnComplete?.Invoke();
                PlaySpawnParticles();
                _text.enabled = true;
                if (!IsActive)
                    _doorAnims.Animate();
            });
        }

        public void PrepareForAnimation()
        {
            _animation.PrepareForAnimation();
            _text.enabled = false;
            if (!IsActive)
            {
                _doorAnims.PrepareForAnimation();
            }
        }

        private void PlaySpawnParticles()
        {
            var particle = ParticleManager.Instance.GetWallSpawnParticle();
            particle.transform.SetParent(transform);
            particle.transform.localPosition = Vector3.zero;
            particle.Play();
        }
    }
}
