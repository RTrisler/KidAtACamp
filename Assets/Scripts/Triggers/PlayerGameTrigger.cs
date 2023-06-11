using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameTrigger : MonoBehaviour
{
    public bool requiresAllCampers = true;
    private bool fired = false;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (requiresAllCampers && !WorldNavPointController.Instance.CampersHaveArrived()) return;
            
            fired = true;
            FireStateEvent(other.transform);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") 
            && !fired)
        {
            if (requiresAllCampers && !WorldNavPointController.Instance.CampersHaveArrived()) return;
            
            fired = true;
            FireStateEvent(other.transform);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fired = false;
        }
    }

    protected virtual void FireStateEvent(Transform hit) { }
}
