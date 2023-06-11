using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class FinalCeremonyTrigger : PlayerGameTrigger
{
    protected override void FireStateEvent(Transform hit)
    {
        if (DayController.Instance._dayState == DayState.FinalCeremony)
        {
            StartFinalCeremonyScene();
        }
    }

    void StartFinalCeremonyScene()
    {
        InputController.Instance.SwitchInput(InputState.Dialogue);
        DialogueSingleton.Instance.GetComponent<DialogueRunner>().onDialogueComplete.AddListener(OnStartFinalCeremonyComplete);
        DialogueSingleton.Instance.PlayNode($"FinalCeremony");
    }
    
    public void OnStartFinalCeremonyComplete()
    {
        DialogueSingleton.Instance.GetComponent<DialogueRunner>().onDialogueComplete.RemoveListener(OnStartFinalCeremonyComplete);

        IEnumerator FinalCutscene()
        {
            yield return SimpleFadeController.Instance.Fade(10f, fadeIn: true);
            Application.Quit();
            Debug.Log($"You win");
        }

        StartCoroutine(FinalCutscene());

    }
}
