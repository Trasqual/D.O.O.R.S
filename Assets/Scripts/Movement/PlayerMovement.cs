using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MovementBase
{
    [SerializeField] private NavMeshAgent _agent;

    public override void Move()
    {
        _agent.SetDestination(_movementVector);
    }

    private void Update()
    {
        if (_agent != null)
        {
            if (_movementVector != Vector3.zero)
            {
                Move();
            }
        }
    }
}
