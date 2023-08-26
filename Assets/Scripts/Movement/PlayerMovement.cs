using DG.Tweening;
using GamePlay.EventSystem;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.MovementSystem.PlayerMovements
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MovementBase
    {
        [SerializeField] private NavMeshAgent _agent;

        private Vector3 _moveAmount;

        protected override void Awake()
        {
            base.Awake();
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
            _canMove = false;
            _agent.isStopped = true;
            _agent.ResetPath();
            _agent.enabled = false;
            _movementVector = Vector3.zero;
            _moveAmount = ((RoomsAreSlidingEvent)data).SlideAmount;
            transform.DOMove(_moveAmount, 3f).SetRelative();
        }

        private void OnRoomSlidingEnded(object data)
        {
            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1, 1))
            {
                transform.DOMove(hit.position - _moveAmount.normalized * 2f, 1f).OnComplete(() =>
                {
                    _canMove = true;
                    _agent.enabled = true;
                    _agent.isStopped = false;
                    _movementVector = Vector3.zero;
                    _moveAmount = Vector3.zero;
                });
            }
        }

        public override void Move()
        {
            _agent.SetDestination(_movementVector);
        }

        private void Update()
        {
            if (_agent != null && _agent.enabled == true)
            {
                if (_movementVector != Vector3.zero && _canMove)
                {
                    Move();
                }
            }
        }
    }
}