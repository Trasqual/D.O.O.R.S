using System;
using UnityEngine;

namespace InputSystem.PlayerInputs
{
    public class PlayerInput : InputBase
    {
        public override Action<Vector3> OnInputDrag { get; set; }

        [SerializeField] private LayerMask _mask;

        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButton(1))
            {
                var ray = _cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000, _mask))
                {
                    OnInputDrag?.Invoke(hitInfo.point);
                }
            }
        }
    }
}