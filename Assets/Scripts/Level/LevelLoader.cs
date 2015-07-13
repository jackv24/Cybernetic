using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    //World of level
    public int world = 1;
    //Level to load within world
    public int level = 1;

    void Awake()
    {
        //Set values for world and level (if no values are stores, use world 1 level 1 as default)
        world = PlayerPrefs.GetInt("world", 1);
        level = PlayerPrefs.GetInt("level", 1);
    }

    void Start()
    {
        //If there is already a level child, disable it
        if (transform.childCount == 1)
            transform.GetChild(0).gameObject.SetActive(false);

        //Load level prefab from resouces
        GameObject levelPrefab = Resources.Load<GameObject>("Levels/World " + world + "/Level " + level);

        //If the level prefab was successfully loaded
        if (levelPrefab)
        {
            //Instantiate the level as a child of this transform
            GameObject levelObj = Instantiate(levelPrefab, levelPrefab.transform.position, levelPrefab.transform.rotation) as GameObject;
            levelObj.transform.parent = transform;

            //Set level loaded bool to true (allows other managers to start)
            GameManager.levelLoaded = true;
        }
    }
}
