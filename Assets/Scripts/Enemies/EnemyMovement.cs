using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    //Movement speed
    public float speed = 1f;
    private float currentSpeed = 1f;

    //The height at which the enemy will spawn
    private float height;

    public LevelNode nextNode = null;

    void Start()
    {
        currentSpeed = speed;

        //Set enemy height to intitial y value
        height = transform.position.y;
    }

    void Update()
    {
            //If there are waypoints left
            if (nextNode != null)
            {
                //Target position to move towards
                Vector3 targetPos = new Vector3(nextNode.x, height, nextNode.y);

                currentSpeed = speed * GameManager.enemySpeed;

            //If the enemy has not yet reached the waypoint
            if (transform.position != targetPos)
            {
                //Move towards the waypoint at speed with respect to time
                transform.position = Vector3.MoveTowards(transform.position, targetPos, currentSpeed * Time.deltaTime);

                transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));
            }
            else //If the enemy has reached the waypoint
                nextNode = nextNode.nextNode;
            }
    }
}
