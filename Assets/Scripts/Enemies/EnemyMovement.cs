using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    //Movement speed
    public float speed = 1f;
    private float currentSpeed = 1f;

    //The height at which the enemy will spawn
    private float height;

    //Current waypoint target index
    private int target = 0;

    private WaypointManager waypointManager;

    void Start()
    {
        waypointManager = GameManager.waypointManager;

        currentSpeed = speed;

        //Set enemy height to intitial y value
        height = transform.position.y;
    }

    void Update()
    {
        //If there is a waypoint manager
        if (waypointManager)
        {
            //If there are waypoints left
            if (target < waypointManager.wayPoints.Length)
            {
                //Target position to move towards
                Vector3 targetPos = new Vector3(waypointManager.wayPoints[target].position.x, waypointManager.wayPoints[target].position.y + height, waypointManager.wayPoints[target].position.z);

                currentSpeed = speed * GameManager.enemySpeed;

                //If the enemy has not yet reached the waypoint
                if (transform.position != targetPos)
                    //Move towards the waypoint at speed with respect to time
                    transform.position = Vector3.MoveTowards(transform.position, targetPos, currentSpeed * Time.deltaTime);
                else //If the enemy has reached the waypoint
                    target++; //Set the target waypoint next
            }
        }
    }
}
