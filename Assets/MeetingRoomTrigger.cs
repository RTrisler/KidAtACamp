using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetingRoomTrigger : PlayerGameTrigger
{
    protected override void FireStateEvent()
    {
        Debug.Log($"Firing!");
        if (DayController.Instance._dayState == DayState.MorningMeeting)
        {
            DayController.Instance.ChangeState(DayState.FreeRoam);
        }
    }
}
