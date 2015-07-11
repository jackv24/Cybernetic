using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public static ResourceManager resourceManager;
    public static TowerDatabase towerDatabase;
    public static EnemyManager enemyManager;

    public static WaypointManager waypointManager;

    public static bool levelLoaded = false;

    void Start()
    {
        gameManager = this;

        resourceManager = GetComponent<ResourceManager>();
        towerDatabase = GetComponent<TowerDatabase>();
        enemyManager = GetComponent<EnemyManager>();
    }
}