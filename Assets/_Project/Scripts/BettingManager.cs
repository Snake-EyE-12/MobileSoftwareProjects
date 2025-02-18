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
    [SerializeField] TextMeshProUGUI roundTxt;
    [SerializeField] TextMeshProUGUI feedbackTxt;
    [SerializeField] Slider susSlider;
    [SerializeField] BetType betType;
    [SerializeField] int curRound;
    [SerializeField] int maxRound;

    void Start() {
        updateUI();
    }

    public void setValues(int total, int bet, int changeBet, float sus, float maxSus, int round, int maxRounds) {
        totalMoney = total;
        betAmount = bet;
        changeBetAmount = changeBet;
        suspicion = sus;
        maxSuspicion = maxSus;
        curRound = round;
        maxRound = maxRounds;
        
        updateUI();
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

            RoundController.instance.BetData(new Bet(betAmount, suspicion, betType));
            RoundController.instance.State = GameState.RACE_START;
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
        roundTxt.text = $"Round: {curRound}/{maxRound}";
	}
}
public enum BetType {
    Normal,
    AllOrNothing
}

public class Bet {
    public int betAmount;
    public float suspicion;
    public BetType betType;

    public Bet(int bet, float sus, BetType type) {
        betAmount = bet;
        suspicion = sus;
        betType = type;
    }

    public void win(bool win) {
        switch (betType) {
            case BetType.Normal:
                betAmount = win ? betAmount += betAmount : betAmount -= betAmount;
                break;
            case BetType.AllOrNothing:
                betAmount = win ? betAmount *= 4 : betAmount -= betAmount;
                break;
        }
    }
}