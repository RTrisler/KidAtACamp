using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DayController : MonoBehaviour
{
    public static DayController Instance;
    public event Action<int, DayState> OnStateChange;

    public int _dayCounter;
    public DayState _dayState;

    private void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            _dayCounter = 1;
            _dayState = DayState.WakeUp;
        }
    }

    public void ChangeState(DayState newState)
    {
        OnStateChange?.Invoke(_dayCounter, newState);
    }
}
public enum DayState
{
    WakeUp,
    MorningMeeting,
    Lunch,
    FreeRoam,
    GuidedTask,
    Dinner,
    CampFire,
    Bedtime
}
