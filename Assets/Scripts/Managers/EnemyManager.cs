using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    //A list of all enemies is the scene
    public List<GameObject> enemies = new List<GameObject>();

    public int enemiesInRound = 0;

    public void RegisterDeath(GameObject enemy, int resources)
    {
        //Remove enemy from enemy list
        enemies.Remove(enemy);

        enemiesInRound--;

        //Credit resources
        GameManager.resourceManager.AddResources(resources);
    }

    public void StartSpawn(int enemyAmount, float spawnInterval)
    {
        enemiesInRound = enemyAmount;

        GameManager.spawner.StartSpawn(enemyAmount, spawnInterval);
    }
}
