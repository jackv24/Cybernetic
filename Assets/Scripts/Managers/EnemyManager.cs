using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    //A list of all enemies is the scene
    public List<GameObject> enemies = new List<GameObject>();

    public void RegisterDeath(GameObject enemy, int resources)
    {
        //Remove enemy from enemy list
        enemies.Remove(enemy);
        //Credit resources
        GameManager.resourceManager.AddResources(resources);
    }
}
