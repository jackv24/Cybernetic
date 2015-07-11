using UnityEngine;
using System.Collections;

public class WaypointManager : MonoBehaviour
{
    //Stores all waypoints
    public Transform[] wayPoints;

    void Start()
    {
        GameManager.waypointManager = this;
    }
}
