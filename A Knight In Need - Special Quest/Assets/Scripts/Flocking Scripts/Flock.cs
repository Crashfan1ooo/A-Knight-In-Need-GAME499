using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    //This will store all of our agents so we can use them

    public FlockingBehavior behavior;
    //this is a reference to our flockingbehavior script that will allow us to change the behavior for all of the agents

    [Range(10, 500)]
    public int startingCount = 250;
    //this will allow us to change how many agents are in a flock
    const float AgentDensity = 0.08f;


    //BEHAVIORS FOR THE FLOCK BELOW: Movements, Rotations, etc
    [Range(1f, 100f)]
    public float driveFactor = 10f;

    [Range(1f, 100f)]
    public float maxSpeed = 5f;


    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    //How we will calculate the distance to the neighboring agents

    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;


    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    //These are Utility variables that saves us from doing extra math that we don't want to do :D


    void Start()
    {

        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        //These are utility values that are used for later methods

        for (int i = 0; i < startingCount; i++)
            //basic for loop
        {
            FlockAgent newAgent = Instantiate
                (
                agentPrefab,
                Random.insideUnitSphere * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );

            newAgent.name = "Agent " + i;
            agents.Add(newAgent);
        }

    }


    void Update()
    {
        //This iterate through each agent that is within the List
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            //This sets up a list that will hold the "context" of information within the agent radius

            Vector3 move = behavior.CalculateMove(agent, context, this);
            //the behavior takes over at this point and returns back vector3 that shows how the agent should move
            move *= driveFactor;
            //drive factor allows for speedier movement and caps out at the maximum speed

            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
                //this basically says reset it back to a magnitude of 1 and multiply it so its at the max speed
            }

            agent.Move(move);
            //this basically just uses the values we just calculated and moves the agent.
        }

    }


    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        //This will uses Physics within Unity to 
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        //This will look for a bunch of colliders within the array and take the transform and put it into a list

        foreach (Collider c in contextColliders)
        {

            if (c != agent.AgentCollider)
            {
                //as long as the collider isnt the collider we are currently working with then
                context.Add(c.transform);
            }
            //!= is "Does not Equal" in case you didn't know :D I didn't for the longest
        }
        return context;

    }


}
