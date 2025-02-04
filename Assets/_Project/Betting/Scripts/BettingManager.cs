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

    void Start() {
        updateUI();
    }

    void Update() {
        
    }

    public void changeBet(bool low) {
        if (low) {
            if (totalMoney < 100) {
				betAmount -= changeBetAmount;
				totalMoney += changeBetAmount;

                if (suspicion > 0) suspicion -= 10;
			}

        } else {
            if (totalMoney > 0) {
				betAmount += changeBetAmount;
				totalMoney -= changeBetAmount;

                if (suspicion < maxSuspicion) suspicion += 10;
			}
		}

		updateUI();
	}

    public void confirmBet() {
    
    }

    public void updateUI() {
		totalMoneyTxt.text = "$" + totalMoney;
		betMoneyTxt.text = "$" + betAmount;
		feedbackTxt.text = feedback;
		susSlider.value = suspicion / maxSuspicion;
	}
}
