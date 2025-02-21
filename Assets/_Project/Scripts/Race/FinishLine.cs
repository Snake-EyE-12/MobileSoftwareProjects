using System;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private Margin margin;
    private void OnEnable()
    {
        HorseController.OnCrossFinish += OnHorseFinishedRace;
    }

    private void OnDisable()
    {
        HorseController.OnCrossFinish -= OnHorseFinishedRace;
    }

    public void OnHorseFinishedRace(HorseController horse)
    {
        if (!horse.IsPlayer)
        {
            RoundController.instance.DisplayRaceResult(RaceResults.Lose);
            return;
        }

        if (horse.DistanceTraveled < margin.transform.position.x)
        {
            RoundController.instance.DisplayRaceResult(RaceResults.Win);
            return;
        }

        RoundController.instance.DisplayRaceResult(RaceResults.CaughtCheating);
    }

    [SerializeField] private TiledSpriteSizeSetter checkers;
    public void SetScale(float scale)
    {
        margin.transform.localScale = new Vector3(1, scale, 1);
        checkers.SetHeight(scale);
    }
}