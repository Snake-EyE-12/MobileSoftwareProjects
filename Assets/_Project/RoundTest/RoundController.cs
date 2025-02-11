using System.Collections;
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

    public void BetData(Bet bet)
    {
        betData = bet;
    }

    public void SetRaceResult(RaceResults results)
    {
        raceResults = (results == RaceResults.Win);
        State = GameState.RESULTS;
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
                case GameState.RESULTS: try { SceneManager.UnloadSceneAsync("End"); } catch { } break;
                default: break;  // Loading screen?
            }

            // Loads new scenes
            switch (State)
            {
                case GameState.MAIN: SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive); break;
                case GameState.BETTING: SceneManager.LoadScene("Betting", LoadSceneMode.Additive); break;
                case GameState.RACE_START: SceneManager.LoadScene("Race", LoadSceneMode.Additive); break;
                case GameState.RESULTS: SceneManager.LoadScene("End", LoadSceneMode.Additive); break;
                default: break;  // Loading screen?
            }

            prevState = State;
        }


        // Main Game State Machine
        switch (State)
        {
            case GameState.RACE_START:
                if (SceneManager.GetSceneByName("Race").isLoaded)
                {
                    // Use bet data


                    // Start Race
                    State = GameState.RACING;
                }
                break;


            case GameState.RACING:
                break;


            case GameState.RESULTS:
                if (SceneManager.GetSceneByName("End").isLoaded)
                {

                }
                break;


            default:
                break;
        }
    }
}
