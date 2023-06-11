using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    private Vector3 _positionForPickUp;
    private void Start()
    {
        foreach(GuidedTaskPickUp guidePickUp in gameObject.GetComponentsInChildren<GuidedTaskPickUp>())
        {
            guidePickUp.transform.position = _positionForPickUp;
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
            foreach (GuidedTaskPickUp guidePickUp in gameObject.GetComponentsInChildren<GuidedTaskPickUp>())
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
