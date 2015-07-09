using UnityEngine;
using System.Collections;

public class TowerTargets : MonoBehaviour
{
    //The current target
    public Transform target;

    //Script references
    private EnemyManager enemyManager;
    private TowerStats towerStats;

    void Start()
    {
        //FInd enemy manager attached to game controller
        enemyManager = GameObject.FindWithTag("GameController").GetComponent<EnemyManager>();
        //Get tower stats attached to this tower
        towerStats = GetComponent<TowerStats>();
    }

    void Update()
    {
        //If there is a target
        if (!target)
        {
            //Minimum distance
            float dist = towerStats.range;

            //Iterate through all possible enemy targets
            foreach (GameObject enemy in enemyManager.enemies)
            {
                //If the enemy is closest
                if (Vector3.Distance(transform.position, enemy.transform.position) < dist)
                {
                    //Set it as the target
                    target = enemy.transform;

                    //make minimum distance this
                    dist = Vector3.Distance(transform.position, enemy.transform.position);
                }
            }
        }

        //If the target goes out of range
        if(target && Vector3.Distance(transform.position, target.position) > towerStats.range)
        {
            //Set target as null
            target = null;
        }
    }
}
