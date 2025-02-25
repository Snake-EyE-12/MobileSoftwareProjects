using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RacerUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI timerTxt;
    [SerializeField] TextMeshProUGUI betTxt;
    [SerializeField] Image betImg;
    [SerializeField] Sprite[] possibleBetImages;
    [SerializeField] Slider susBar;
    [SerializeField] float suspicion;
    [SerializeField] float maxSuspicion;
    [SerializeField] BetType betType;
    [SerializeField] int betAmount;
    float time = 0;
    public bool raceTime = false;

	private void OnEnable() {
        RaceController.OnRaceStart += racing;
        RaceController.OnRacePause += racing;
	}

	private void OnDisable() {
	    RaceController.OnRaceStart -= racing;
        RaceController.OnRacePause -= racing;
	}

	void Start() {
        setValues(
            RoundController.instance.globalSuspicion,
            RoundController.instance.maxSuspicion,
            RoundController.instance.betAmount,
            RoundController.instance.betType
            );
    }

    private void setValues(float sus, float maxsus, int bet, BetType btype)
    {
        suspicion = sus;
        maxSuspicion = maxsus;
        betAmount = bet;
        betType = btype;

        updateUI();
    }

    void Update() {
        if (raceTime) {
            time += Time.deltaTime;
            timerTxt.text = "Time: " + time;
        }
    }

    void racing() {
        raceTime = !raceTime;
    }

    public void toggleClick(GameObject bar) {
        bar.SetActive(!bar.activeSelf);
    }

    public void updateUI() {
        timerTxt.text = "Time: " + time;
        betTxt.text = "Bet: $" + betAmount;
        susBar.value = suspicion / maxSuspicion;

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