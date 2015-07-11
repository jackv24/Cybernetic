﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    //The enemy prefab to spawn (will be replaced. TEMPORARY)
    public GameObject enemyPrefab;

    //Time between spawns
    public float spawnTime = 1;

    //The time at which the next enemy will spawn
    private float nextSpawnTime;

    private bool startSpawn = true;

    void Update()
    {
        if (startSpawn && GameManager.levelLoaded)
        {
            startSpawn = false;

            nextSpawnTime = Time.time + spawnTime;
        }

        if (GameManager.levelLoaded)
        {
            //If it is time to spawn another enemy
            if (nextSpawnTime <= Time.time)
            {
                //Set new spawn time
                nextSpawnTime += spawnTime;

                //Spawn enemy
                Spawn(enemyPrefab);
            }
        }
    }

    //Spawns an enemy prefab
    void Spawn(GameObject spawn)
    {
        GameObject obj = Instantiate(spawn, new Vector3(transform.position.x, spawn.transform.position.y, transform.position.z), spawn.transform.rotation) as GameObject;

        obj.name = spawn.name;
    }
}
