using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayButton : MonoBehaviour
{
    [SerializeField] protected bool moving = false;
    [SerializeField] protected bool dropped = false;
    
    public float grabRadius = 10;

    private void OnEnable()
    {
        TouchManager.instance.touchHoldAction.started += Move;
        TouchManager.instance.touchHoldAction.canceled += Drop;
    }

    private void OnDisable()
    {
        TouchManager.instance.touchHoldAction.started -= Move;
        TouchManager.instance.touchHoldAction.canceled -= Drop;
    }

    private void Move(InputAction.CallbackContext context)
    {
        if (Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(
            TouchManager.instance.touchPositionAction.ReadValue<Vector2>()
            )) < grabRadius & !moving)
            moving = true;
    }

    private void Drop(InputAction.CallbackContext context)
    {
        if (moving)
        {
            moving = false;
            dropped = true;
        }
    }
}
