using UnityEngine;
using UnityEngine.EventSystems;

public class Kick : GameplayButton
{
    private HorseController playerHorse;

    private void Start() { canDrag = false; }

    protected override bool Condition(PointerEventData eventData) { return false; }

    protected override void Drop(PointerEventData eventData) {}

    public override void Press()
    {
        playerHorse = null;

        foreach (var horse in HorseController.horses) if (horse.IsPlayer) { playerHorse = horse; break; }

        playerHorse.Kick();
    }
}
