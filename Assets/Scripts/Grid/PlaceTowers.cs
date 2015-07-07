using UnityEngine;
using System.Collections;

public class PlaceTowers : MonoBehaviour
{
    //The prefab of the tower to place (temporary, will be replaced to allow different kinds of towers)
    public GameObject towerPrefab;

    //The maximum distance from the camera a tower can be placed
    public float maxDistance = 100.0f;
    //The layer on which the nodes are located
    public LayerMask layer;

    //The camera to project rays from
    private Camera cam;

    //The last node wich was selected
    private Node lastNode;

    //The resource manager
    private ResourceManager resourceManager;

    void Start()
    {
        //Set cam as the main camera
        cam = Camera.main;

        //Gets a reference to the resource manager
        resourceManager = GameObject.FindWithTag("GameController").GetComponent<ResourceManager>();
    }

    void Update()
    {
        //Create a ray from the cursors position on the screen
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //Cast a ray out from the cursor
        if (Physics.Raycast(ray, out hitInfo, maxDistance, layer))
        {
            //Store the node hit
            Node node = hitInfo.collider.GetComponent<Node>();

            //Set node state
            HoverNode(node);

            //If the node is avalable, and the click button is pressed when the cursor is not over a UI element
            if (node.isAvailable && Input.GetButton("Click") && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                //If there are enough resources available
                if (resourceManager.resources >= towerPrefab.GetComponent<TowerStats>().cost)
                {
                    //Place the tower
                    Place(towerPrefab, node);
                    //Set the nodes availability to false
                    node.isAvailable = false;
                }
            }
        }
        //If the user clicks on something that is not a node
        else if (lastNode)
        {
            //Deselect the last node
            lastNode.SelectNode(false);
            //Clear the reference to the last node
            lastNode = null;
        }
    }

    //Sets node states
    void HoverNode(Node node)
    {
        //If a new node is selected
        if (node != lastNode)
        {
            //Reset the last node
            if (lastNode)
                lastNode.SelectNode(false);

            //Set this node as selected
            node.SelectNode(true);

            //Set this node as the last node
            lastNode = node;
        }
    }

    //Places a tower
    void Place(GameObject towerPrefab, Node node)
    {
        //Instantiates the tower at the node position
        GameObject tower = Instantiate(towerPrefab, node.transform.position + towerPrefab.transform.position, towerPrefab.transform.rotation) as GameObject;

        //Makes the tower a child of the node
        tower.transform.parent = node.transform;
        //Sets the nodes occupying tower to this tower
        node.occupyingTower = tower;

        //Charge the cost of this tower
        resourceManager.RemoveResources(tower.GetComponent<TowerStats>().cost);
    }
}
