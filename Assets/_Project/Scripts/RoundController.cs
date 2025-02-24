using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    STARTUP,
    MAIN,
    BETTING,
    RACING,
    RESULTS
}

public class RoundController : MonoBehaviour
{
    public static RoundController instance;

    [Space(10)]
    [Header("Current State"), Space(5)]
    public GameState State;

    [Space(10)]
    [Header("Rounds"), Space(5)]
    public int round = 1;
    [Range(1, 20)] public int maxRounds = 10;

    [Space(10)]
    [Header("Suspicion"), Space(5)]
    public int globalSuspicion = 0;
    public int maxSuspicion = 15;
    public int raceSuspicion = 0;

    [Space(10)]
    [Header("Betting"), Space(5)]
    public int money = 200;
    public int startingMoney = 200;
    [Range(1, 100)] public float changeBetByPercentage = 10;
    public int betAmount = 0;
    public BetType betType;

    [Space(10)]
    [Header("Results"), Space(5)]
    [SerializeField] private RaceResultDisplay raceResultDisplay;
    public bool gameWon = true;

    [Space(10)]
    [Header("Settings"), Space(5)]
    [SerializeField] private SettingsDataBinding settingsDataBinding;

    GameState prevState;


    private void PlayerEndedWithProfit(int profit) { settingsDataBinding.OnWin(profit); }

    private void PlayerEndedWithoutProfit() { settingsDataBinding.OnLose(); }

    public void BetData(Bet bet)
    {
        betAmount = bet.betAmount;
        globalSuspicion += bet.suspicion;
        betType = bet.betType;
    }

    public void DisplayRaceResult(RaceResults result)
    {
        switch (result)
        {
            case RaceResults.CaughtCheating:
                print("Caught Cheating");
                globalSuspicion += 2;
                if (globalSuspicion > maxSuspicion) globalSuspicion = maxSuspicion;
                money -= betAmount;
                break;

            case RaceResults.Win:
                print("Won");
                globalSuspicion += 1;
                money += betAmount;
                break;

            case RaceResults.Lose:
                print("Lost");
                globalSuspicion -= 2;
                if (globalSuspicion < 0) globalSuspicion = 0;
                money -= betAmount;
                break;

            default: print("Something went wrong"); break;
        }

        betAmount = 0;

        if (money <= 0)  // Game Over
        {
            round = 10;
            gameWon = false;
        }

        raceResultDisplay.SetState(result);
    }

    public void NextRound()
    {
        round++;

        if (round > maxRounds) // Max Rounds
        {
            if (money > startingMoney) PlayerEndedWithProfit(money - startingMoney);
            else { PlayerEndedWithoutProfit(); gameWon = false; }  // You Lose :(

            round = 1;
            State = GameState.RESULTS;
        }
        else
        {
            State = GameState.BETTING;
        }
    }

    private void UpdateScene()
    {
        // Handle State changing
        if (prevState != State)
        {
            // Unloads old scenes
            switch (prevState)
            {
                case GameState.MAIN: try { SceneManager.UnloadSceneAsync("MainMenu"); } catch { } break;
                case GameState.BETTING: try { SceneManager.UnloadSceneAsync("Betting"); } catch { } break;
                case GameState.RACING: try { SceneManager.UnloadSceneAsync("LongRace"); } catch { } break;
                case GameState.RESULTS: try { SceneManager.UnloadSceneAsync("End"); } catch { } break;
                default: break;  // Loading screen?
            }

            // Loads new scenes
            switch (State)
            {
                case GameState.MAIN: SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive); break;
                case GameState.BETTING: SceneManager.LoadScene("Betting", LoadSceneMode.Additive); break;
                case GameState.RACING: SceneManager.LoadScene("LongRace", LoadSceneMode.Additive); break;
                case GameState.RESULTS: SceneManager.LoadScene("End", LoadSceneMode.Additive); break;
                default: break;  // Loading screen?
            }

            SoundManager.instance.Stop();
            prevState = State;
        }
    }

    public void ExitGame()
    {
        switch (State)
        {
            case GameState.MAIN:
#if UNITY_EDITOR 
                UnityEditor.EditorApplication.ExitPlaymode();
#else
                Application.Quit();
#endif
                break;

            default: State = GameState.MAIN; break;
        }
    }

    public void ResetGame()
    {
        globalSuspicion = 0;
        raceSuspicion = 0;
        money = startingMoney;
        betAmount = 0;

        gameWon = true;
    }


    private void Awake() { instance = this; money = startingMoney; gameWon = true; }

    // Load Main Menu when app starts
    void Start() { if (State == GameState.STARTUP) State = GameState.MAIN; }

    void Update() { UpdateScene(); }
}