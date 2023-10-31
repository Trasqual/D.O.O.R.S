using GamePlay.InputSystem;
using GamePlay.MovementSystem.PlayerMovements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerMovement _playerMovement;

    [field: SerializeField] public PlayerDetector PlayerDetector { get; private set; }
}
