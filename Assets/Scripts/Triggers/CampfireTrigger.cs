using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CampfireTrigger : PlayerGameTrigger
{
    protected override void FireStateEvent(Transform hit)
    {
        if (DayController.Instance._dayState == DayState.MorningMeeting)
        {
            PlayMorningMeetingScene();
        }
        else if (DayController.Instance._dayState == DayState.FreeTimeMeetup)
        {
            PlayPostFreetimeScene();
        }
        else if (DayController.Instance._dayState == DayState.CampFire)
        {
            PlayCampfireScene();
        }
    }
    
    public void PlayMorningMeetingScene()
    {
        InputController.Instance.SwitchInput(InputState.Dialogue);
        DialogueSingleton.Instance.GetComponent<DialogueRunner>().onDialogueComplete.AddListener(OnMorningMeetingDialogueComplete);
        DialogueSingleton.Instance.PlayNode($"MorningMeeting_{DayController.Instance._dayCounter}");
    }

    public void OnMorningMeetingDialogueComplete()
    {
        DialogueSingleton.Instance.GetComponent<DialogueRunner>().onDialogueComplete.RemoveListener(OnMorningMeetingDialogueComplete);
        DayController.Instance.ChangeState(DayState.FreeRoam);
        InputController.Instance.SwitchInput(InputState.Defualt);
    }
    public void PlayPostFreetimeScene()
    {
        InputController.Instance.SwitchInput(InputState.Dialogue);
        DialogueSingleton.Instance.GetComponent<DialogueRunner>().onDialogueComplete.AddListener(OnPostFreetimeDialogueComplete);
        DialogueSingleton.Instance.PlayNode($"PostFreetime_{DayController.Instance._dayCounter}");
    }

    public void OnPostFreetimeDialogueComplete()
    {
        DialogueSingleton.Instance.GetComponent<DialogueRunner>().onDialogueComplete.RemoveListener(OnPostFreetimeDialogueComplete);
        InputController.Instance.SwitchInput(InputState.Defualt);

        if (DayController.Instance._dayCounter != 5)
        {
            DayController.Instance.ChangeState(DayState.GuidedTask);    
        }
        else
        {
            DayController.Instance.ChangeState(DayState.FinalCeremony);
        }
        
    }
    
    public void PlayCampfireScene()
    {
        InputController.Instance.SwitchInput(InputState.Dialogue);
        DialogueSingleton.Instance.GetComponent<DialogueRunner>().onDialogueComplete.AddListener(OnCampfireDialogueComplete);
        DialogueSingleton.Instance.PlayNode($"Campfire_{DayController.Instance._dayCounter}");
    }

    public void OnCampfireDialogueComplete()
    {
        DialogueSingleton.Instance.GetComponent<DialogueRunner>().onDialogueComplete.RemoveListener(OnCampfireDialogueComplete);
        DayController.Instance.ChangeState(DayState.Bedtime);
        InputController.Instance.SwitchInput(InputState.Defualt);
    }
}
