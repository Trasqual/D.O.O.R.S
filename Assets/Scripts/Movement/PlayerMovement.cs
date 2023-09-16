using DG.Tweening;
using GamePlay.CommandSystem;
using GamePlay.EventSystem;
using UnityEngine;

namespace GamePlay.MovementSystem.PlayerMovements
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MovementBase
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _movementSpeed = 8f;

        private Vector3 _moveAmount;

        protected override void Awake()
        {
            base.Awake();
            EventManager.Instance.AddListener<DoorSelectedEvent>(OnDoorSelected);
            EventManager.Instance.AddListener<RoomsAreSlidingEvent>(OnRoomsAreSliding);
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener<DoorSelectedEvent>(OnDoorSelected);
            EventManager.Instance.RemoveListener<RoomsAreSlidingEvent>(OnRoomsAreSliding);
        }

        private void OnDoorSelected(object data)
        {
            _canMove = false;
            var movePlayerToNewRoomCommand = new MovePlayerToNewRoomCommand(transform, _moveAmount, () =>
            {
                OnPlayerInPosition();
            }, RoomCreationPriority.PlayerToNewRoom);

            CommandManager.Instance.AddCommand(movePlayerToNewRoomCommand);
        }

        private void OnRoomsAreSliding(object data)
        {
            _moveAmount = ((RoomsAreSlidingEvent)data).SlideAmount;
            transform.DOMove(_moveAmount, 3f).SetRelative();
        }

        private void OnPlayerInPosition()
        {
            _canMove = true;
            _moveAmount = Vector3.zero;
        }

        public override void Move()
        {
            if (_canMove)
            {
                _controller.Move(_inputManager.Movement() * _movementSpeed * Time.deltaTime);
            }
        }

        private void Update()
        {
            Move();
        }
    }
}