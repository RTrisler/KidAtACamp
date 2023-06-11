using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public GameObject interactionPrompt;
    public GameObject pickUpCounter;
    private int _pickUpCount;
    private int _pickUpTotal;
    public static HUDController Instance;

    void Awake()
    {
        Instance = this;

        ChangeInteractText("Click to move...");
        HidePickUp();
        DayController.Instance.OnPickUpTask += ShowPickUp;
        DayController.Instance.OnAllItemsCollected += HidePickUp;
        GuidedTaskPickUp.OnGuidedTaskPickUp += IncreaseCount;
    }

    private void OnDisable()
    {
        DayController.Instance.OnPickUpTask -= ShowPickUp;
        GuidedTaskPickUp.OnGuidedTaskPickUp -= IncreaseCount;
        DayController.Instance.OnAllItemsCollected -= HidePickUp;
    }

    public void ShowInteractionTip()
    {
        interactionPrompt.SetActive(true);
    }

    public void ShowPickUp(int pickUpTotal)
    {
        _pickUpTotal = pickUpTotal;
        _pickUpCount = 0;
        pickUpCounter.SetActive(true);
        pickUpCounter.GetComponent<TextMeshProUGUI>().text = _pickUpCount + "/" + _pickUpTotal;
    }

    public void IncreaseCount(GuidedTaskInteractable guidedTask)
    {
        _pickUpCount++;
        pickUpCounter.GetComponent<TextMeshProUGUI>().text = _pickUpCount + "/" + _pickUpTotal;
    }

    public void HidePickUp()
    {
        pickUpCounter.SetActive(false);
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
