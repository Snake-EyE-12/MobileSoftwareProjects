using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyExchangeManager : MonoBehaviour {
    [SerializeField] int yourMoney;
    [SerializeField] int betMoney;
    [SerializeField] MoneyBtn[] yourMoneyBtns;
    [SerializeField] MoneyBtn[] betMoneyBtns;
    [SerializeField] TextMeshProUGUI yourTxt;
    [SerializeField] TextMeshProUGUI betTxt;
    public GameObject yourArea;
    public GameObject betArea;

    void Start() {
        yourMoney = 100;
        betMoney = 100;
		yourTxt.text = "Your Money: " + yourMoney;
		betTxt.text = "Bet Money: " + betMoney;
		updateButtons(10);
	}

    void updateButtons(int change) {
		foreach (MoneyBtn m in yourMoneyBtns) {
			m.setValues(0, change, yourMoney, betMoney);
		}

		foreach (MoneyBtn m in betMoneyBtns) {
            m.setValues(change, 0, yourMoney, betMoney);
		}
	}

    public void updateMoney(int your, int bet) {
        yourMoney = your;
        betMoney = bet;
        yourTxt.text = "Your Money: " + yourMoney;
        betTxt.text = "Bet Money: " + betMoney;
    }

    public void toggleClick(GameObject g) {
        g.SetActive(!g.activeSelf);
        GetComponent<RectTransform>().anchoredPosition = (g.activeSelf) ? new Vector3(0, 240, 0) : new Vector3(0, -340, 0);
        //GetComponent<RectTransform>().localPosition = (g.activeSelf) ? new Vector3(0, 240, 0) : new Vector3(0, -340, 0);
	}
}