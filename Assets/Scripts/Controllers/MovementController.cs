using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    private NavMeshAgent _navAgent;
    private Vector2 _moveDirection;
    [SerializeField]
    private float _rotationSpeed;
    private void Start()
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
        _moveDirection = movementVector;
    }
    private void Update()
    {
        if (_moveDirection == new Vector2(1, 0) || _moveDirection == new Vector2(-1, 0))
        {
            transform.Rotate(new Vector3(0, _rotationSpeed * _moveDirection.x, 0) * Time.deltaTime);
        }
        _navAgent.destination = (transform.position + (transform.forward * _moveDirection.y));
    }
}
