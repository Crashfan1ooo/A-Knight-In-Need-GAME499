using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]

public class AlignementBehavior : FlockingBehavior
{

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, maintain current alignment
        if (context.Count == 0)
            return agent.transform.forward;

        //add all the points together and average them
        Vector3 alignmentMove = Vector3.zero;
        foreach (Transform item in context)
        {
            alignmentMove += (Vector3)item.transform.forward;
        }
        alignmentMove /= context.Count;
        //averages out


        return alignmentMove;
    }
}
