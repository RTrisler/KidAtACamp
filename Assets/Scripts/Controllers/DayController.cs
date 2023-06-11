using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class DayController : MonoBehaviour
{
    public static DayController Instance;
    public event Action<int, DayState> OnStateChange;

    [HideInInspector]
    public int _dayCounter; //integer value representation for the day number
    [HideInInspector]
    public DayState _dayState;
    private int _pickUpCount;

    private void OnEnable()
    {
        GuidedTaskInteractable.OnGuidedTaskPickUp += TrackPickUps;
    }
    private void OnDisable()
    {
        GuidedTaskInteractable.OnGuidedTaskPickUp -= TrackPickUps;
    }

	[SerializeField]
	AudioController AudioController;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        StartDay(0);
    }

    public void ChangeState(DayState newState)
    {
        _dayState = newState;

        if(newState == DayState.WakeUp)
        {
            _dayCounter++; //increases day
            Debug.Log($"Day {_dayCounter}!");
			AudioController.StartDay();
        }
		else if (newState == DayState.FreeTimeMeetup)
		{
			AudioController.SummonChildren();
		}

        OnStateChange?.Invoke(_dayCounter, _dayState);
    }

    void StartDay(int day)
    {
        _dayCounter = day;
        _pickUpCount = 0;
        ChangeState(DayState.WakeUp);
    }

    public void StartNextDay()
    {
        StartDay(_dayCounter + 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GotoNextState();
        }
    }
    
    public void GotoNextState()
    {
        
        var states = (DayState[])Enum.GetValues(typeof(DayState));
        var idx = states.ToList().IndexOf(_dayState);
        idx = idx >= states.Length-1 ? 0 : idx + 1;
        Debug.Log($"Forcing next state: {states[idx].ToString()}");
        ChangeState(states[idx]);
        
    }

    private void TrackPickUps()
    {
        _pickUpCount++;
        if(_pickUpCount == 5)
        {
            _pickUpCount = 0;
            ChangeState(DayState.Dinner);
        }
    }
}
public enum DayState
{
    WakeUp,
    Breakfast,
    MorningMeeting,
    FreeRoam,
    FreeTimeMeetup,
    GuidedTask,
    Dinner,
    CampFire,
    Bedtime
}
