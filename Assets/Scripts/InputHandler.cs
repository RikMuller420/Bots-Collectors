using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;

    public event Action<Vector2> MouseClicked;
    public event Action<Vector2> MouseMoved;

    public Vector2 MousePosition { get; private set; }

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Player.Click.performed += OnClick;
        _playerInput.Player.MouseMoved.performed += OnMouseMoved;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        MouseClicked?.Invoke(MousePosition);
    }

    private void OnMouseMoved(InputAction.CallbackContext context)
    {
        MousePosition = Mouse.current.position.ReadValue();
        MouseMoved?.Invoke(MousePosition);
    }
}
