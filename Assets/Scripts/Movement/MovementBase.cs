using UnityEngine;

public abstract class MovementBase : MonoBehaviour
{
    [SerializeField] private InputBase _inputManager;

    protected Vector3 _movementVector;

    private void Awake()
    {
        _inputManager.OnInputDrag += OnInputDrag;
    }

    private void OnInputDrag(Vector3 input)
    {
        _movementVector = input;
    }

    public abstract void Move();
}
