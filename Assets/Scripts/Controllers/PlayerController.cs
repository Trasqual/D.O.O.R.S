using GamePlay.DetectionSystem;
using GamePlay.EventSystem;
using GamePlay.InputSystem;
using GamePlay.MovementSystem.PlayerMovements;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Entities.Controllers
{
    public class PlayerController : ControllerBase
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private NavMeshAgent _agent;

        [field: SerializeField] public PlayerDetector PlayerDetector { get; private set; }

        private void Awake()
        {
            EventManager.Instance.AddListener<RoomSpawnAnimationFinishedEvent>(ActivateAgent);
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener<RoomSpawnAnimationFinishedEvent>(ActivateAgent);
        }

        private void ActivateAgent(object data)
        {
            _agent.enabled = true;
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            Debug.LogWarning($"I took {damage} damage.");
        }
    }
}