using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputController : MonoBehaviour, GameInput.IMovementActions, GameInput.IInteractionActions
{
    public static InputController Instance;
    public event Action<InputState> OnInputStateChange;
    public event Action<Vector2> OnMovementInput;
    public event Action OnInteractPressed;

    private GameInput _gameInput;
    private InputState _gameInputState;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log(this);
            Instance = this;
            _gameInput = new GameInput();
            _gameInput.Movement.SetCallbacks(this);
            _gameInput.Interaction.SetCallbacks(this);
            SwitchInput(InputState.Defualt);
        }
    }

    private void OnDisable()
    {
        DisableInputs();
    }
    #region InputMethods
    public void OnMovement(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Canceled)
        {
            OnMovementInput?.Invoke(new Vector3(0,0,0));
        }
        else
        {
            OnMovementInput?.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            OnInteractPressed?.Invoke();
        }
    }
    #endregion
    public void SwitchInput(InputState newInputState)
    {
        _gameInputState = newInputState;
        switch (newInputState)
        {
            case InputState.Disable:
                DisableInputs();
                break;
            case InputState.Dialogue:
                _gameInput.Movement.Disable();
                break;
            case InputState.Defualt:
                EnableInputs();
                break;
            case InputState.Trial1:
                _gameInput.Movement.Disable();
                break;
        }
    }

    private void DisableInputs()
    {
        _gameInput.Movement.Disable();
        _gameInput.Interaction.Disable();
    }

    private void EnableInputs()
    {
        _gameInput.Movement.Enable();
        _gameInput.Interaction.Enable();
    }
}
public enum InputState
{
    Disable,
    Dialogue,
    Defualt,
    Trial1,
    Trial2,
    Trial3,
    Trial4
}

