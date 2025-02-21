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

    GameState prevState;

    public Bet betData;
    public bool raceResults;
    public int Round = 1;


    [SerializeField] private SettingsDataBinding settingsDataBinding;
    private void PlayerEndedWithProfit(int profit) { settingsDataBinding.OnWin(profit); }

    private void PlayerEndedWithoutProfit() { settingsDataBinding.OnLose(); }

    public void BetData(Bet bet) { betData = bet; }

    [SerializeField] private RaceResultDisplay raceResultDisplay;
    public void DisplayRaceResult(RaceResults result)
    {
        raceResultDisplay.SetState(result);
    }

    public void NextRound()
    {
        Round++;
        if (Round > 10) // Max Rounds
        {
            //if(cashRemaining > statingCash) PlayerEndedWithProfit(cashRemaining - statingCash);
            //else PlayerEndedWithoutProfit();
            State = GameState.RESULTS;
        }
        else State = GameState.BETTING;
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