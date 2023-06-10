using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"?");
        if (other.CompareTag("Player"))
        {
            Debug.Log($"??");
            if (DayController.Instance._dayState == DayState.FreeTimeMeetup)
            {
                Debug.Log($"???");
                DayController.Instance.ChangeState(DayState.GuidedTask);
            }
            else if (DayController.Instance._dayState == DayState.CampFire)
            {
                Debug.Log($"----");
                DayController.Instance.ChangeState(DayState.Bedtime);
            }
        }
    }
}
