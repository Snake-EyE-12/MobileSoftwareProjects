using TMPro;
using UnityEngine;

public class RaceResultDisplay : MonoBehaviour
{
    [SerializeField] private GameObject resultScreen;
    [SerializeField] private TMP_Text text;
    
    public void SetState(RaceResults state)
    {
        resultScreen.SetActive(true);
        Invoke(nameof(End), 3);
        switch (state)
        {
            case RaceResults.Win:
                text.text = "You Win!";
                break;
            case RaceResults.Lose:
                text.text = "You Lose! :(";
                break;
            case RaceResults.CaughtCheating:
                text.text = "Caught Cheating!";
                break;
        }
    }

    private void End()
    {
        resultScreen.SetActive(false);
        RoundController.instance.NextRound();
    }
}