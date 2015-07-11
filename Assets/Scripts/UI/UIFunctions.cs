using UnityEngine;
using System.Collections;

//A class dedicated to basic UI functions to be controlled through the Unity 4.6 UI system
public class UIFunctions : MonoBehaviour
{
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

    public void SetWorld(int number)
    {
        PlayerPrefs.SetInt("world", number);
    }

    public void SetLevel(int number)
    {
        PlayerPrefs.SetInt("level", number);
    }
}
