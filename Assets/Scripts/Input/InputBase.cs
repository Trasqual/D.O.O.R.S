using System;
using UnityEngine;

public abstract class InputBase : MonoBehaviour
{
    public abstract Action<Vector3> OnInputDrag { get; set; }
}
