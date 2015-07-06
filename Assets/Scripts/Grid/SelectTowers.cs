using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlaceTowers))]
public class SelectTowers : MonoBehaviour
{
    public GameObject selectedTower;

    private PlaceTowers placeTowers;
    private Camera cam;

    void Start()
    {
        placeTowers = GetComponent<PlaceTowers>();
        cam = Camera.main;
    }

    void Update()
    {
        //Create a ray from the cursors position on the screen
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //Cast a ray out from the cursor
        if (Physics.Raycast(ray, out hitInfo, placeTowers.maxDistance, placeTowers.layer))
        {
            //Store the node hit
            Node node = hitInfo.collider.GetComponent<Node>();

            if (node.occupyingTower)
            {
                selectedTower = node.occupyingTower;
            }
        }
    }
}
