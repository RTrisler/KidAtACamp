using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private Interactable _interactable;
    [SerializeField]
    private float _interactionDistance;
    [SerializeField]
    private LayerMask _interactionMask;

    private void Start()
    {
        InputController.Instance.OnInteractPressed += Interact;
    }
    private void OnDisable()
    {
        InputController.Instance.OnInteractPressed -= Interact;
    }

    public void SetInteractable(Interactable interactable) //will assign the interactable to this script
    {
        if (interactable == null)
        {
            HUDController.Instance.HideInteractionTip();
        }
        else
        {
            HUDController.Instance.ShowInteractionTip();
        }
        _interactable = interactable;
    }

    private void Interact() //executes the interaction for the stored interactable
    {
        if(_interactable != null)
        {
            _interactable.Interact();
            HUDController.Instance.HideInteractionTip();
        }
    }
}
