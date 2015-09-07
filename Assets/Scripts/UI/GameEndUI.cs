using UnityEngine;
using System.Collections;

public class GameEndUI : MonoBehaviour
{
    public static GameObject instance;

    void Start()
    {
        instance = gameObject;

        gameObject.SetActive(false);
    }
}
