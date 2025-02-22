using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndUI : MonoBehaviour {
    [SerializeField] GameObject winP;
    [SerializeField] GameObject loseP;
    [SerializeField] TextMeshProUGUI moneyTxt;
    public int money;

    public void Start()
    {
        resultsScreen(RoundController.instance.gameWon);
        money = RoundController.instance.money;
    }

    public void resultsScreen(bool win) {
        if (win) {
            winP.SetActive(true);
            moneyTxt.text = "Your winnings: $" + money + "!!!"; 
        } else {
            loseP.SetActive(true);
        }

        RoundController.instance.ResetGame();
    }

    public void againClick() {
        RoundController.instance.State = GameState.BETTING;
    }

    public void mainClick() {
        RoundController.instance.State = GameState.MAIN;
    }
}