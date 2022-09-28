using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNode : MonoBehaviour
{

    [Header ("Speed set once we reach the waypoint")]
    public float maxSpeed = 0;

    [Header("this is the waypoint to go towards, not yet reached")]
    public float minDistanceToReachWaypoint = 5;


    public WaypointNode[] nextWaypointNode;

}
