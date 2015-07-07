using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlaceTowers))]
public class SelectTowers : MonoBehaviour
{
    //The prefab to instantiate for the tooltip
    public GameObject tooltipPrefab;

    //The instantiated tooltip
    private GameObject tooltip;
    //Holds a reference to the script on this tooltip
    private TowerTooltip towerTooltip;

    //The height to display the tooltip above a node
    public float toolTipHeight = 1f;

    [HideInInspector] //The currently selected tower
    public GameObject selectedTower;

    //Reference to the placetowers script
    private PlaceTowers placeTowers;
    //Reference to the main camera
    private Camera cam;

    void Start()
    {
        //Instantiates and hides the tooltip
        tooltip = Instantiate(tooltipPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        tooltip.SetActive(false);
        tooltip.name = tooltipPrefab.name;
        
        //Gets a reference to the tooltip script
        towerTooltip = tooltip.GetComponent<TowerTooltip>();

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
                Node node = hitInfo.collider.GetComponent<Node>();

                //If there is a tower occupying this node
                if (node.occupyingTower)
                {
                    //Set the selected tower to the node's occupying tower
                    selectedTower = node.occupyingTower;
                    //Display the tooltip above this tower
                    DisplayTooltip(selectedTower.transform.position, toolTipHeight);

                    towerTooltip.selectedNode = node;
                    towerTooltip.selectTowers = this;
                }
                else //If there is no tower on this node
                {
                    //Hide the tooltip
                    RemoveTooltip();
                }
            }
        }
    }

    //Displays the tooltip
    void DisplayTooltip(Vector3 position, float height)
    {
        //Sets its position to a certain height above the node position
        tooltip.transform.position = position + Vector3.up * height;
        //Enables the tooltip
        tooltip.SetActive(true);
    }

    //Removes the tooltip
    public void RemoveTooltip()
    {
        //Reset references
        selectedTower = null;
        //Deactivate it
        tooltip.SetActive(false);
    }
}
