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

	public void giveOtake(bool give) {
		if (give) {

		} else {
		
		}
	}

	protected override bool Condition(PointerEventData eventData) {
		throw new System.NotImplementedException();
	}

	protected override void Drop(PointerEventData eventData) {
		if (Vector2.Distance(moneyManager.yourArea.transform.position, Camera.main.ScreenToWorldPoint(itemRectTransform.position)) <= detectionRadius) {
			giveOtake(true);

			return;
		}

		if (Vector2.Distance(moneyManager.theirArea.transform.position, Camera.main.ScreenToWorldPoint(itemRectTransform.position)) <= detectionRadius) {
			giveOtake(false);

			return;
		}
	}

	public override void Press() {}
}