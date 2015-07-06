using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlaceTowers))]
public class SelectTowers : MonoBehaviour
{
    public GameObject tooltipPrefab;

    private GameObject tooltip;
    private TowerTooltip towerTooltip;

    public float toolTipHeight = 1f;

    public GameObject selectedTower;

    private PlaceTowers placeTowers;
    private Camera cam;

    void Start()
    {
        tooltip = Instantiate(tooltipPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        tooltip.SetActive(false);
        tooltip.name = tooltipPrefab.name;

        towerTooltip = tooltip.GetComponent<TowerTooltip>();

        placeTowers = GetComponent<PlaceTowers>();
        cam = Camera.main;
    }

    void Update()
    {
        //Create a ray from the cursors position on the screen
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Input.GetButtonDown("Click") && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            //Cast a ray out from the cursor
            if (Physics.Raycast(ray, out hitInfo, placeTowers.maxDistance, placeTowers.layer))
            {
                //Store the node hit
                Node node = hitInfo.collider.GetComponent<Node>();

                if (node.occupyingTower)
                {
                    selectedTower = node.occupyingTower;
                    DisplayTooltip(selectedTower.transform.position, toolTipHeight);

                    towerTooltip.selectedNode = node;
                    towerTooltip.selectTowers = this;
                }
                else
                {
                    RemoveTooltip();
                }
            }
        }
    }

    void DisplayTooltip(Vector3 position, float height)
    {
        Vector3 pos = position + Vector3.up * height;

        tooltip.transform.position = pos;
        tooltip.SetActive(true);
    }

    public void RemoveTooltip()
    {
        selectedTower = null;
        tooltip.SetActive(false);
    }
}
