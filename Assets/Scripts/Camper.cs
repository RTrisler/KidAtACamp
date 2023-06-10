using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Camper : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    
    public void Goto(Vector3 position)
    {
        agent.SetDestination(position);
    }
    
    void Update()
    {
        bool isMoving = agent.velocity.magnitude > 0.25f;
        animator.SetBool("WalkingFwd", isMoving);
        if (isMoving)
        {
            animator.speed = agent.velocity.magnitude / agent.speed;
        }
        else
        {
            animator.speed = 1f;
        }
    }
}
