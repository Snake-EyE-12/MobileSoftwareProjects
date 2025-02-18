using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private Margin margin;
    private void OnEnable()
    {
        HorseController.OnCrossFinish += OnHorseFinishedRace;
    }

    public void OnHorseFinishedRace(HorseController horse)
    {
        if (!horse.IsPlayer)
        {
            RoundController.instance.SetRaceResult(RaceResults.Lose);
            return;
        }

        if (horse.DistanceTraveled < margin.transform.position.x)
        {
            RoundController.instance.SetRaceResult(RaceResults.Win);
            return;
        }

        RoundController.instance.SetRaceResult(RaceResults.CaughtCheating);
    }

    [SerializeField] private TiledSpriteSizeSetter checkers;
    public void SetScale(float scale)
    {
        margin.transform.localScale = new Vector3(1, scale, 1);
        checkers.SetHeight(scale);
    }
}