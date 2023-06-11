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

    private Transform player;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        DayController.Instance.OnStateChange += OnDayStateChange;

        player = FindObjectOfType<MovementController>().gameObject.transform;
    }

    private Coroutine followCR;
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
                agent.SetDestination(WorldNavPointController.Instance.campFireNodes[camperIndex].position);
                break;
            case DayState.FreeRoam:
                agent.SetDestination(WorldNavPointController.Instance.freeRoamNodes[camperIndex].position);
                break;
            case DayState.FreeTimeMeetup:
                agent.SetDestination(WorldNavPointController.Instance.campFireNodes[camperIndex].position);
                break;
            case DayState.GuidedTask:
                // TODO: custom behavior per-day
                switch (day)
                {
                    case 1:
                        agent.SetDestination(WorldNavPointController.Instance.freeRoamNodes[camperIndex].position);
                        break;
                    // 2 they stay behind and do something secret
                    case 3:
                        followCR = StartCoroutine(FollowPlayer());
                        break;
                    case 4:
                        agent.SetDestination(WorldNavPointController.Instance.messHallNodes[camperIndex].position);
                        break;
                    default:
                        break;
                }
                break;
            case DayState.Dinner:
                if (followCR != null)
                {
                    StopCoroutine(followCR);
                }
                agent.SetDestination(WorldNavPointController.Instance.messHallNodes[camperIndex].position);
                break;
            case DayState.CampFire:
                agent.SetDestination(WorldNavPointController.Instance.campFireNodes[camperIndex].position);
                break;
            case DayState.Bedtime:
                agent.SetDestination(WorldNavPointController.Instance.cabinStartNodes[camperIndex].position);
                break;
            case DayState.FinalCeremony:
                agent.SetDestination(WorldNavPointController.Instance.ceremonyNodes[camperIndex].position);
                break;
            default:
                break;
        }
    }

    public float FollowThreshold = 10f;
    IEnumerator FollowPlayer()
    {
        while (true)
        {
            if (Vector3.Magnitude(transform.position - player.position) > FollowThreshold)
            {
                if (!agent.SetDestination( player.position + (player.position - transform.position).normalized * (FollowThreshold / 2)))
                {
                    Debug.LogError($"Unable to move");
                }
            }
            yield return new WaitForSeconds(Random.Range(1f, 5f));
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
