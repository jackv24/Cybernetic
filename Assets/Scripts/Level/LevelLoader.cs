using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    //Array of prefabs to instantiate when on level load
    public GameObject[] prefabs;

    void Start()
    {
        //Iterate through array
        for (int i = 0; i < prefabs.Length; i++)
        {
            //Instantiate gameobject
            GameObject obj = Instantiate(prefabs[i], prefabs[i].transform.position, prefabs[i].transform.rotation) as GameObject;

            //Set name to original
            obj.name = prefabs[i].name;
        }

        //Mark level as loaded
        GameManager.levelLoaded = true;
    }
}
