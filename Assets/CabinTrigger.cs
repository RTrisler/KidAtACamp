using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinTrigger : PlayerGameTrigger
{
    protected override void FireStateEvent()
    {
        Debug.Log($"Firing!");
        if (DayController.Instance._dayState == DayState.Bedtime)
        {
            DayController.Instance.StartNextDay();
        }
    }
}
