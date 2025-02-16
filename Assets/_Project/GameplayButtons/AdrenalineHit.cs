using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class AdrenalineHit : GameplayButton
{
    protected override bool Condition()
    {
        return false;
    }

    protected override void Drop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
