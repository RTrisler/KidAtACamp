using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class MessHallPlayerTrigger : PlayerGameTrigger
{
    protected override void FireStateEvent(Transform hit)
    {
        if (DayController.Instance._dayState == DayState.Breakfast)
        {
            PlayBreakfastScene();
        }
        else if (DayController.Instance._dayState == DayState.Dinner)
        {
            PlayDinnerScene();
        }
    }

    public void PlayBreakfastScene()
    {
        InputController.Instance.SwitchInput(InputState.Dialogue);
        DialogueSingleton.Instance.GetComponent<DialogueRunner>().onDialogueComplete.AddListener(OnBreakfastDialogueComplete);
        DialogueSingleton.Instance.PlayNode($"Breakfast_{DayController.Instance._dayCounter}");
    }

    void OnBreakfastDialogueComplete()
    {
        DialogueSingleton.Instance.GetComponent<DialogueRunner>().onDialogueComplete.RemoveListener(OnBreakfastDialogueComplete);
        DayController.Instance.ChangeState(DayState.MorningMeeting);
        InputController.Instance.SwitchInput(InputState.Defualt);
    }
    
    public void PlayDinnerScene()
    {
        InputController.Instance.SwitchInput(InputState.Dialogue);
        DialogueSingleton.Instance.GetComponent<DialogueRunner>().onDialogueComplete.AddListener(OnDinnerDialogueComplete);
        DialogueSingleton.Instance.PlayNode($"Dinner_{DayController.Instance._dayCounter}");
    }

    void OnDinnerDialogueComplete()
    {
        DialogueSingleton.Instance.GetComponent<DialogueRunner>().onDialogueComplete.RemoveListener(OnDinnerDialogueComplete);
        DayController.Instance.ChangeState(DayState.CampFire);
        InputController.Instance.SwitchInput(InputState.Defualt);
    }
}
