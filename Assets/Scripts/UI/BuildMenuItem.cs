using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildMenuItem : MonoBehaviour
{
    //The tower icon to show
    public Image towerIcon;

    //Text to display tower info
    public Text infoText;

    //Text to display tower cost
    public Text costText;

    //Tower variable
    public string towerName = "";
    public int health = 0;
    public float speed = 0;
    public int power = 0;
    public float range = 0;
    public int cost = 0;

    //The tower prefab
    public GameObject towerPrefab;

    [HideInInspector] //Build menu is set externally by the build menu script
    public BuildMenu buildMenu;

    void Start()
    {
        TowerStats towerStats = towerPrefab.GetComponent<TowerStats>();

        //If a tower icon is assigned
        if(towerStats.icon)
            //Set the mage to that sprite
            towerIcon.sprite = towerStats.icon;

        //Set values from tower prefab
        towerName = towerStats.towerName;
        cost = towerStats.levels[towerStats.currentLevel].cost;
        health = towerStats.currentHealth;
        speed = towerStats.levels[towerStats.currentLevel].speed;
        range = towerStats.levels[towerStats.currentLevel].range;

        //Set the new text value by formatting the original with data
        infoText.text = string.Format(infoText.text, 
            towerName, 
            health, 
            speed, 
            power, 
            range);

        //Set cost text
        costText.text = cost.ToString();
    }

    //Selects this menu item (called from UI)
    public void Select()
    {
        buildMenu.SelectItem(this);
    }
}
