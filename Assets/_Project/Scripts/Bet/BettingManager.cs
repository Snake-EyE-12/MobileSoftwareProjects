using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BettingManager : MonoBehaviour {
    [SerializeField] int totalMoney;
    [SerializeField] int betAmount;
    [SerializeField] int changeBetAmount;
    [SerializeField] string feedback;
    [SerializeField] int suspicion = 0;
    [SerializeField] int globalSuspicion;
    [SerializeField] int maxSuspicion;
    [SerializeField] TextMeshProUGUI totalMoneyTxt;
    [SerializeField] TextMeshProUGUI betMoneyTxt;
    [SerializeField] TextMeshProUGUI roundTxt;
    [SerializeField] TextMeshProUGUI feedbackTxt;
    [SerializeField] Slider susSlider;
    [SerializeField] BetType betType;
    [SerializeField] int curRound;
    [SerializeField] int maxRound;

    void Start() {
        setValues(
            RoundController.instance.money,
            RoundController.instance.betAmount,
            RoundController.instance.changeBetByPercentage,
            RoundController.instance.globalSuspicion,
            RoundController.instance.maxSuspicion,
            RoundController.instance.round,
            RoundController.instance.maxRounds
            );
    }

    public void setValues(int total, int bet, float changeBet, int gsus, int maxSus, int round, int maxRounds) {
        totalMoney = total;
        betAmount = bet;
        changeBetAmount = (int)(totalMoney / changeBet);
        globalSuspicion = gsus;
        maxSuspicion = maxSus;
        curRound = round;
        maxRound = maxRounds;

        if (changeBetAmount == 0) changeBetAmount = 1;
        
        updateUI();
    }

    public void changeBet(bool low) {
        if (low) {
            if (betAmount > 0) betAmount -= changeBetAmount;
            else feedback = "You're not even betting...";
        } else {
            if (totalMoney - betAmount > 0) betAmount += changeBetAmount;
            else
            {
                betAmount = totalMoney;
                feedback = "Got nothing else to give...";
            }
		}

        if (betAmount != 0)
        {
            float susTier = (float)betAmount / totalMoney;

            if (susTier == 1) suspicion = 5;
            else if (susTier > 0.75) suspicion = 3;
            else if (susTier > 0.50) suspicion = 2;
            else if (susTier > 0.25) suspicion = 1;
            else suspicion = 0;
        }

        updateUI();
	}

    public void confirmBet() {
        if (betAmount > 0) {
            feedback = "Bet placed good luck!!!";

            if (suspicion + globalSuspicion > maxSuspicion) suspicion = maxSuspicion - globalSuspicion;

            if (totalMoney == betAmount) {
                betType = BetType.AllOrNothing;
            } else {
                betType = BetType.Normal;
            }

            RoundController.instance.BetData(new Bet(betAmount, suspicion, betType));
            RoundController.instance.State = GameState.RACING;
        } else {
            feedback = "Need to bet something...";
        }

        updateUI();
    }

    public void updateUI() {
		totalMoneyTxt.text = "$" + (totalMoney - betAmount);
		betMoneyTxt.text = "$" + betAmount;
		feedbackTxt.text = feedback;

		susSlider.value = (float)(suspicion + globalSuspicion) / maxSuspicion;
        roundTxt.text = $"Round: {curRound}/{maxRound}";
	}
}
public enum BetType {
    Normal,
    AllOrNothing
}

public class Bet {
    public int betAmount;
    public int suspicion;
    public BetType betType;

    public Bet(int bet, int sus, BetType type) {
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