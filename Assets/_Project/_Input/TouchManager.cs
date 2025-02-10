using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    public static TouchManager instance;

    private PlayerInput playerInput;

    public InputAction touchPositionAction;
    public InputAction touchPressAction;
    public InputAction touchHoldAction;

    private void Awake()
    {
        instance = this;

        playerInput = GetComponent<PlayerInput>();
        touchPositionAction = playerInput.actions["TouchPosition"];
        touchPressAction = playerInput.actions["TouchPress"];
        touchHoldAction = playerInput.actions["TouchHold"];
    }
}
