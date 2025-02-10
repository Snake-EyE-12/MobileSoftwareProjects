using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndUI : MonoBehaviour {
    [SerializeField] GameObject winP;
    [SerializeField] GameObject loseP;
    [SerializeField] TextMeshProUGUI moneyTxt;
    public int money;

    public void resultsScreen(bool win) {
        if (win) {
            winP.SetActive(true);
            moneyTxt.text = "Your winnings: $" + money + "!!!"; 
        } else {
            loseP.SetActive(true);
        }
    }

    public void againClick() {
        SceneManager.LoadScene("Betting");
    }

    public void mainClick() {
        SceneManager.LoadScene("MainMenu");
    }
}