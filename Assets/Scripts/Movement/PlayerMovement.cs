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
        //[SerializeField] private CharacterController _controller;
        [SerializeField] private NavMeshAgent _controller;
        [SerializeField] private PlayerAnimationManager _anim;
        [SerializeField] private float _movementSpeed = 8f;
        [SerializeField] private float _rotationSpeed = 50f;
        //[SerializeField] private float _gravity = -10f;

        private Vector3 _slideWithRoomAmount;
        private Vector3 _movement;

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
            _controller.enabled = false;
        }

        private void OnRoomsAreSliding(object data)
        {
            _slideWithRoomAmount = ((RoomsAreSlidingEvent)data).SlideAmount;
            transform.DOMove(_slideWithRoomAmount, 1f).SetRelative();
        }

        private void OnRoomSpawnAnimationFinished(object data)
        {
            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 100, 1))
            {
                transform.DOMove(hit.position - _slideWithRoomAmount.normalized * 2f, .5f).OnUpdate(() =>
                {
                    _anim.SetMovement(1f);

                }).OnComplete(() =>
                {
                    _canMove = true;
                    _controller.enabled = true;
                    _slideWithRoomAmount = Vector3.zero;
                    _anim.SetMovement(0f);
                    EventManager.Instance.TriggerEvent<CharacterEnteredNewRoomEvent>();
                });
            }
        }

        public override void Move()
        {
            if (_canMove)
            {
                _movement = new Vector3(_inputManager.Movement().x * _movementSpeed, _movement.y, _inputManager.Movement().z * _movementSpeed);

                //if (_controller.isGrounded)
                //{
                //    _movement.y = -0.1f;
                //}
                //else
                //{
                //    _movement.y += _gravity * Time.deltaTime;
                //}

                _controller.Move(_movement * Time.deltaTime);

                if (_inputManager.Movement() != Vector3.zero)
                    _controller.transform.rotation = Quaternion.Lerp(_controller.transform.rotation, Quaternion.LookRotation(_inputManager.Movement()), _rotationSpeed * Time.deltaTime);

                _anim.SetMovement(_inputManager.Movement().magnitude);
            }
            else
            {
                _anim.SetMovement(0f);
            }
        }

        private void Update()
        {
            Move();
        }
    }
}