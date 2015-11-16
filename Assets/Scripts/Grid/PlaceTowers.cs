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
    private PlaceNode lastNode;

    public bool canPlace = true;

    void Start()
    {
        //Set cam as the main camera
        cam = Camera.main;
    }

    void Update()
    {
        canPlace = !GameManager.roundManager.isDefendRound;

        //Create a ray from the cursors position on the screen
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //If there is a tower prefab to be placed
        if (towerPrefab)
        {
            //Cast a ray out from the cursor
            if (Physics.Raycast(ray, out hitInfo, maxDistance, layer))
            {
                //Store the node hit
                PlaceNode node = hitInfo.collider.GetComponent<PlaceNode>();

                //Set node state
                HoverNode(node);

                //If the node is avalable, and the click button is pressed when the cursor is not over a UI element
                if (canPlace && node.isAvailable && Input.GetButton("Click") && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    //If there are enough resources available
                    if (GameManager.resourceManager.resources >= towerPrefab.GetComponent<TowerStats>().levels[towerPrefab.GetComponent<TowerStats>().currentLevel].cost)
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
    }

    //Sets node states
    void HoverNode(PlaceNode node)
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
    void Place(GameObject towerPrefab, PlaceNode node)
    {
        //Instantiates the tower at the node position
        GameObject tower = Instantiate(towerPrefab, 
            node.transform.position, 
            towerPrefab.transform.rotation) as GameObject;

        //Makes the tower a child of the node
        tower.transform.parent = node.transform;
        //Sets the nodes occupying tower to this tower
        node.occupyingTower = tower;

        //Charge the cost of this tower
        GameManager.resourceManager.RemoveResources(towerPrefab.GetComponent<TowerStats>().levels[towerPrefab.GetComponent<TowerStats>().currentLevel].cost);

        SoundManager.instance.PlaySound("click");
    }

    //Sets the tower prefab to instantiate
    public void SetCurrentTower(GameObject tower)
    {
        towerPrefab = tower;
    }
}
