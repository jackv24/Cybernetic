using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
    //Is the node available to place a tower on?
    public bool isAvailable = false;
    //Stores previous availability
    private bool currentIsAvailable;

    public GameObject occupyingTower;

    //Color to display when available
    public Color availableColor = Color.green;
    //Color to display when taken
    public Color takenColor = Color.red;

    //The mesh renderer which the materials color will be changed on
    private MeshRenderer rend;

    //Determine whther this node is a build node
    public bool canPlace = false;

    void Start()
    {
        //If this is a placement node, make it available
        isAvailable = canPlace;

        //Set the last available state to the current state
        currentIsAvailable = isAvailable;

        //Get a reference to the mesh renderer
        rend = GetComponent<MeshRenderer>();

        //Set the initial color state
        SetColorState(isAvailable);
    }

    void Update()
    {
        //If the availability state has changed
        if(currentIsAvailable != isAvailable)
        {
            //Store the new state
            currentIsAvailable = isAvailable;
            //Set the appropriate color
            SetColorState(isAvailable);
        }
    }

    //Sets the color state
    void SetColorState(bool state)
    {
        //If the node is available
        if (state)
            rend.material.color = availableColor;
        else
            rend.material.color = takenColor;
    }

    //Sets node selected state (mouse over)
    public void SelectNode(bool state)
    {
        //Set mesh renderer on/off
        rend.enabled = state;
    }

    //Clears the node (called externally)
    public void Clear()
    {
        //If this is a placeable node
        if (canPlace)
        {
            //Delete all children of the node (tower)
            foreach (Transform tower in transform)
            {
                GameObject.Destroy(tower.gameObject);
            }

            //Set the occupying tower to null
            occupyingTower = null;

            //Make this tower available
            isAvailable = true;
        }
    }
}
