using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Camper : MonoBehaviour
{
    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public void Goto(Vector3 position)
    {
        agent.SetDestination(position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            agent.updatePosition = false;
            agent.updateRotation = true;
            
            Debug.Log($"Pos: {agent.updatePosition}  Rot: {agent.updateRotation}");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            agent.updatePosition = true;
            agent.updateRotation = false;
            
            Debug.Log($"Pos: {agent.updatePosition}  Rot: {agent.updateRotation}");
        }
    }
}
