using GamePlay.DetectionSystem;
using GamePlay.InputSystem;
using GamePlay.MovementSystem.PlayerMovements;
using UnityEngine;

public class PlayerController : ControllerBase
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerMovement _playerMovement;

    [field: SerializeField] public PlayerDetector PlayerDetector { get; private set; }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Debug.LogWarning($"I took {damage} damage.");
    }
}
