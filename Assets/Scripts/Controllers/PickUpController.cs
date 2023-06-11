using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    private Vector3 _positionForPickUp;

    public Transform allPickupsController;
    private Dictionary<int, List<GuidedTaskPickUp>> dayPickups = new Dictionary<int, List<GuidedTaskPickUp>>();
    private void Start()
    {
        foreach (Transform child in transform)
        {
            var idx = child.GetSiblingIndex() + 1;
            dayPickups.Add(idx, new List<GuidedTaskPickUp>());
            foreach(GuidedTaskPickUp guidePickUp in child.GetComponentsInChildren<GuidedTaskPickUp>())
            {
                dayPickups[idx].Add(guidePickUp);
                guidePickUp.transform.position = _positionForPickUp;
            }    
        }
        
    }
    private void Awake()
    {
        DayController.Instance.OnStateChange += CheckState;
        GuidedTaskPickUp.OnGuidedTaskPickUp += ReturnPickUp;
    }
    private void OnDisable()
    {
        GuidedTaskPickUp.OnGuidedTaskPickUp -= ReturnPickUp;
        DayController.Instance.OnStateChange -= CheckState;
    }

    private void CheckState(int dayCount, DayState currentState)
    {
        if (currentState == DayState.GuidedTask)
        {
            Debug.Log("set active");
            foreach (GuidedTaskPickUp guidePickUp in dayPickups[DayController.Instance._dayCounter])
            {
                guidePickUp.MoveToOriginalPosition();
            }
        }
    }

    private void ReturnPickUp(GuidedTaskInteractable guideInteractable)
    {
        guideInteractable.transform.position = _positionForPickUp;
    }
}
