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

        return true;
    }

    protected override void Drop(PointerEventData eventData)
    {
        Vector3 spawnLocation = Camera.main.ScreenToWorldPoint(itemRectTransform.position);
        spawnLocation.z = 0;

        var newSpring = Instantiate(SpringPadPrefab, spawnLocation, Quaternion.identity, InstantiateTransform);
        newSpring.transform.SetParent(null);
    }

    public override void Press() {}
}
