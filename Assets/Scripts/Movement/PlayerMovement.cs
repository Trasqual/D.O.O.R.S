using DG.Tweening;
using GamePlay.AnimationSystem.CharacterAnimations;
using GamePlay.EventSystem;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.MovementSystem.PlayerMovements
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MovementBase
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private PlayerAnimationManager _anim;
        [SerializeField] private float _movementSpeed = 8f;
        [SerializeField] private float _rotationSpeed = 50f;

        private Vector3 _moveAmount;

        protected override void Awake()
        {
            base.Awake();
            EventManager.Instance.AddListener<DoorSelectedEvent>(OnDoorSelected);
            EventManager.Instance.AddListener<RoomsAreSlidingEvent>(OnRoomsAreSliding);
            EventManager.Instance.AddListener<RoomSpawnAnimationFinishedEvent>(OnRoomSpawnAnimationFinished);
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener<DoorSelectedEvent>(OnDoorSelected);
            EventManager.Instance.RemoveListener<RoomsAreSlidingEvent>(OnRoomsAreSliding);
            EventManager.Instance.RemoveListener<RoomSpawnAnimationFinishedEvent>(OnRoomSpawnAnimationFinished);
        }

        private void OnDoorSelected(object data)
        {
            _canMove = false;
        }

        private void OnRoomsAreSliding(object data)
        {
            _moveAmount = ((RoomsAreSlidingEvent)data).SlideAmount;
            transform.DOMove(_moveAmount, 3f).SetRelative();
        }

        private void OnRoomSpawnAnimationFinished(object data)
        {
            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 100, 1))
            {
                transform.DOMove(hit.position - _moveAmount.normalized * 2f, 1f).OnUpdate(() =>
                {
                    _anim.SetMovement(1f);

                }).OnComplete(() =>
                {
                    _canMove = true;
                    _moveAmount = Vector3.zero;
                    _anim.SetMovement(0f);
                });
            }
        }

        public override void Move()
        {
            var movementAmount = Vector3.zero;
            if (_canMove)
            {
                movementAmount = _inputManager.Movement() * _movementSpeed;
                _controller.Move(movementAmount * Time.deltaTime);
                if (movementAmount != Vector3.zero)
                    _controller.transform.rotation = Quaternion.Lerp(_controller.transform.rotation, Quaternion.LookRotation(movementAmount), _rotationSpeed * Time.deltaTime);
            }
            _anim.SetMovement(movementAmount.magnitude);
        }

        private void Update()
        {
            Move();
        }
    }
}