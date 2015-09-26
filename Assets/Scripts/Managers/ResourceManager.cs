using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    //Current amount of resource
    public int resources = 50;
    //Previous amount of resources
    private int lastResourcesValue = 0;

    //The percentage of cost to refund
    public float refundPercent = 50f;

    //The text to display resources
    public Text resourceText;

    void Update()
    {
        //If the resources value has changed
        if (lastResourcesValue != resources && resourceText)
        {
            //Store the change
            lastResourcesValue = resources;

            //Set the text
            resourceText.text = resources.ToString("000");
        }
    }

    //Removes resources
    public void RemoveResources(int value)
    {
        resources -= value;
    }

    //Adds resources
    public void AddResources(int value)
    {
        resources += value;
    }

    //Refunds the cost of a tower
    public void Refund(TowerStats towerStats)
    {
        //Refunds some of the cost of the tower
        AddResources(Mathf.RoundToInt(towerStats.GetComponent<TowerStats>().cost * (refundPercent / 100)));
    }
}
