using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    //A list of all enemies is the scene
    public List<GameObject> currentEnemies = new List<GameObject>();

    //How many enemies are in the round
    public int enemiesInRound = 0;

    //Called when an enemy dies
    public void RegisterDeath(GameObject enemy, int resources)
    {
        //Remove enemy from enemy list
        currentEnemies.Remove(enemy);

        //Reflects this death in counter
        enemiesInRound--;

        //Credit resources
        GameManager.resourceManager.AddResources(resources);
    }

    public void StartSpawn(int enemyAmount, float spawnInterval)
    {
        enemiesInRound = enemyAmount;

        GameManager.spawner.StartSpawn(enemyAmount, spawnInterval);
    }

    //Returns an enemy prefab
    public GameObject GetEnemyToSpawn()
    {
        GameObject[] possibleEnemies = GameManager.levelInfo.rounds[GameManager.roundManager.currentRound].enemiesToSpawn;

        //Randomly select an enemy from the list of candidates
        GameObject enemy = possibleEnemies[Random.Range(0, possibleEnemies.Length)];

        //Return this enemy
        return enemy;
    }
}
