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
        control.Player.Enable();
        control.Player.Move.started +=OnMove;
    }
    private void OnMove(InputAction.CallbackContext cxt)
    {
        moveInput = cxt.ReadValue<Vector2>();
        OnMoveEvent?.Invoke();
    }
}
