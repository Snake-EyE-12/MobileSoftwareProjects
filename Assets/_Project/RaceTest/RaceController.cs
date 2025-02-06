using System;
using NaughtyAttributes;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    [SerializeField] private RaceConstructor constructor;
    [SerializeField] private float tickDelay;
    [SerializeField] private Margin margin;
    [SerializeField] private Transform scalar;
    [SerializeField] private float raceDistance;
    [SerializeField] private Transform endFlag;
    [Button]
    public void Build()
    {
        constructor.Construct(GetScaleMultiplier());
        endFlag.position = new Vector3(raceDistance, endFlag.position.y, endFlag.position.z);
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
        CheckWin();
    }

    private void CheckWin() //Player Wins Ties atm
    {
        HorseData winningHorse = null;
        float furthestDistance = -1;
        foreach (var horse in HorseData.horses)
        {
            if(horse.IsPlayer) continue;
            if (horse.GetTotalDistanceTraveled > furthestDistance)
            {
                winningHorse = horse;
                furthestDistance = horse.GetTotalDistanceTraveled;
            }
        }
        if (furthestDistance > raceDistance + 2)
        {
            OnHorseReachedEnd(winningHorse, furthestDistance);
            Debug.Log("We Have A Winner");
        }
    }

    private void OnHorseReachedEnd(HorseData horse, float distance)
    {
        racing = false;
        if (!horse.IsPlayer)
        {
            PlayerLose();
            return;
        }

        if (distance > lastMarginPosition)
        {
            PlayerCaughtCheating();
            return;
        }
        PlayerWin();
    }

    private void PlayerLose()
    {
        RoundController.instance.RaceResults(RaceResults.Lose);
    }

    private void PlayerCaughtCheating()
    {
        RoundController.instance.RaceResults(RaceResults.CaughtCheating);
    }

    private void PlayerWin()
    {
        RoundController.instance.RaceResults(RaceResults.Win);
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

public enum RaceResults
{
    Win,
    Lose,
    CaughtCheating
}