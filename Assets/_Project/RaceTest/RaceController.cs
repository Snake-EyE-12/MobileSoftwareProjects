using System;
using NaughtyAttributes;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    [SerializeField] private RaceConstructor constructor;
    [SerializeField] private float tickDelay;
    [SerializeField] private Margin margin;
    
    [Button]
    private void Build()
    {
        constructor.LaneCount = 4;
        constructor.Construct();
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

    private void PositionMargin()
    {
        margin.SetMarginStartX(GetPositionOfNonPlayerFirstPlaceHorse().x);
    }

    private Vector3 GetPositionOfNonPlayerFirstPlaceHorse()
    {
        Vector3 furthest = Vector3.negativeInfinity;
        foreach (var lane in constructor.GetLanes())
        {
            if(lane is PlayerLane) continue;
            if(lane.GetHorsePosition().x > furthest.x) furthest = lane.GetHorsePosition();
        }

        return furthest;
    }

    private void TickHorses()
    {
        TimeToTickHorses = Time.time + tickDelay;
        foreach (var lane in constructor.GetLanes())
        {
            lane.AdvanceHorses();
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