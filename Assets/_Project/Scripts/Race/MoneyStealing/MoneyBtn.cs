using UnityEngine;
using UnityEngine.EventSystems;

public class MoneyBtn : GameplayButton {
    [SerializeField] float detectionRadius;
	[SerializeField] MoneyExchangeManager moneyManager;
    int moneyGain;
    int moneyLose;
    int yourMoney;
	int betAmount;
	Vector3 initialpos;
	
	void Start() {
		canDrag = true;
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
		//if (EventSystem.current.currentSelectedGameObject == null) {
		//	return false;
		//}

		float betDistance = Vector2.Distance(moneyManager.betArea.transform.position, itemRectTransform.position);
		float yourDistance = Vector2.Distance(moneyManager.yourArea.transform.position, itemRectTransform.position);

		if (betDistance <= detectionRadius || yourDistance <= detectionRadius) {
			return true;
		}

		//if (EventSystem.current.currentSelectedGameObject.name == "YourMoneyArea" ||
		//	EventSystem.current.currentSelectedGameObject.name == "BetMoneyArea") {
		//	return true;
		//}

		//if ((Vector2.Distance(moneyManager.yourArea.transform.position, Camera.main.ScreenToWorldPoint(itemRectTransform.position)) <= detectionRadius) || 
		//	Vector2.Distance(moneyManager.betArea.transform.position, Camera.main.ScreenToWorldPoint(itemRectTransform.position)) <= detectionRadius) {
		//	return true;
		//}

		return false;
	}

	protected override void Drop(PointerEventData eventData) {
		if (Vector2.Distance(moneyManager.yourArea.transform.position, Camera.main.ScreenToWorldPoint(itemRectTransform.position)) <= detectionRadius) {
			giveOtake(false);

			return;
		}

		if (Vector2.Distance(moneyManager.betArea.transform.position, Camera.main.ScreenToWorldPoint(itemRectTransform.position)) <= detectionRadius) {
			giveOtake(true);

			return;
		}
	}

	public override void Press() {}
}