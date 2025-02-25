using UnityEngine;
using UnityEngine.EventSystems;

public class MoneyBtn : GameplayButton {
    [SerializeField] float detectionRadius;
	[SerializeField] MoneyExchangeManager moneyManager;
    int moneyGain;
    int moneyLose;
    int yourMoney;
	int betAmount;
	Vector3 mousePos;
	
	void Start() {
		canDrag = true;
    }

	void Update() {
		mousePos = Input.mousePosition;
	}

	public void setValues(int gain, int lose, int your, int bet) {
		moneyGain = gain;
		moneyLose = lose;
		yourMoney = your;
		betAmount = bet;
	}

	public void giveOtake(bool give) {
		if (give) {
			if ((yourMoney - moneyLose) > 0) {
				yourMoney -= moneyLose;
				betAmount += moneyLose;
				moneyManager.updateMoney(yourMoney, betAmount);
			}
		} else {
			if ((betAmount - moneyGain) > 0) {
				betAmount -= moneyGain;
				yourMoney += moneyGain;
				moneyManager.updateMoney(yourMoney, betAmount);
			}
		}
	}

	protected override bool Condition(PointerEventData eventData) {
		if (mousePos.y <= 465 && ((mousePos.x >= 285 && mousePos.x <= 440) || (mousePos.x >= 1020 && mousePos.x <= 1175))) {
			return true;
		}

		//285, 440 x || 0, 465 y your area
		//1020, 1175 x || 0, 465 y bet area

		return false;
	}

	protected override void Drop(PointerEventData eventData) {
		if (mousePos.y <= 465 && (mousePos.x >= 285 && mousePos.x <= 440)) {
			giveOtake(false);

			return;
		}

		if (mousePos.y <= 465 && (mousePos.x >= 1020 && mousePos.x <= 1175)) {
			giveOtake(true);

			return;
		}
	}

	public override void Press() {}
}