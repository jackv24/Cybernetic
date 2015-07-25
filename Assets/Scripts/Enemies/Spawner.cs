using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    //The enemy prefab to spawn
    private GameObject enemyPrefab;

    //Time between spawns
    public float spawnTime = 1f;

    //Amount of enemies left to spawn in this round
    private int enemiesToSpawn = 0;

    void Start()
    {
        //Sets a static reference to this script in the game manager
        GameManager.spawner = this;
    }

    //Sets initial values and starts spawning
    public void StartSpawn(int enemyAmount, float spawnInterval)
    {
        //Sets the time between spawns (spawnInterval is spawns per second)
        spawnTime = 1 / spawnInterval;

        //How many enemies to spawn this round
        enemiesToSpawn = enemyAmount;

        //Begin spawning
        StartCoroutine("SpawnTimer");
    }

    //Spawns enemies
    IEnumerator SpawnTimer()
    {
        //While there are still enemies left to spawn
        while (enemiesToSpawn > 0)
        {
            //Wait for spawntime
            yield return new WaitForSeconds(spawnTime);

            //Spawn an enemy
            Spawn();

            //Decrement the amount of enemies left to spawn
            enemiesToSpawn--;
        }
    }

    //Spawns an enemy prefab
    void Spawn()
    {
        //Gets the enemy prefab to spawn
        enemyPrefab = GameManager.enemyManager.GetEnemyToSpawn();
        //Spawns the enemy
        GameObject obj = Instantiate(enemyPrefab, new Vector3(transform.position.x, enemyPrefab.transform.position.y, transform.position.z), enemyPrefab.transform.rotation) as GameObject;
        
        //Sets the enemy name
        obj.name = enemyPrefab.name;
    }
}
