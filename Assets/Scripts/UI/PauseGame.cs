using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour
{
    void Start()
    {
        GameManager.pauseGame = this;

        gameObject.SetActive(false);
    }
}
