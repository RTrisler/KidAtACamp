using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DayController))]
public class FreeTimeTimerController : MonoBehaviour
{
    public float FreeTimeDuration = 10;
    private bool timerRunning = false;
    void Start()
    {
        DayController.Instance.OnStateChange += OnStateChange;
    }

    void OnStateChange(int day, DayState state)
    {
        // interrupted, probably by debug
        if (timerRunning)
        {
            EndFreeTime();
            return;
        }
        
        if (state == DayState.FreeRoam)
        {
            IEnumerator DelayThenChangeState()
            {
                yield return new WaitForSeconds(FreeTimeDuration);
                EndFreeTime();
            }
            timerRunning = true;
            StartCoroutine(DelayThenChangeState());
        }
    }

    public void EndFreeTime()
    {
        StopAllCoroutines();
        timerRunning = false;
        DayController.Instance.ChangeState(DayState.FreeTimeMeetup);
    }
}
