using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GenerateGrid : MonoBehaviour
{
    //The node prefab to be instantiated
    public GameObject nodePrefab;

    //The size of the grid to be generated
    public Vector2 size;

    //The layer on which the grid will be generated
    public LayerMask layer;

    //Toggle to generate the grid within the editor
    public bool generateGrid = false;

    void Start()
    {
        //Prevents the grid from regenerating on game start
        generateGrid = false;
    }

    void Update()
    {
        
        if (generateGrid)
        {
            //Set the generateGrid bool to false. A method of turning a checkbox into a form of button in the inspector
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
            GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
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

                //If a raycast p[rojected downward from a point at the center of every grid square intersects the ground layer
                if (Physics.Raycast(new Vector3(x + 0.5f, 100, y + 0.5f) + transform.position, Vector3.down, out hitInfo, 200f, layer))
                {
                    //Instantiate a node at that point
                    GameObject node = Instantiate(nodePrefab, hitInfo.point, Quaternion.identity) as GameObject;
                    //Set it as a child of the grid
                    node.transform.parent = transform;
                    //Change it's name to reflect its grid position
                    node.name = nodePrefab.name + x + "x" + y + "y";
                }
            }
        }
    }
}
