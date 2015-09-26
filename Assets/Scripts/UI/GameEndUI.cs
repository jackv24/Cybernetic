using UnityEngine;
using System.Collections;

public class GameEndUI : MonoBehaviour
{
    // Static instance for easily finding
    public static GameEndUI instance;

    // Gameobjects for winning and losing game ui
    public GameObject GameWonUI;
    public GameObject GameLostUI;

    void Start()
    {
        instance = this;
    }

    // Called on game win
    public void GameWon()
    {
        // If there is a game won ui object
        if (GameWonUI)
            // Enable it
            GameWonUI.SetActive(true);
    }

    // Called on game lose
    public void GameLost()
    {
        // If there is a game lost ui object
        if (GameLostUI)
            // Enable it
            GameLostUI.SetActive(true);
    }
}
