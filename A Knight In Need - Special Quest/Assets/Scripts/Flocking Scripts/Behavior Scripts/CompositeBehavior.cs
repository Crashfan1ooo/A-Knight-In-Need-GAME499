using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]

public class CompositeBehavior : FlockingBehavior
{
    public FlockingBehavior[] behaviors;
    public float[] weights;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //This handles data mismatch in the case that it does happen, we use this to find exactly where the error is occuring
        if (weights.Length != behaviors.Length)
        {
            Debug.Log("Data mismatch in " + name, this);
            return Vector3.zero;
        }

        //This sets up movement
        Vector3 move = Vector3.zero;

        //iterate through behaviors 
        for (int i = 0; i < behaviors.Length; i++)
        {

            Vector3 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];

            if (partialMove != Vector3.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];

                }
            }

            move += partialMove;

        }

        return move;

    }

}
