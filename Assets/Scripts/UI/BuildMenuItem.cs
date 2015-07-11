using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildMenuItem : MonoBehaviour
{
    //Text to display tower info
    public Text infoText;

    //Text to display tower cost
    public Text costText;

    //Tower variable
    public string towerName = "";
    public int health = 0;
    public int speed = 0;
    public int power = 0;
    public float range = 0;
    public int cost = 0;

    //The tower prefab
    public GameObject towerPrefab;

    [HideInInspector] //Build menu is set externally by the build menu script
    public BuildMenu buildMenu;

    void Start()
    {
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
