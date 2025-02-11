using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RacerUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI timerTxt;
    [SerializeField] TextMeshProUGUI betTxt;
    [SerializeField] Image betImg;
    [SerializeField] Image[] warningImages;
    [SerializeField] Sprite[] possibleWarningImages;
    [SerializeField] Sprite[] possibleBetImages;
    [SerializeField] Slider susBar;
    [SerializeField] float suspicion;
    [SerializeField] float maxSuspicion;
    [SerializeField] int warnings = 0;
    [SerializeField] BetType betType;
    [SerializeField] int betAmount;
    bool countTime = false;
    float time = 0;

    void Start() {
        updateUI();
    }

    void Update() {
        if (countTime) {
            time += Time.deltaTime;
            timerTxt.text = "Time: " + time;
        }
    }

    public void toggleClick(GameObject bar) {
        
    }

    public void updateUI() {
        timerTxt.text = "Time: " + time;
        betTxt.text = "Bet: $" + betAmount;
        susBar.value = suspicion / maxSuspicion;

        for (int i = 0; i <= warningImages.Length; i++) {
            if (i == 0) continue;

            if (i <= warnings) {
                warningImages[i - 1].sprite = possibleWarningImages[1];
            } else {
				warningImages[i - 1].sprite = possibleWarningImages[0];
			}
        }

        switch (betType) {
            case BetType.Normal:
                betImg.sprite = possibleBetImages[0];
                break;
            case BetType.AllOrNothing:
                betImg.sprite = possibleBetImages[1];
                break;
        }
    }
}