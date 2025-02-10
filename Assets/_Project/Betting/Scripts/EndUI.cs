using TMPro;
using UnityEngine;

public class EndUI : MonoBehaviour {
    [SerializeField] GameObject winP;
    [SerializeField] GameObject loseP;
    [SerializeField] TextMeshProUGUI moneyTxt;
    [SerializeField] int money;

    void Start() {
        
    }

    void Update() {
        
    }

    public void resultsScreen(bool win) {
        if (win) {
            winP.SetActive(true);
        } else {
            loseP.SetActive(true);
        }
    }

    public void againClick() {
    
    }

    public void mainClick() {
    
    }
}