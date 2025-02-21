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
    public float globalSuspicion = 0f;
    public float maxSuspicion = 200f;

    [Space(10)]
    [Header("Betting"), Space(5)]
    public int money = 200;
    public int startingMoney = 200;
    public int changeBetBy = 10;
    public int betAmount = 0;
    public BetType betType;

    [Space(10)]
    [Header("Results"), Space(5)]
    [SerializeField] private RaceResultDisplay raceResultDisplay;
    public bool raceResults;

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
        raceResultDisplay.SetState(result);

        // if (result == RaceResults.CaughtCheating) { }
    }

    public void NextRound()
    {
        round++;

        if (round > maxRounds) // Max Rounds
        {
            //if(cashRemaining > statingCash) PlayerEndedWithProfit(cashRemaining - statingCash);
            //else PlayerEndedWithoutProfit();

            round = 1;
            State = GameState.RESULTS;
        }
        else
        {
            betAmount = 0;
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


    private void Awake() { instance = this; }

    // Load Main Menu when app starts
    void Start() { if (State == GameState.STARTUP) State = GameState.MAIN; }

    void Update() { UpdateScene(); }
}