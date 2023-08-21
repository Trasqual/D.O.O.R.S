using DG.Tweening;
using MovementSystem;
using UnityEngine;
using UnityEngine.AI;

namespace MovementSystem.PlayerMovements
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MovementBase
    {
        [SerializeField] private NavMeshAgent _agent;

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
            _agent.enabled = false;
            var moveAmount = ((RoomsAreSlidingEvent)data).SlideAmount;
            transform.DOMove(moveAmount, 3f).SetRelative();
        }

        private void OnRoomSlidingEnded(object data)
        {
            _canMove = true;
            _agent.enabled = true;
            _agent.isStopped = false;
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