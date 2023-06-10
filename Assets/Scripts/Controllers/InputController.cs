using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputController : MonoBehaviour, GameInput.IMovementActions
{
    public static InputController Instance;
    public event Action<InputState> OnInputStateChange;
    public event Action<Vector2> OnMovementInput;

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
            SwitchInput(InputState.Defualt);
        }
    }

    private void OnDisable()
    {
        _gameInput.Movement.Disable();
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
    #endregion
    public void SwitchInput(InputState newInputState)
    {
        _gameInputState = newInputState;
        switch (newInputState)
        {
            case InputState.Disable:
                _gameInput.Movement.Disable();
                break;
            case InputState.Defualt:
                _gameInput.Movement.Enable();
                break;
            case InputState.Trial1:
                _gameInput.Movement.Disable();
                break;
        }
    }
}
public enum InputState
{
    Disable,
    Defualt,
    Trial1,
    Trial2,
    Trial3,
    Trial4
}

