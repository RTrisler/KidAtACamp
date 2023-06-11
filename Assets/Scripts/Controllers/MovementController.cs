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
    [SerializeField]
    private float _navMeshTargetDistance;
    
    private void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        // InputController.Instance.OnMovementInput += MovePlayer;
    }
    private void OnDisable()
    {
        // InputController.Instance.OnMovementInput -= MovePlayer;
    }

    // private void MovePlayer(Vector2 movementVector)
    // {
    //     _moveDirection = movementVector;
    // }
    public LayerMask terrainMask;
    private void Update()
    {
        // if (_moveDirection == new Vector2(1, 0) || _moveDirection == new Vector2(-1, 0))
        // {
        //     transform.Rotate(new Vector3(0, _rotationSpeed * _moveDirection.x, 0) * Time.deltaTime); //rotates the player around
        // }
        // _navAgent.destination = (transform.position + (transform.forward * _moveDirection.y * _navMeshTargetDistance)); //the position in front of the player for the navMesh to move towards
        
        // need current camera
        if (CameraZone._currentZone != null 
            && Input.GetMouseButtonUp(0)
            && !DialogueSingleton.Instance.runner.IsDialogueRunning)
        {
            var cam = CameraZone._currentZone.cam;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out var hit, float.MaxValue, terrainMask))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Terrain"))
                {
                    if (!_navAgent.SetDestination(hit.point))
                    {
                        Debug.LogError($"Attempting to navigate off-mesh");
                    }                    
                }
            }
        }
    }
}
