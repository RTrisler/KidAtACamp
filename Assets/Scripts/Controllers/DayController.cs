using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class DayController : MonoBehaviour
{
    public static DayController Instance;
    public event Action<int, DayState> OnStateChange;
    public event Action<int> OnPickUpTask;
    public event Action OnAllItemsCollected;

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
        StartNextDay();
        StartCoroutine(DelayThenStartFirstDay());
    }

    IEnumerator DelayThenStartFirstDay()
    {
        StartCoroutine(SimpleFadeController.Instance.Fade(5f, fadeIn: false));
        yield return new WaitForSeconds(3f);
        ChangeState(DayState.Breakfast);
    }

    public void ChangeState(DayState newState)
    {
        _dayState = newState;
        Debug.Log(_dayState);

        if(newState == DayState.WakeUp)
        {
            _dayCounter++; //increases day
            Debug.Log($"Day {_dayCounter}!");
        }
        else if (newState == DayState.GuidedTask)
        {
            OnPickUpTask?.Invoke(dayGuidedPickupCount[_dayCounter]);
        }

		AudioController.StateChangeInit(newState);
        OnStateChange?.Invoke(_dayCounter, _dayState);
    }

    public void StartNextDay()
    {
        _pickUpCount = 0;
        ChangeState(DayState.WakeUp);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
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

    private Dictionary<int, int> dayGuidedPickupCount = new Dictionary<int, int>
    {
        { 1, 3 },
        { 2, 1 },
        {3, 1},
        {4, 7},
        {5, 1},
    };
    private void TrackPickUps(GuidedTaskInteractable guideInteractable)
    {
        _pickUpCount++;
        if(_pickUpCount == dayGuidedPickupCount[_dayCounter])
        {
            _pickUpCount = 0;
            ChangeState(DayState.Dinner);
            OnAllItemsCollected?.Invoke();
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
    Bedtime,
    FinalCeremony
}
