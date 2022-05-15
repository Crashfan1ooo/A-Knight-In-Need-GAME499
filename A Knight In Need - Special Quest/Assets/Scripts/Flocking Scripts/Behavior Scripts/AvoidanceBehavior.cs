using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]

public class AvoidanceBehavior : FlockingBehavior
{

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, return no adjustment!
        if (context.Count == 0)
            return Vector3.zero;

        //add all the points together and average them
        Vector3 avoidanceMove = Vector3.zero;

        int nAvoid = 0;

        foreach (Transform item in context)
        {
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;

                avoidanceMove += (Vector3)(agent.transform.position - item.position);
            }
            
        }
        if (nAvoid > 0)
            avoidanceMove /= nAvoid;

        return avoidanceMove;
    }



}
