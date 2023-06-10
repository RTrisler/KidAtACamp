using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;

public abstract class Interactable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<InteractionController>(out InteractionController interactionController))
        {
            interactionController.SetInteractable(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<InteractionController>(out InteractionController interactionController))
        {
            interactionController.SetInteractable(null);
        }
    }

    public abstract void Interact();
}
public class NPCInteractable : Interactable
{
    [SerializeField]
    private string _dialogueString;

    public override void Interact()
    {
        InputController.Instance.SwitchInput(InputState.Dialogue);
        DialogueSingleton.Instance.gameObject.GetComponent<Yarn.Unity.DialogueRunner>().onDialogueComplete.AddListener(OnDialogueCompleted);
        DialogueSingleton.Instance.PlayNode(_dialogueString);
    }

    private void OnDialogueCompleted()
    {
        InputController.Instance.SwitchInput(InputState.Defualt);
        DialogueSingleton.Instance.gameObject.GetComponent<Yarn.Unity.DialogueRunner>().onDialogueComplete.RemoveListener(OnDialogueCompleted);

    }
}