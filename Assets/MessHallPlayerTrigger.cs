using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessHallPlayerTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (DayController.Instance._dayState == DayState.Breakfast)
            {
                DayController.Instance.ChangeState(DayState.MorningMeeting);
            }
            else if (DayController.Instance._dayState == DayState.Dinner)
            {
                DayController.Instance.ChangeState(DayState.CampFire);
            }
        }
    }
}
