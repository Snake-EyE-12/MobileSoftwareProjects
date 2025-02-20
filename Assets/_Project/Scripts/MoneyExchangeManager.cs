using UnityEngine;
using UnityEngine.UI;

public class MoneyExchangeManager : MonoBehaviour {
    [SerializeField] int yourMoney;
    [SerializeField] int theirMoney;
    [SerializeField] int betMoney;
    [SerializeField] MoneyBtn[] yourMoneyBtns;
    [SerializeField] MoneyBtn[] theirMoneyBtns;
    public GameObject yourArea;
    public GameObject theirArea;

    void Start() {
        updateButtons(10);
	}

    void updateButtons(int change) {
		foreach (MoneyBtn m in yourMoneyBtns) {
			m.setValues(0, change, yourMoney, betMoney);
		}

		foreach (MoneyBtn m in theirMoneyBtns) {
            m.setValues(change, 0, theirMoney, betMoney);
		}
	}

    void Update() {
        
    }
}