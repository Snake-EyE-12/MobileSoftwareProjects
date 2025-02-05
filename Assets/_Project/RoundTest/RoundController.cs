using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MAIN,
    SETTINGS,
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
    [SerializeField, Space(5)] SceneAsset Betting;   // Betting Scene
    [SerializeField, Space(5)] SceneAsset Racing;   // Racing Scene

    [Space(10)]
    [Header("Current State"), Space(5)]
    public GameState State;

    GameState prevState;

    private void Awake()
    {
        instance = this;
    }

    public void RaceResults(bool playerWon)
    {

    }

    void Start()
    {
        // Load Main Menu when app starts
        SceneManager.LoadScene(Main.name, LoadSceneMode.Additive);

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
                case GameState.MAIN: SceneManager.UnloadSceneAsync(Main.name); break;
                case GameState.SETTINGS: break;
                case GameState.BETTING: SceneManager.UnloadSceneAsync(Betting.name); break;
                case GameState.RACING: break;
                case GameState.RACE_START: break;
                case GameState.RACE_END: SceneManager.UnloadSceneAsync(Racing.name); break;
                case GameState.RESULTS: break;
                case GameState.WIN: break;
                case GameState.LOSE: break;
                default: break;
            }

            // Loads new scenes
            switch (State)
            {
                case GameState.MAIN: SceneManager.LoadScene(Main.name, LoadSceneMode.Additive); break;
                case GameState.SETTINGS: break;
                case GameState.BETTING: SceneManager.LoadScene(Betting.name, LoadSceneMode.Additive); break;
                case GameState.RACE_START: SceneManager.LoadScene(Racing.name, LoadSceneMode.Additive); break;
                case GameState.RESULTS: break;
                case GameState.WIN: break;
                case GameState.LOSE: break;
                default: break;
            }
        }

        prevState = State;

        switch (State)
        {
            case GameState.MAIN:
                break;

            case GameState.SETTINGS:
                break;

            case GameState.BETTING:
                break;

            case GameState.RACING:
                break;

            case GameState.RACE_START:
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
