using System;
using UnityEngine;

namespace GamePlay.InputSystem
{
    public abstract class InputBase : MonoBehaviour
    {
        public abstract Vector3 Movement();
    }
}