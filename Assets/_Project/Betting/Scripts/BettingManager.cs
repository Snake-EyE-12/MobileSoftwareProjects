using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BettingManager : MonoBehaviour {
    [SerializeField] int totalMoney;
    [SerializeField] int betAmount;
    [SerializeField] int changeBetAmount;
    [SerializeField] string feedback;
    [SerializeField] float suspicion;
    [SerializeField] float maxSuspicion;
    [SerializeField] TextMeshProUGUI totalMoneyTxt;
    [SerializeField] TextMeshProUGUI betMoneyTxt;
    [SerializeField] TextMeshProUGUI feedbackTxt;
    [SerializeField] Slider susSlider;

    BetType betType;

    public enum BetType {
        Normal,
        AllOrNothing
    }

    void Start() {
        updateUI();
    }

    void Update() {
        
    }

    public void changeBet(bool low) {
        if (low) {
            if (betAmount > 0) {
                betAmount -= changeBetAmount;
                totalMoney += changeBetAmount;
                suspicion -= 10;
            } else {
                feedback = "Your not even betting...";
            }
        } else {
            if (totalMoney > 0) {
                betAmount += changeBetAmount;
                totalMoney -= changeBetAmount;
                suspicion += 10;
            } else {
                feedback = "Got nothing else to give...";
            }
		}

		updateUI();
	}

    public void confirmBet() {
        if (betAmount > 0) {
            feedback = "Bet placed good luck!!!";

            if (totalMoney == 0) {
                betType = BetType.AllOrNothing;
            } else {
                betType = BetType.Normal;
            } 
        } else {
            feedback = "Need to bet something...";
        }

        updateUI();
    }

    public void updateUI() {
		totalMoneyTxt.text = "$" + totalMoney;
		betMoneyTxt.text = "$" + betAmount;
		feedbackTxt.text = feedback;
		susSlider.value = suspicion / maxSuspicion;
	}
}