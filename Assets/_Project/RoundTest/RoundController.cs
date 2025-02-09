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

    [Space(10)]
    [Header("Current State"), Space(5)]
    public GameState State;

    GameState prevState;


    public void BetData(Bet bet)
    {

    }

    public void SetRaceResult(RaceResults results)
    {
        if (results == global::RaceResults.Win)
        {

        }
    }

    private void Awake() { instance = this; }

    void Start()
    {
        // Load Main Menu when app starts
        if (State == GameState.STARTUP) State = GameState.MAIN;

        // Load any important/persistent data here
    }

    void Update()
    {
        // Handle State changing
        if (prevState != State) 
        {
            // Unloads old scenes
            switch (prevState)
            {
                case GameState.MAIN: try { SceneManager.UnloadSceneAsync("MainMenu"); } catch { } break;
                case GameState.BETTING: try { SceneManager.UnloadSceneAsync("Betting"); } catch { } break;
                case GameState.RACING: try { SceneManager.UnloadSceneAsync("Race"); } catch { } break; 
                case GameState.WIN: try { SceneManager.UnloadSceneAsync("Win"); } catch { } break;
                case GameState.LOSE: try { SceneManager.UnloadSceneAsync("Lose"); } catch { } break;
                default: break;  // Loading screen?
            }

            // Loads new scenes
            switch (State)
            {
                case GameState.MAIN: SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive); break;
                case GameState.BETTING: SceneManager.LoadScene("Betting", LoadSceneMode.Additive); break;
                case GameState.RACING: SceneManager.LoadScene("Race", LoadSceneMode.Additive); break;
                case GameState.WIN: SceneManager.LoadScene("Win", LoadSceneMode.Additive); break;
                case GameState.LOSE: SceneManager.LoadScene("Lose", LoadSceneMode.Additive); break;
                default: break;  // Loading screen?
            }

            prevState = State;
        }


        // Main Game State Machine
        switch (State)
        {
            case GameState.RACE_START:
                // Use bet data


                // Start Race
                State = GameState.RACING;
                break;


            case GameState.RACING:
                break;


            case GameState.RACE_END:
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
