using UnityEngine;
using UnityEngine.EventSystems;

public class SpringPadButton : GameplayButton
{
    [SerializeField] private Transform InstantiateTransform;
    [SerializeField] private GameObject SpringPadPrefab;

    private void Start() { canDrag = true; }

    protected override bool Condition(PointerEventData eventData)
    {
        // Check if dropped on race track?
        var lanes = FindObjectsByType<Lane>(FindObjectsSortMode.None);

        foreach (Lane lane in lanes)
        {
            BoxCollider2D collider = lane.GetComponent<BoxCollider2D>();
            Vector2 currentPosition = Camera.main.ScreenToWorldPoint(itemRectTransform.position);

            if (IsPointWithinCollider(collider, currentPosition) == true) return true; 
        }

        return false;
    }

    protected override void Drop(PointerEventData eventData)
    {
        Vector3 spawnLocation = Camera.main.ScreenToWorldPoint(itemRectTransform.position);
        spawnLocation.z = 0;

        var newSpring = Instantiate(SpringPadPrefab, spawnLocation, Quaternion.identity, InstantiateTransform);
        newSpring.transform.SetParent(null);
    }

    public override void Press() {}

    private bool IsPointWithinCollider(BoxCollider2D collider, Vector2 point) { return (collider.ClosestPoint(point) - point).sqrMagnitude < Mathf.Epsilon * Mathf.Epsilon; }
}
