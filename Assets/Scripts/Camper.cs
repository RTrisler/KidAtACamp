using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Camper : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent agent;
    private Animator animator;
    [HideInInspector]
    public int camperIndex;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        DayController.Instance.OnStateChange += OnDayStateChange;
    }

    void OnDayStateChange(int day, DayState state)
    {
        switch (state)
        {
            case DayState.WakeUp:
                agent.Warp(WorldNavPointController.Instance.cabinStartNodes[camperIndex].position);
                break;
            case DayState.Breakfast:
                agent.SetDestination(WorldNavPointController.Instance.messHallNodes[camperIndex].position);
                break;
            case DayState.MorningMeeting:
                agent.SetDestination(WorldNavPointController.Instance.morningMeetingNodes[camperIndex].position);
                break;
            case DayState.FreeRoam:
                agent.SetDestination(WorldNavPointController.Instance.freeRoamNodes[camperIndex].position);
                break;
            case DayState.FreeTimeMeetup:
                agent.SetDestination(WorldNavPointController.Instance.campFireNodes[camperIndex].position);
                break;
            case DayState.GuidedTask:
                break;
            case DayState.Dinner:
                agent.SetDestination(WorldNavPointController.Instance.messHallNodes[camperIndex].position);
                break;
            case DayState.CampFire:
                agent.SetDestination(WorldNavPointController.Instance.campFireNodes[camperIndex].position);
                break;
            case DayState.Bedtime:
                agent.SetDestination(WorldNavPointController.Instance.cabinStartNodes[camperIndex].position);
                break;
            default:
                break;
        }
    }
    
    public void Goto(Vector3 position)
    {
        agent.SetDestination(position);
    }
    
    void Update()
    {
        // this is fucking horrible
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
    
    public bool Arrived()
    {
        if (agent.remainingDistance <= 0.25f)
        {
            return true;
        }

        return false;
    }
}
