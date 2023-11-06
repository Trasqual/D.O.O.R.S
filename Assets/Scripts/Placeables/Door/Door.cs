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
using DG.Tweening;
using GamePlay.Rewards.AbilityRewards;

namespace GamePlay.RoomSystem.Placeables.Doors
{
    public class Door : MonoBehaviour, IPlaceable, IAnimateable
    {
        [SerializeField] private AnimationBase _animation;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private DoorOpenCloseAnimations _doorAnim;


        private Vector2 _doorSide;
        private RoomType _roomType;
        private Room _room;
        public bool IsUsable { get; private set; }
        public bool IsActive { get; private set; }
        public Reward Reward { get; private set; }

        public List<GridCell> GridCells { get; set; } = new();

        public void Initialize(Vector2 doorSide, RoomType roomType, Room room, bool isUsable, Reward reward)
        {
            _doorSide = doorSide;
            _roomType = roomType;
            _room = room;
            IsUsable = isUsable;
            IsActive = false;
            if (IsUsable)
            {
                Reward = reward;
                _text.SetText(((AbilityReward)Reward).AbilityData.Name);
            }

            EventManager.Instance.AddListener<AllEnemiesAreDeadEvent>(ActivateDoor);
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener<AllEnemiesAreDeadEvent>(ActivateDoor);
        }

        private void ActivateDoor(object data)
        {
            IsActive = true;
        }

        private void SelectDoor()
        {
            if (!IsActive) return;

            if (Reward != null) Reward.GiveReward();
            _doorAnim.Animate(true);
            var doorData = new DoorData() { DoorSide = _doorSide, RoomType = _roomType, Room = _room };
            IsUsable = false;
            EventManager.Instance.TriggerEvent<DoorSelectedEvent>(new DoorSelectedEvent() { DoorData = doorData });
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsUsable) return;
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
                if (!IsUsable)
                {
                    DOVirtual.DelayedCall(2f, () =>
                    {
                        _doorAnim.Animate(false);
                    });
                }
            });
        }

        public void PrepareForAnimation()
        {
            _animation.PrepareForAnimation();
            _text.enabled = false;

            _doorAnim.SetState(!IsUsable);

        }

        private void PlaySpawnParticles()
        {
            var particle = ParticleManager.Instance.GetWallSpawnParticle();
            particle.transform.SetParent(transform);
            particle.transform.localPosition = Vector3.zero;
            particle.Play();
        }

        private void CloseInActiveDoor(object data)
        {
            _doorAnim.Animate(false);
        }
    }
}
