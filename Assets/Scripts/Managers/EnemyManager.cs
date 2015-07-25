using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    //Database of enemies
    public Enemy[] enemies;

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
        //a list of possible candidates to spawn
        List<GameObject> possibleEnemies = new List<GameObject>();

        //Gets the max enemy level allowed to spawn
        int maxLevel = GameManager.roundManager.rounds[GameManager.roundManager.currentRound].maxSpawnLevel;

        //Iterates through the enemy database
        for (int i = 0; i < enemies.Length; i++)
        {
            //if the enemy is within the level range
            if (enemies[i].spawnLevel <= maxLevel)
            {
                //Add it to the list of candidates
                possibleEnemies.Add(enemies[i].prefab);
            }
        }

        //Randomly select an enemy from the list of candidates
        GameObject enemy = enemies[Random.Range(0, possibleEnemies.Count)].prefab;

        //Return this enemy
        return enemy;
    }
}

//Class to hold enemy prefab information
[System.Serializable]
public class Enemy
{
    //The prefab to spawn
    public GameObject prefab;

    //What level is this enemy
    public int spawnLevel = 0;
}
