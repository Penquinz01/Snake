using UnityEngine;
using UnityEngine.InputSystem;
using System;



public class InputReader
{
    Control control;
    public Vector2 moveInput { get; private set; }
    public event Action OnMoveEvent;
    public InputReader()
    {
        control = new Control();
        Enable();

    }
    public void Dispose()
    {
        control.Player.Move.performed -= OnMove;
        control.Player.Disable();
    }
    public void Enable()
    {
        control.Player.Enable();
        control.Player.Move.performed += OnMove;
    }
    private void OnMove(InputAction.CallbackContext cxt)
    {
        moveInput = cxt.ReadValue<Vector2>();
        OnMoveEvent?.Invoke();
    }
}
