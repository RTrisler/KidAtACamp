using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireTrigger : PlayerGameTrigger
{
    protected override void FireStateEvent(Transform hit)
    {
        if (DayController.Instance._dayState == DayState.FreeTimeMeetup)
        {
            DayController.Instance.ChangeState(DayState.GuidedTask);
        }
        else if (DayController.Instance._dayState == DayState.CampFire)
        {
            DayController.Instance.ChangeState(DayState.Bedtime);
        }
    }
}
