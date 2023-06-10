using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    private NavMeshAgent _navAgent;
    private void OnEnable()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        InputController.Instance.OnMovementInput += MovePlayer;
    }
    private void OnDisable()
    {
        InputController.Instance.OnMovementInput -= MovePlayer;
    }

    private void MovePlayer(Vector2 movementVector)
    {
        if (movementVector == new Vector2(1, 0) || movementVector == new Vector2(-1, 0))
        {
            var currentPosition = this.transform.position;
            //_navAgent.SetDestination(new Vector3(currentPosition.x + movementVector.x, currentPosition.));
        }
        else if (movementVector == new Vector2(0, 1) || movementVector == new Vector2(0, -1)) 
        {

        }
    }
}
