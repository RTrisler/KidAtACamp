using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DayController))]
public class FreeTimeTimerController : MonoBehaviour
{
    public float FreeTimeDuration = 10;
    void Start()
    {
        DayController.Instance.OnStateChange += OnStateChange;
    }

    void OnStateChange(int day, DayState state)
    {
        if (state == DayState.FreeRoam)
        {
            IEnumerator DelayThenChangeState()
            {
                yield return new WaitForSeconds(FreeTimeDuration);
                DayController.Instance.ChangeState(DayState.FreeTimeMeetup);
            }

            StartCoroutine(DelayThenChangeState());
        }
    }
}
