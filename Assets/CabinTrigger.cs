using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (DayController.Instance._dayState == DayState.Bedtime)
            {
                DayController.Instance.StartNextDay();
            }
        }
    }
}
