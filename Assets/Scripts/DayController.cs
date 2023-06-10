using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DayController : MonoBehaviour
{
    public static DayController Instance;
    public event Action<int, DayState> OnStateChange;

    public int _dayCounter; //integer value representation for the day number
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
            _dayCounter = 0;
            ChangeState(DayState.WakeUp);
        }
    }

    public void ChangeState(DayState newState)
    {
        _dayState = newState;
        if(newState == DayState.WakeUp)
        {
            _dayCounter++; //increases day
        }
        OnStateChange?.Invoke(_dayCounter, _dayState);
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
