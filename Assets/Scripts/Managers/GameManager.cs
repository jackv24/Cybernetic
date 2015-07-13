using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    //Static instance of this script
    public static GameManager gameManager;

    //Static instances of managers attached to this gameObject
    public static ResourceManager resourceManager;
    public static TowerDatabase towerDatabase;
    public static EnemyManager enemyManager;

    //Static instance of the waypoint manager (attached to the level)
    public static WaypointManager waypointManager;

    //Has the level been loaded?
    public static bool levelLoaded = false;

    void Start()
    {
        //Set the instance for this game manager
        gameManager = this;

        //Set other static instances
        resourceManager = GetComponent<ResourceManager>();
        towerDatabase = GetComponent<TowerDatabase>();
        enemyManager = GetComponent<EnemyManager>();
    }
}