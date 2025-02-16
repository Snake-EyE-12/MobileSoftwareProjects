using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Credits

public abstract class GameplayButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform draggingPlane;

    private GameObject item;
    private RectTransform itemRectTransform;
    private Vector3 originalPosition;


    private void Awake()
    {
        item = gameObject;
        itemRectTransform = item.GetComponent<RectTransform>();
        originalPosition = itemRectTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // item.transform.SetParent(canvas.transform, false);
        // item.transform.SetAsLastSibling();

        Move(eventData);
    }

    public void OnDrag(PointerEventData eventData) { Move(eventData); }

    public void OnEndDrag(PointerEventData eventData) {
        if (Condition() == true) Drop(eventData);

        itemRectTransform.position = originalPosition;
    }


    private void Move(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
        {
            draggingPlane = eventData.pointerEnter.transform as RectTransform;
        }

        var rt = item.GetComponent<RectTransform>();

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane, eventData.position, eventData.pressEventCamera, out Vector3 globalMousePosition))
        {
            rt.position = globalMousePosition;
            rt.rotation = draggingPlane.rotation;
        }
    }

    protected abstract bool Condition();

    protected abstract void Drop(PointerEventData eventData);
}
