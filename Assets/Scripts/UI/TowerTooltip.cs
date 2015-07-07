using UnityEngine;
using System.Collections;

public class TowerTooltip : MonoBehaviour
{
    //Stores the selected node
    public Node selectedNode;

    [HideInInspector] //Stores a reference to the selectTowers script (which is set by that script, hence the public variable)
    public SelectTowers selectTowers;

    //Stores a reference to the resource manager
    private ResourceManager resourceManager;

    void Start()
    {
        //gets a reference to the resourcemanager script attached to the game controller
        resourceManager = GameObject.FindWithTag("GameController").GetComponent<ResourceManager>();
    }

    //Clears the selected node
    public void ClearNode()
    {
        //Refunds the tower
        resourceManager.Refund(selectTowers.selectedTower.GetComponent<TowerStats>());
        //Clears the node
        selectedNode.Clear();
        //Removes tooltip
        selectTowers.RemoveTooltip();
    }
}
