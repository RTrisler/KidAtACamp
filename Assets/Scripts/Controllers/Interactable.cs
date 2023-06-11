using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;
using System;

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
public class LoreNotes : Interactable
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
        Destroy(gameObject);
    }
}

public class GuidedTaskInteractable : Interactable
{
    public static event Action<GuidedTaskInteractable> OnGuidedTaskPickUp;
    private Vector3 _originalPosition;
    private void Awake()
    {
        _originalPosition = this.transform.position;
    }

    public override void Interact()
    {
        OnGuidedTaskPickUp?.Invoke(this);
    }
    public void MoveToOriginalPosition()
    {
        this.gameObject.transform.position = _originalPosition;
    }
}
