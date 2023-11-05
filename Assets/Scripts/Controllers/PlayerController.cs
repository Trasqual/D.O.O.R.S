using GamePlay.DetectionSystem;
using GamePlay.InputSystem;
using GamePlay.MovementSystem.PlayerMovements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private HealthManager _healthManager;

    [field: SerializeField] public PlayerDetector PlayerDetector { get; private set; }

    public void TakeDamage(float damage)
    {
        _healthManager.TakeDamage(damage);
    }
}
