using System;
using NaughtyAttributes;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    [SerializeField] private RaceConstructor constructor;
    [SerializeField] private float tickDelay;
    [SerializeField] private Margin margin;
    [SerializeField] private Transform scalar;
    [Button]
    private void Build()
    {
        constructor.Construct(GetScaleMultiplier());
        scalar.localScale = GetScaleMultiplier() * Vector3.one;
    }
    
    private void Update()
    {
        AdvanceRace();
    }

    private float TimeToTickHorses;
    private void AdvanceRace()
    {
        if(!racing) return;
        if (TimeToTickHorses < Time.time) TickHorses();
        PositionMargin();
    }

    private float lastMarginPosition;
    private void PositionMargin()
    {
        lastMarginPosition = margin.SetMarginStartX(GetPositionOfNonPlayerFirstPlaceHorse());
    }

    [SerializeField] private float scaleMultiplier;
    public float GetScaleMultiplier()
    {
        return Screen.width / (float)Screen.height * scaleMultiplier;
    }

    private float GetPositionOfNonPlayerFirstPlaceHorse()
    {
        float furthest = -1;
        foreach (var horse in HorseData.horses)
        {
            if(horse.IsPlayer) continue;
            if (horse.GetTotalDistanceTraveled > furthest) furthest = horse.GetTotalDistanceTraveled;
        }

        return furthest;
    }

    private void TickHorses()
    {
        TimeToTickHorses = Time.time + tickDelay;
        foreach (var horse in HorseData.horses)
        {
            horse.Advance();
        }
    }

    private bool racing;
    [Button]
    public void BeginRace()
    {
        racing = true;
    }

    [Button]
    public void PauseRace()
    {
        racing = false;
    }


}