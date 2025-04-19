using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;

    public event Action MouseClicked;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Player.Click.performed += OnClick;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            MouseClicked?.Invoke();
        }
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        MouseClicked?.Invoke();
    }
}
