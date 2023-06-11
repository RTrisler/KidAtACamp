using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public GameObject interactionPrompt;
    public static HUDController Instance;

    void Awake()
    {
        Instance = this;

        ChangeInteractText("Click to move...");
    }

    public void ShowInteractionTip()
    {
        interactionPrompt.SetActive(true);
    }

    public void HideInteractionTip()
    {
        interactionPrompt.SetActive(false);
    }

    public void ChangeInteractText(string newText)
    {
        interactionPrompt.GetComponent<TextMeshProUGUI>().text = newText;
    }
}
