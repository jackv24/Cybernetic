using UnityEngine;
using System.Collections;

public class SetAtWaypoint : MonoBehaviour
{
    public bool placeAtStart = false;
    public bool placeAtEnd = false;

    public float height = 0.325f;

    private bool canMove = true;

    void Update()
    {
        if (canMove && GameManager.waypointManager)
        {
            canMove = false;

            Vector3 startPoint = GameManager.waypointManager.wayPoints[0].transform.position;
            Vector3 endPoint = GameManager.waypointManager.wayPoints[GameManager.waypointManager.wayPoints.Length - 1].transform.position;

            if (placeAtStart)
                transform.position = new Vector3(startPoint.x, startPoint.y + height, startPoint.z);
            else if (placeAtEnd)
                transform.position = new Vector3(endPoint.x, endPoint.y + height, endPoint.z);
        }
    }
}
