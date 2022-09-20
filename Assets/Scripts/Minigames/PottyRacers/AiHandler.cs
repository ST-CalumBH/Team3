using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AiHandler : MonoBehaviour
{

    public enum AIMode { followPlayer, followWaypoints};

    [Header("AI Settings")]
    public AIMode aiMode;
    public float maxSpeed = 16;

    Vector3 targetPosition = Vector3.zero;
    Transform TargetTransform = null;

    //waypoints code
    WaypointNode currentWaypoint = null;
    WaypointNode[] allWayPoints;



    CarController carController;
    private void Awake()
    {
        carController = GetComponent<CarController>();
        allWayPoints = FindObjectsOfType<WaypointNode>();

    }

    private void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

        switch (aiMode)
        {
            case AIMode.followPlayer:
                FollowPlayer();
                break;

            case AIMode.followWaypoints:
                FollowWaypoints();
                break;
        }

        inputVector.x = TurnTowardTarget();
        inputVector.y =/*ApplyThrottleOrBreak(inputVector.x);*/ 0.8f;
        
        carController.SetInputVector(inputVector);
    }

    void FollowPlayer()
    {
        if (TargetTransform == null)
            TargetTransform = GameObject.FindGameObjectWithTag("Car").transform;

        if (TargetTransform != null)
            targetPosition = TargetTransform.position;
    }

    void FollowWaypoints()
    {
        if (currentWaypoint == null)
            currentWaypoint = FindClosestWaypoint();

        if(currentWaypoint != null)
        {
            targetPosition = currentWaypoint.transform.position;
            float distanceToWaypoint = (targetPosition - transform.position).magnitude;

            if(  distanceToWaypoint <= currentWaypoint.minDistanceToReachWaypoint)
            {
                currentWaypoint = currentWaypoint.nextWaypointNode[Random.Range(0, currentWaypoint.nextWaypointNode.Length)];
            }
        }
    }

    WaypointNode FindClosestWaypoint()
    {
        return allWayPoints
            .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
    }



    float TurnTowardTarget()
    {
        Vector2 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();

        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;

        float steerAmount = angleToTarget / 45.0f;

        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);
        return steerAmount;

    }
   /* float ApplyThrottleOrBreak(float inputX)
    {
        if (carController.GetVelocity.Magnitude() > maxSpeed)
            return 0;

        return 1.05f - Mathf.Abs(inputX) / 1.0f;
        
    }*/

}
