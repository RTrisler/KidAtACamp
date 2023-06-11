using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class MeetingRoomTrigger : PlayerGameTrigger
{
    // protected override void FireStateEvent(Transform hit)
    // {
    //     if (DayController.Instance._dayState == DayState.MorningMeeting)
    //     {
    //         PlayMeetingRoomScene();
    //     }
    // }
    //
    // public void PlayMeetingRoomScene()
    // {
    //     InputController.Instance.SwitchInput(InputState.Dialogue);
    //     DialogueSingleton.Instance.GetComponent<DialogueRunner>().onDialogueComplete.AddListener(OnMeetingRoomDialogueComplete);
    //     DialogueSingleton.Instance.PlayNode($"PostFreetime_{DayController.Instance._dayCounter}");
    // }
    //
    // public void OnMeetingRoomDialogueComplete()
    // {
    //     DialogueSingleton.Instance.GetComponent<DialogueRunner>().onDialogueComplete.RemoveListener(OnMeetingRoomDialogueComplete);
    //     DayController.Instance.ChangeState(DayState.FreeRoam);
    //     InputController.Instance.SwitchInput(InputState.Defualt);
    // }
}
