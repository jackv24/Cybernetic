using UnityEngine;
using System.Collections;

//A class dedicated to basic UI functions to be controlled through the Unity 4.6 UI system
public class UIFunctions : MonoBehaviour
{
    private int selectedWorld = 0;
    private int selectedLevel = 0;

    public void ExitGame()
    {
        Application.Quit();
    }

    //Load scene with id
    public void LoadScene(int id)
    {
        Application.LoadLevel(id);
    }

    //Load scene with name
    public void LoadScene(string name)
    {
        Application.LoadLevel(name);
    }

    // Reloads the current loaded level
    public void ReloadLevel()
    {
        //Makes sure game is not paused on reload
        if (GameManager.gameManager.isGamePaused)
            GameManager.gameManager.PauseGame(false);

        Application.LoadLevel(Application.loadedLevel);
    }

    // Pauses the game
    public void PauseGame(bool state)
    {
        GameManager.gameManager.PauseGame(state);
    }

    //Sets currently selected world
    public void SetWorld(int number)
    {
        selectedWorld = number;
    }

    //Sets the currently selected level
    public void SetLevel(int number)
    {
        selectedLevel = number;
    }
    
    //Loads the selected level with a template string name
    public void LoadSelectedLevel()
    {
        //The template for scene name
        string levelStringTemplate = "W{0}_Level{1}";

        //Load level template with substituded world and level
        Application.LoadLevel(string.Format(levelStringTemplate, selectedWorld, selectedLevel));
    }

    //Sets game as started in game manager
    public void StartGame()
    {
        GameManager.startGame = true;
    }

    //Disables the gameobject
    public void HideGameObject(GameObject hide)
    {
        hide.SetActive(false);
    }

    //Enables the gameobject
    public void ShowGameObject(GameObject show)
    {
        show.SetActive(true);
    }
}
