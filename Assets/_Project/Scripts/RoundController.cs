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
    private void PlayerEndedWithProfit() { settingsDataBinding.OnWin(3); }

    private void PlayerEndedWithoutProfit() { settingsDataBinding.OnLose(); }

    public void BetData(Bet bet) { betData = bet; }

    public void RaceResult(RaceResults result)
    {
        if (result == RaceResults.Win)
        {
            raceResults = true;
            PlayerWon();

        }
        else if (result == RaceResults.Lose)
        {
            raceResults = false;
            PlayerLost();
        }
        else
        {
            raceResults = false;
        }

        State = GameState.RESULTS;
    }

    private void PlayerWon()
    {
        
    }

    private void PlayerLost()
    {
        
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
