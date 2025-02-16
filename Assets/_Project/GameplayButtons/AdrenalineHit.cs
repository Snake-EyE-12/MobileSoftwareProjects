using UnityEngine;
using UnityEngine.InputSystem;

public class AdrenalineHit : GameplayButton
{
    private void Update()
    {
        if (moving)
        {
            // Dragging

            Vector3 position = Camera.main.ScreenToWorldPoint(TouchManager.instance.touchPositionAction.ReadValue<Vector2>());
            position.z = 0f;
            transform.position = position;
        }
        else if (dropped)
        {
            // Gameplay Button Function

            dropped = false;

            
        }
    }
}
