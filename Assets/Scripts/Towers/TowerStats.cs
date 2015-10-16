using UnityEngine;
using System.Collections;

public class TowerStats : MonoBehaviour
{
    //The name of the tower
    public string towerName = "";

    //The icon to display in build menu and tooltip
    public Sprite icon;

    //Current health
    public int currentHealth = 100;

    //The current level
    public int currentLevel = 0;

    public Tower[] levels;

    [HideInInspector]
    public TowerAppearance towerAppearance;

    [HideInInspector]
    public bool isSelected = false;

    void Start()
    {
        towerAppearance = GetComponent<TowerAppearance>();
    }

    public void Upgrade()
    {
        if (currentLevel + 1 < levels.Length && GameManager.resourceManager.resources >= levels[currentLevel + 1].cost)
        {
            currentLevel++;

            GameManager.resourceManager.RemoveResources(levels[currentLevel].cost);

            // Adds the health difference between levels to the tower's current health
            currentHealth += (levels[currentLevel].maxHealth - levels[currentLevel - 1].maxHealth);

            if (levels[currentLevel].material)
            {
                towerAppearance.ChangeMaterial(levels[currentLevel].material);
            }

            if (towerAppearance)
            {
                towerAppearance.ChangeRangeDisplay(levels[currentLevel].range);
            }
        }
    }
}

[System.Serializable]
public class Tower
{
    //How much the tower costs to build
    public int cost = 10;

    //Attack speed
    public float speed = 2.0f;
    //Attack range
    public float range = 2.0f;

    //The max health the tower can have
    public int maxHealth = 100;

    //The material to change to - optional
    public Material material;
}
