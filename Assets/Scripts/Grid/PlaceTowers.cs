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

    void Start()
    {
        //Set cam as the main camera
        cam = Camera.main;
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

            if (node.isAvailable && Input.GetButton("Click"))
            {
                Place(towerPrefab, node);
                node.isAvailable = false;
            }
        }
        else if (lastNode)
        {
            lastNode.SelectNode(false);
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

    void Place(GameObject towerPrefab, Node node)
    {
        GameObject tower = Instantiate(towerPrefab, node.transform.position + towerPrefab.transform.position, towerPrefab.transform.rotation) as GameObject;

        tower.transform.parent = node.transform;
        node.occupyingTower = tower;
    }
}
