using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockingBehavior : ScriptableObject
{

    public abstract Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
    // an abstract,which is a restricted class that cannot be used to create objects but rather is used to inherit from,
    // method that includes the behaviors for what we want,
    // This returns a Vector 3 called Calculate move that contains information from other scripts used for the flocking mechanism
    //Flock agent is the object, List Transform will put all the agents into the list and allows us to see all the agents around the flock. 
}
