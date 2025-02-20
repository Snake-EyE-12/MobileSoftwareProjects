using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

public class HorseTranquilizer : GameplayButton
{
    [SerializeField, MaxValue(0.9f)] private float speedMultiplier = 0.4f;
    [SerializeField] private float duration = 5;
    [SerializeField] private float detectionRadius = 10;

    private HorseController affectedHorse;

    private void Start() { canDrag = true; }

    protected override bool Condition(PointerEventData eventData)
    {
        affectedHorse = null;

        foreach (var horse in HorseController.horses)
        {
            if (Vector2.Distance(horse.transform.position, Camera.main.ScreenToWorldPoint(itemRectTransform.position)) <= detectionRadius)
            {
                if (affectedHorse != null && affectedHorse.IsPlayer) continue;  // Prioritize player
                else if (affectedHorse == null || horse.IsPlayer) affectedHorse = horse;
            }
        }

        if (affectedHorse != null) return true;
        else return false;
    }

    protected override void Drop(PointerEventData eventData) { affectedHorse.AddSpeedModifier(speedMultiplier, duration); }

    public override void Press() { }
}
