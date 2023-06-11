using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public GameObject interactionPrompt;
    public static HUDController Instance;

    void Awake()
    {
        Instance = this;
        
        HideInteractionTip();
    }

    public void ShowInteractionTip()
    {
        interactionPrompt.SetActive(true);
    }

    public void HideInteractionTip()
    {
        interactionPrompt.SetActive(false);
    }
}
