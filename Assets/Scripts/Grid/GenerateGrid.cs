using UnityEngine;
using System.Collections;

public class GenerateGrid : MonoBehaviour
{
    //The node prefab to be instantiated
    public GameObject nodePrefab;

    //The size of the grid to be generated
    public Vector2 size;

    //The layer on which the grid will be generated
    public LayerMask layer;

    //Determine whether the selected node can be built on
    public string buildTag = "Build";
    public string pathTag = "Path";

    //Toggle to generate the grid on game start
    private bool generateGrid = true;

    void Update()
    {
        //If the grid shall generate, and the level has loaded
        if (generateGrid && GameManager.levelLoaded)
        {
            generateGrid = false;

            //If there is a node prefab
            if (nodePrefab)
            {
                //Destroy the current grid...
                DestroyCurrentNodes();
                //...and generate a new one
                GenerateNewNodes();
            }
            else
                //If there is no node prefab provided, display a warning
                Debug.Log("No node prefab provided!");
        }
    }

    //Destroys the current grid
    void DestroyCurrentNodes()
    {
        //While the grid object still has child objects...
        while (transform.childCount > 0)
        {
            //...destroy them
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    //Generated a new grid of nodes
    void GenerateNewNodes()
    {
        //For every column
        for (int x = 0; x < size.x; x++)
        {
            //Generate a set of rows (to form a grid)
            for (int y = 0; y < size.y; y++)
            {
                //Stores info from the raycast
                RaycastHit hitInfo;

                //If a raycast projected downward from a point at the center of every grid square intersects the ground layer
                if (Physics.Raycast(new Vector3(x + 0.5f, 100, y + 0.5f) + transform.position, Vector3.down, out hitInfo, 200f, layer))
                {
                    //Instantiate a node at that point
                    GameObject node = Instantiate(nodePrefab, hitInfo.point, Quaternion.identity) as GameObject;
                    //Set it as a child of the grid
                    node.transform.parent = transform;
                    //Change it's name to reflect its grid position
                    node.name = nodePrefab.name + x + "x" + y + "y";

                    //If the ray did not collide with a path
                    if (hitInfo.collider.tag != pathTag)
                    {
                        PlaceNode n = node.GetComponent<PlaceNode>();

                        //Make this node a build node
                        n.canPlace = true;
                        n.isAvailable = true;
                    }
                }
            }
        }
    }
}
