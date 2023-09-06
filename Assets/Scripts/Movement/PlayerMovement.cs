using DG.Tweening;
using GamePlay.EventSystem;
using UnityEngine;
using UnityEngine.AI;

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
            EventManager.Instance.AddListener<RoomsAreSlidingEvent>(OnRoomsAreSliding);
            EventManager.Instance.AddListener<RoomSpawnAnimationFinishedEvent>(OnRoomSpawnAnimationFinished);
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener<RoomsAreSlidingEvent>(OnRoomsAreSliding);
            EventManager.Instance.RemoveListener<RoomSpawnAnimationFinishedEvent>(OnRoomSpawnAnimationFinished);
        }

        private void OnRoomsAreSliding(object data)
        {
            _canMove = false;
            _moveAmount = ((RoomsAreSlidingEvent)data).SlideAmount;
            transform.DOMove(_moveAmount, 3f).SetRelative();
        }

        private void OnRoomSpawnAnimationFinished(object data)
        {
            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 100, 1))
            {
                transform.DOMove(hit.position - _moveAmount.normalized * 2f, 1f).OnComplete(() =>
                {
                    _canMove = true;
                    _moveAmount = Vector3.zero;
                });
            }
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