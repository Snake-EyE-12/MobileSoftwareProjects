using UnityEngine;
using UnityEngine.InputSystem;

public class DragTest : MonoBehaviour
{
    [SerializeField] private bool grabbed = false;
    [SerializeField] private float grabRadius = 10;

    private void OnEnable()
    {
        TouchManager.instance.touchHoldAction.started += SetGrabbed;
        TouchManager.instance.touchHoldAction.canceled += SetGrabbed;
    }

    private void OnDisable()
    {
        TouchManager.instance.touchHoldAction.started -= SetGrabbed;
        TouchManager.instance.touchHoldAction.canceled -= SetGrabbed;
    }

    private void SetGrabbed(InputAction.CallbackContext context)
    {
        if (Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(TouchManager.instance.touchPositionAction.ReadValue<Vector2>())) < grabRadius & !grabbed) grabbed = true;
        else grabbed = false;

        Debug.Log(context.ReadValue<float>());
    }

    private void Update()
    {
        if (grabbed)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(TouchManager.instance.touchPositionAction.ReadValue<Vector2>());
            position.z = 0f;
            transform.position = position;
        }
    }
}
