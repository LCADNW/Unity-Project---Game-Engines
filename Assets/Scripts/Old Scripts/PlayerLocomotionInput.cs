using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotionInput : MonoBehaviour, PlayerControls.IPlayerLocomotionMapActions
{
  public PlayerControls PlayerControl {  get; private set; }
    public Vector2 MovementInput {  get; private set; }

    private void OnEnable()
    {
        PlayerControl = new PlayerControls();
        PlayerControl.Enable();

        PlayerControl.PlayerLocomotionMap.Enable();
       PlayerControl.PlayerLocomotionMap.SetCallbacks(this);
    }

    private void OnDisable()
    {
        PlayerControl.PlayerLocomotionMap.Disable();
        PlayerControl.PlayerLocomotionMap.RemoveCallbacks(this);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
        print(MovementInput); 
    }
}
