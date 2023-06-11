using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinTrigger : PlayerGameTrigger
{
    public Transform wakeupSpawnPt;
    public float FadeDuration = 5f;
    public float InterFadeStallDuration = 3f;

    public static CabinTrigger Instance;
    public bool inFade = false;

    void Awake()
    {
        Instance = this;
    }
    protected override void FireStateEvent(Transform hit)
    {
        if (DayController.Instance._dayState == DayState.Bedtime && !inFade)
        {
            StartCoroutine(PlayFadesAndStartNewDay(hit));
        }
    }

    IEnumerator PlayFadesAndStartNewDay(Transform player)
    {
        inFade = true;
        InputController.Instance.SwitchInput(InputState.Dialogue);
        yield return SimpleFadeController.Instance.Fade(FadeDuration, fadeIn: true);
        yield return new WaitForSeconds(InterFadeStallDuration);
        DayController.Instance.StartNextDay();
        player.transform.position = wakeupSpawnPt.position;
        player.transform.forward = wakeupSpawnPt.forward;
        yield return SimpleFadeController.Instance.Fade(FadeDuration, fadeIn: false);
        DayController.Instance.ChangeState(DayState.Breakfast);
        InputController.Instance.SwitchInput(InputState.Defualt);
        inFade = false;
    }
}
