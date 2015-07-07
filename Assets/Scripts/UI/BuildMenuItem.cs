using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildMenuItem : MonoBehaviour
{
    //Text to display tower info
    public Text infoText;

    //Text to display tower cost
    public Text costText;

    //The name of the tower
    public string towerName = "";
    //Cost of the tower
    public int cost = 0;

    public GameObject towerPrefab;

    [HideInInspector]
    public BuildMenu buildMenu;

    void Start()
    {
        //Set the new text value by formatting the original with data
        infoText.text = string.Format(infoText.text, towerName);
        //Set cost text
        costText.text = cost.ToString();
    }

    //Selects this menu item (called from UI)
    public void Select()
    {
        buildMenu.SelectItem(this);
    }
}
