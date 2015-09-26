using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    //Static instance of this script
    public static GameManager gameManager;

    //Static instances of managers attached to this gameObject
    public static ResourceManager resourceManager;
    public static TowerDatabase towerDatabase;
    public static EnemyManager enemyManager;

    //Static instance of the waypoint manager (attached to the level)
    public static WaypointManager waypointManager;

    //Tower selection static references
    public static SelectTowers selectTowers;
    public static TowerTooltip towerTooltip;

    //Stores reference to level information script
    public static LevelInfo levelInfo;

    //Has the level been loaded?
    public static bool levelLoaded = false;
    //Should the game start?
    public static bool startGame = false;

    public static RoundManager roundManager;
    public static float enemySpeed = 1f;

    public static BaseHealth baseHealth;
    public static Spawner spawner;

    public static PauseGame pauseGame;
    public bool isGamePaused = false;

    void Start()
    {
        //Set the instance for this game manager
        gameManager = this;

        //Set other static instances
        resourceManager = GetComponent<ResourceManager>();
        towerDatabase = GetComponent<TowerDatabase>();
        enemyManager = GetComponent<EnemyManager>();
        roundManager = GetComponent<RoundManager>();

        startGame = false;
    }

    void Update()
    {
        //if the pause game ui object exists & the escape key is pressed
        if (pauseGame && Input.GetButtonDown("Cancel") && startGame)
        {
            // Toggle the paused game state
            PauseGame(!pauseGame.gameObject.activeSelf);
        }
    }

    // Called by round manager
    public void EndGame(bool gameWon)
    {
        //Get an instance of the game end UI (for access to non-static objects)
        GameEndUI gameEndUI = GameEndUI.instance;

        if (gameWon)
        {
            gameEndUI.GameWon();
        }
        else
        {
            gameEndUI.GameLost();
        }
    }

    //Pauses the game
    public void PauseGame(bool state)
    {
        isGamePaused = state;

        // Sets the active state of the ui object
        pauseGame.gameObject.SetActive(state);

        //Toggle the time scale
        if (state)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}