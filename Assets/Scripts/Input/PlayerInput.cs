using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace GamePlay.InputSystem
{
    public class PlayerInput : InputBase
    {
        public Action OnDashPressed;

        [field: SerializeField] public bool UseMouseMovement { get; private set; }

        private PlayerInputActions _playerInput;
        private Camera _cam;
        private bool _isRightClickPressed;

        private void Awake()
        {
            _cam = Camera.main;
            _playerInput = new PlayerInputActions();
            _playerInput.Player.Enable();
            SubscribeToInputs();
        }

        private void SubscribeToInputs()
        {
            _playerInput.Player.Dash.started += DashPressed;
            _playerInput.Player.RightClick.started += RightClickPressed;
            _playerInput.Player.RightClick.canceled += RightClickCanceled;
        }

        public override Vector3 Movement()
        {
            var movement = _playerInput.Player.Movement.ReadValue<Vector2>();
            bool isUsingMouse = _isRightClickPressed && _playerInput.Player.Look.activeControl.device.displayName == "Mouse";
            var calculatedVector = isUsingMouse ? Look() : new Vector3(movement.x, 0f, movement.y);
            return Quaternion.Euler(0f, 45f, 0f) * calculatedVector;
        }

        public Vector3 Look()
        {
            var readLookVector = _playerInput.Player.Look.ReadValue<Vector2>();
            var look = Vector3.zero;
            if (_playerInput.Player.Look.activeControl != null)
            {
                if (_playerInput.Player.Look.activeControl.device.displayName == "Mouse")
                {
                    look = GetAxisFromMousePos(readLookVector);
                }
                else
                {
                    look = new Vector3(readLookVector.x, 0f, readLookVector.y);
                }
            }

            return UseMouseMovement ? (_isRightClickPressed ? look : Vector3.zero) : look;
        }

        private void RightClickPressed(InputAction.CallbackContext ctx)
        {
            _isRightClickPressed = true;
        }

        private void RightClickCanceled(InputAction.CallbackContext ctx)
        {
            _isRightClickPressed = false;
        }

        private Vector3 GetAxisFromMousePos(Vector2 mousePosition)
        {
            var playerPos = _cam.WorldToScreenPoint(transform.position);
            playerPos.z = 0f;
            var dir = (Vector3)mousePosition - playerPos;
            dir.Normalize();

            dir.z = dir.y;
            dir.y = 0f;
            dir = Quaternion.Euler(0f, _cam.transform.rotation.y, 0f) * dir;
            return dir;
        }

        private void DashPressed(InputAction.CallbackContext ctx)
        {
            OnDashPressed?.Invoke();
        }
    }
}