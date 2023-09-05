using GamePlay.InputSystem;
using UnityEngine;

namespace GamePlay.MovementSystem
{
    public abstract class MovementBase : MonoBehaviour
    {
        [SerializeField] private InputBase _inputManager;

        protected Vector3 _movementVector;
        protected bool _canMove = true;

        protected virtual void Awake()
        {

        }

        public abstract void Move();
    }
}