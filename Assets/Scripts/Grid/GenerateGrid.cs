using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GenerateGrid : MonoBehaviour
{
    public GameObject nodePrefab;

    public Vector2 size;

    public LayerMask layer;

    public bool generateGrid = false;

    void Start()
    {
        generateGrid = false;
    }

    void Update()
    {
        if (generateGrid)
        {
            generateGrid = false;

            if (nodePrefab)
            {
                DestroyCurrentNodes();

                GenerateNewNodes();
            }
            else
                Debug.Log("No node prefab provided!");
        }
    }

    void DestroyCurrentNodes()
    {
        while (transform.childCount > 0)
        {
            GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    void GenerateNewNodes()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                RaycastHit hitInfo;
                if (Physics.Raycast(new Vector3(x + 0.5f, 100, y + 0.5f), Vector3.down, out hitInfo, 200f, layer))
                {
                    GameObject node = Instantiate(nodePrefab, new Vector3(x, hitInfo.point.y, y), Quaternion.identity) as GameObject;
                    node.transform.parent = transform;
                    node.name = nodePrefab.name + x + "x" + y + "y";
                }
            }
        }
    }
}
