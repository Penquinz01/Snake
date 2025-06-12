using UnityEngine;
using UnityEngine.InputSystem;



public class InputReader
{
    Control control;
    public Vector2 moveInput { get; private set; }
    public InputReader()
    {
        control = new Control();
        control.Player.Enable();
        control.Player.Move.performed +=(InputAction.CallbackContext cxt)=>moveInput=cxt.ReadValue<Vector2>();
    }
}
