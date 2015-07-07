using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisplayBuildMenu : MonoBehaviour
{
    //Menu item prefab to instantiate
    public GameObject menuItemPrefab;

    //A list of all menu items
    public List<GameObject> menuItems = new List<GameObject>();

    //top displacement
    public float topPos = -10f;
    //the height of each item
    public float itemHeight = 84f;

    //Database of towers
    private TowerDatabase towerDatabase;

    void Start()
    {
        //Get the tower database from the game controller
        towerDatabase = GameObject.FindWithTag("GameController").GetComponent<TowerDatabase>();

        //For every tower in the database
        for (int i = 0; i < towerDatabase.towers.Length; i++)
        {
            //Instantiate a menu item
            GameObject menuItem = Instantiate(menuItemPrefab, 
                new Vector3(menuItemPrefab.transform.position.x, 
                topPos * (i + 1) - (itemHeight * i), 
                menuItemPrefab.transform.position.z), 
                Quaternion.identity) as GameObject;

            //Set it's parent to the build menu
            menuItem.transform.SetParent(transform, false);

            //Set its name to that of the tower
            menuItem.name = towerDatabase.towers[i].name;

            //Add the menu item to the menu item list
            menuItems.Add(menuItem);

            //Get references to build menu and tower stats
            BuildMenuItem buildMenuItem = menuItem.GetComponent<BuildMenuItem>();
            TowerStats towerStats = towerDatabase.towers[i].GetComponent<TowerStats>();

            //Set menu item data
            buildMenuItem.towerName = towerStats.towerName;
            buildMenuItem.cost = towerStats.cost;
        }
    }
}
