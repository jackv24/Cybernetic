using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceText : MonoBehaviour
{
    void Start()
    {
        //Register self with game manager
        GameManager.resourceManager.resourceText = GetComponent<Text>();
    }
}
