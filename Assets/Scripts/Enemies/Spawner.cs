using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    //The enemy prefab to spawn (will be replaced. TEMPORARY)
    public GameObject enemyPrefab;

    //Time between spawns
    private float spawnTime = 1f;

    private int enemiesToSpawn = 0;

    void Start()
    {
        GameManager.spawner = this;
    }

    public void StartSpawn(int enemyAmount, float spawnInterval)
    {
        spawnTime = spawnInterval;

        enemiesToSpawn = enemyAmount;

        StartCoroutine("SpawnTimer");
    }

    IEnumerator SpawnTimer()
    {
        while (enemiesToSpawn > 0)
        {
            yield return new WaitForSeconds(spawnTime);

            Spawn();

            enemiesToSpawn--;
        }
    }

    //Spawns an enemy prefab
    void Spawn()
    {
        GameObject obj = Instantiate(enemyPrefab, new Vector3(transform.position.x, enemyPrefab.transform.position.y, transform.position.z), enemyPrefab.transform.rotation) as GameObject;

        obj.name = enemyPrefab.name;
    }
}
