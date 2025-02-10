using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RacerUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI timerTxt;
    [SerializeField] TextMeshProUGUI betTxt;
    [SerializeField] Image betImg;
    [SerializeField] Image[] warningImages;

    void Start() {
        
    }

    void Update() {
        
    }

    public void toggleClick(GameObject bar) {
        bar.SetActive(!bar.activeSelf);    
    }
}
