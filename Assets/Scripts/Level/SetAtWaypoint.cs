using UnityEngine;
using System.Collections;

public class SetAtWaypoint : MonoBehaviour
{
    public bool placeAtStart = false;
    public bool placeAtEnd = false;

    //Height above waypoint
    public float height = 0.325f;

    private bool canMove = true;

    void Update()
    {
        //If waypoints exist
        if (canMove && GameManager.waypointManager)
        {
            //One time use
            canMove = false;

            //Get start position
            Vector3 startPoint = GameManager.waypointManager.wayPoints[0].transform.position;
            //Get end position
            Vector3 endPoint = GameManager.waypointManager.wayPoints[GameManager.waypointManager.wayPoints.Length - 1].transform.position;

            //If the object is to be placed at the start
            if (placeAtStart)
                transform.position = new Vector3(startPoint.x, startPoint.y + height, startPoint.z);
            //Else if it is to be placed at the end
            else if (placeAtEnd)
                transform.position = new Vector3(endPoint.x, endPoint.y + height, endPoint.z);
        }
    }
}
