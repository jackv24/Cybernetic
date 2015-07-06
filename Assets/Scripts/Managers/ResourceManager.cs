using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public int resources = 50;
    private int lastResourcesValue = 0;

    public Text resourceText;

    void Update()
    {
        if (lastResourcesValue != resources)
        {
            lastResourcesValue = resources;

            resourceText.text = resources.ToString("000");
        }
    }

    public void UseResources(int value)
    {
        resources -= value;
    }
}
