using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]

public class CohesionBehavior : FlockingBehavior
{

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, return no adjustment!
        if (context.Count == 0)
            return Vector3.zero;

        //add all the points together and average them
        Vector3 cohesionMove = Vector3.zero;
        foreach (Transform item in context)
        {
            cohesionMove += (Vector3)item.position;
        }
        cohesionMove /= context.Count;
        //averages out


        //create offset from the agent position
        cohesionMove -= (Vector3)agent.transform.position;
        return cohesionMove;
        //This finds the middle point between all the neighbors and moves the agent to that position
    }

}
