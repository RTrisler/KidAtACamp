using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetingRoomTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (DayController.Instance._dayState == DayState.MorningMeeting)
            {
                DayController.Instance.ChangeState(DayState.FreeRoam);
            }
        }
    }
}
