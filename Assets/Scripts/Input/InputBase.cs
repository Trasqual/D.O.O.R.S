using System;
using UnityEngine;

namespace InputSystem
{
    public abstract class InputBase : MonoBehaviour
    {
        public abstract Action<Vector3> OnInputDrag { get; set; }
    }
}