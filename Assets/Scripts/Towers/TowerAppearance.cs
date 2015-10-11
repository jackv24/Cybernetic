using UnityEngine;
using System.Collections;

public class TowerAppearance : MonoBehaviour
{
    public MeshRenderer[] meshRenderers;

    public GameObject rangeDisplay;

    private TowerStats towerStats;

    void Start()
    {
        towerStats = GetComponent<TowerStats>();
    }

    void Update()
    {
        rangeDisplay.SetActive(towerStats.isSelected);
    }

    public void ChangeMaterial(Material material)
    {
        foreach (MeshRenderer mesh in meshRenderers)
        {
            mesh.material = material;
        }
    }

    public void ChangeRangeDisplay(float radius)
    {
        rangeDisplay.transform.localScale = new Vector3(radius * 2, radius * 2, 1);
    }
}
