using UnityEngine;
using UnityEngine.EventSystems;

public class AdrenalineHit : GameplayButton
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float duration;
    [SerializeField] private float detectionRadius;

    private HorseController affectedHorse;

    protected override bool Condition(PointerEventData eventData)
    {
        affectedHorse = null;

        foreach (HorseController horse in HorseController.horses)
        {
            if (Vector2.Distance(horse.transform.position, Camera.main.ScreenToWorldPoint(item.transform.position)) <= detectionRadius)
            {
                if (affectedHorse != null && affectedHorse.IsPlayer) continue;  // Prioritize player
                else if (affectedHorse == null || horse.IsPlayer) affectedHorse = horse;
            }
        }

        if (affectedHorse != null)
        {
            print("Horse found!");
            return true;
        }
        else return false;
    }

    protected override void Drop(PointerEventData eventData) { affectedHorse.AddSpeedModifier(speedMultiplier, duration); }
}
