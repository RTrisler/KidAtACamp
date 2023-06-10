using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameTrigger : MonoBehaviour
{
    private bool fired = false;
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OTEnter {gameObject.name}: {other.CompareTag("Player") } {WorldNavPointController.Instance.CampersHaveArrived()}");
        if (other.CompareTag("Player") 
            && WorldNavPointController.Instance.CampersHaveArrived())
        {
            fired = true;
            FireStateEvent();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.Log($"OTStay {gameObject.name}: {other.CompareTag("Player") } {WorldNavPointController.Instance.CampersHaveArrived()} {fired}");
        if (other.CompareTag("Player") 
            && WorldNavPointController.Instance.CampersHaveArrived()
            && !fired)
        {
            fired = true;
            FireStateEvent();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log($"OTExit {gameObject.name}: {other.CompareTag("Player") }");
        if (other.CompareTag("Player"))
        {
            fired = false;
        }
    }

    protected virtual void FireStateEvent() { }
}
