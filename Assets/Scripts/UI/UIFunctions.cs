using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//A class dedicated to basic UI functions to be controlled through the Unity 4.6 UI system
public class UIFunctions : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }

    //Load scene with id
    public void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    //Load scene with name
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    // Reloads the current loaded level
    public void ReloadLevel()
    {
        //Makes sure game is not paused on reload
        if (GameManager.gameManager.isGamePaused)
            GameManager.gameManager.PauseGame(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Pauses the game
    public void PauseGame(bool state)
    {
        GameManager.gameManager.PauseGame(state);
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
