using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointController : MonoBehaviour
{
    /* this simple script controls basic enemy AI pathing along created waypoints, via empty game objects that store transform data. 
     * those gameobject transforms will be placed into a list variable using the inspector and the enemy will move to those positions ordered from first to last
     * and repeats back to the first waypoint creating a loop.
     * The loop gets broken when the player enters the look radius, which is listed as a public float that is configurable. 
    */

    bool playerDetected = false;
    public List<Transform> waypoints = new List<Transform>();
    private Transform targetWaypoint;
    private int targetWaypointIndex;

    private float minDistance = 0.1f;

    private float lastWaypointIndex;


    public float movementSpeed;
    public float rotationSpeed = 1.0f;


    public float lookRadius = 10f;
    private Transform playerTarget;
    private float chaseDistance;

    private void Start()
    {
        lastWaypointIndex = waypoints.Count - 1;

        targetWaypoint = waypoints[targetWaypointIndex];


        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;


    }

    private void Update()
    {
        float rotationStep = rotationSpeed * Time.deltaTime;

        Vector3 directionToTarget = targetWaypoint.position - transform.position;
        Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);
        //This rotates the enemy as they move towards the next waypoint. Slerp allows for a smoother rotation, makes it so the rotation doesnt happen instantly. Uses the Rotation step variable, so you can change how fast the rotation happens.

        Debug.DrawRay(transform.position, transform.forward * 25f, Color.green, 0f);
        Debug.DrawRay(transform.position, directionToTarget, Color.red, 0f);

        
        chaseDistance = Vector3.Distance(playerTarget.position, transform.position);


        if (chaseDistance <= lookRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, movementSpeed * Time.deltaTime);
            Debug.Log("Player is within Range, following the player with a distance of " + chaseDistance);
            playerDetected = true;

        }
        else
            playerDetected = false;




        float movementStep = movementSpeed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        //Debug.Log("Distance: " + distance);
        CheckDistanceToWaypoint(distance);
        if (playerDetected == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movementStep);
        }



    }

    void CheckDistanceToWaypoint(float currentDistance)
    {
        if (currentDistance <= minDistance && chaseDistance > lookRadius)
        {
            Debug.Log("Player is out of range, moving to next waypoint");
            targetWaypointIndex++;
            playerDetected = false;
            UpdateTargetWaypoint();
        }
    }

    void UpdateTargetWaypoint()
    {
        if (targetWaypointIndex > lastWaypointIndex && chaseDistance > lookRadius)
        // if the target waypoint index is greater than the lastwaypoint index it will reset the pathing of the enemy back from the first waypoint within the list.
        {
            targetWaypointIndex = 0;
        }

        targetWaypoint = waypoints[targetWaypointIndex];
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
