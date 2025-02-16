using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Credits

public abstract class GameplayButton : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] protected bool canDrag;

    [SerializeField] protected Canvas canvas;
    [SerializeField] protected RectTransform draggingPlane;

    protected GameObject item;
    protected RectTransform itemRectTransform;
    protected Vector3 originalPosition;


    private void Awake()
    {
        item = gameObject;
        itemRectTransform = item.GetComponent<RectTransform>();
        originalPosition = itemRectTransform.position;
    }

    public void OnDrag(PointerEventData eventData) { if (canDrag) Move(eventData); }

    public void OnEndDrag(PointerEventData eventData) {
        if (canDrag)
        {
            if (Condition(eventData) == true) Drop(eventData);

            itemRectTransform.position = originalPosition;
        }
    }


    protected void Move(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
        {
            draggingPlane = eventData.pointerEnter.transform as RectTransform;
        }

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane, eventData.position, eventData.pressEventCamera, out Vector3 globalMousePosition))
        {
            itemRectTransform.position = globalMousePosition;
            itemRectTransform.rotation = draggingPlane.rotation;
        }
    }

    protected abstract bool Condition(PointerEventData eventData);

    protected abstract void Drop(PointerEventData eventData);

    public abstract void Press();
}
