using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    STARTUP,
    MAIN,
    BETTING,
    RACE_START,
    RACING,
    RACE_END,
    RESULTS,  // Might not use this one
    WIN,
    LOSE
}

public class RoundController : MonoBehaviour
{
    public static RoundController instance;

    [Header("Scenes")]
    [SerializeField, Space(5)] SceneAsset Main;       // Main Menu Scene
    [SerializeField, Space(5)] SceneAsset Betting;    // Betting Scene
    [SerializeField, Space(5)] SceneAsset Racing;     // Racing Scene
    [SerializeField, Space(5)] SceneAsset Win;        // Win Scene
    [SerializeField, Space(5)] SceneAsset Lose;       // Lose Scene

    [Space(10)]
    [Header("Current State"), Space(5)]
    public GameState State;

    GameState prevState;

	private void Awake()
    {
        instance = this;
    }

    public void BetData(Bet bet)
    {

    }

    public void SetRaceResult(RaceResults results)
    {
        if (results == global::RaceResults.Win)
        {

        }
    }

    void Start()
    {
        // Load Main Menu when app starts
        State = GameState.MAIN;

        // Load important/persistent data here
    }

    void Update()
    {
        // Handle State changing
        if (prevState != State) 
        {
            // Unloads old scenes
            switch (prevState)
            {
                case GameState.MAIN: try { SceneManager.UnloadSceneAsync(Main.name); } catch { } break;
                case GameState.BETTING: try { SceneManager.UnloadSceneAsync(Betting.name); } catch { } break;
                case GameState.RACING: try { SceneManager.UnloadSceneAsync(Racing.name); } catch { } break; 
                case GameState.WIN: try { SceneManager.UnloadSceneAsync(Win.name); } catch { } break;
                case GameState.LOSE: try { SceneManager.UnloadSceneAsync(Lose.name); } catch { } break;
                default: break;  // Loading screen?
            }

            // Loads new scenes
            switch (State)
            {
                case GameState.MAIN: SceneManager.LoadScene(Main.name, LoadSceneMode.Additive); break;
                case GameState.BETTING: SceneManager.LoadScene(Betting.name, LoadSceneMode.Additive); break;
                case GameState.RACING: SceneManager.LoadScene(Racing.name, LoadSceneMode.Additive); break;
                case GameState.WIN: SceneManager.LoadScene(Win.name, LoadSceneMode.Additive); break;
                case GameState.LOSE: SceneManager.LoadScene(Lose.name, LoadSceneMode.Additive); break;
                default: break;  // Loading screen?
            }

            prevState = State;
        }

        // Main Game State Machine
        switch (State)
        {
            case GameState.MAIN:
                break;

            case GameState.BETTING:
                break;

            case GameState.RACING:
                break;

            case GameState.RACE_START:
                State = GameState.RACING;
                break;

            case GameState.RACE_END:
                break;

            case GameState.RESULTS:
                break;

            case GameState.WIN:
                break;

            case GameState.LOSE:
                break;

            default:
                break;
        }
    }
}
