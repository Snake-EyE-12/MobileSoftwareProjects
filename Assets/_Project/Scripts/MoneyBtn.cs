using UnityEngine;
using UnityEngine.EventSystems;

public class MoneyBtn : GameplayButton {
    [SerializeField] float detectionRadius;
	[SerializeField] MoneyExchangeManager moneyManager;
    int moneyGain;
    int moneyLose;
    int totalMoney;
	int betAmount;
	
	void Start() {
		canDrag = true;
    }

	public void setValues(int gain, int lose, int total, int bet) {
		moneyGain = gain;
		moneyLose = lose;
		totalMoney = total;
		betAmount = bet;
	}

	protected override bool Condition(PointerEventData eventData) {
		throw new System.NotImplementedException();
	}

	protected override void Drop(PointerEventData eventData) {
		throw new System.NotImplementedException();
	}

	public override void Press() {}
}