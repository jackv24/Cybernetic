using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlaceTowers))]
public class SelectTowers : MonoBehaviour
{
    //Holds a reference to the script on this tooltip
    public TowerTooltip towerTooltip;

    //The height to display the tooltip above a node
    public float toolTipHeight = 1f;

    [HideInInspector] //The currently selected tower
    public GameObject selectedTower;

    private Node lastSelectedNode;
    private Node selectedNode;

    //Reference to the placetowers script
    private PlaceTowers placeTowers;
    //Reference to the main camera
    private Camera cam;

    void Start()
    {
        GameManager.selectTowers = this;
        towerTooltip = GameManager.towerTooltip;

        //Gets a reference to the placetowers script on this gameobject
        placeTowers = GetComponent<PlaceTowers>();
        //References the main camera
        cam = Camera.main;
    }

    void Update()
    {
        //Create a ray from the cursors position on the screen
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //If the click button is pressed while the mouse cursor is not over a UI element
        if (Input.GetButtonDown("Click") && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            //Cast a ray out from the cursor
            if (Physics.Raycast(ray, out hitInfo, placeTowers.maxDistance, placeTowers.layer))
            {
                //Store the node hit
                selectedNode = hitInfo.collider.GetComponent<Node>();

                //If there is a tower occupying this node
                if (selectedNode.occupyingTower && towerTooltip)
                {
                    //Set the selected tower to the node's occupying tower
                    selectedTower = selectedNode.occupyingTower;

                    //Enables the tooltip
                    towerTooltip.gameObject.SetActive(true);

                    //Set tooltip references
                    towerTooltip.selectedNode = selectedNode;

                    //If a node was previously selected
                    if (lastSelectedNode)
                        //Deselect it
                        lastSelectedNode.SelectNode(false, false);

                    //Select the new node
                    selectedNode.SelectNode(true, true);

                    //Set currently selected node as the last selected node
                    lastSelectedNode = selectedNode;

                    //Set selected tower for tower tooltip
                    towerTooltip.towerStats = selectedTower.GetComponent<TowerStats>();
                }
                else //If there is no tower on this node
                {
                    //Hide the tooltip
                    RemoveTooltip();

                    //If a node was selected
                    if (lastSelectedNode)
                    {
                        //Deselect and clear
                        lastSelectedNode.SelectNode(false, false);
                        lastSelectedNode = null;
                    }
                }
            }
        }
    }

    //Removes the tooltip
    public void RemoveTooltip()
    {
        //Reset references
        selectedTower = null;
        //Deactivate it
        towerTooltip.gameObject.SetActive(false);
    }
}
