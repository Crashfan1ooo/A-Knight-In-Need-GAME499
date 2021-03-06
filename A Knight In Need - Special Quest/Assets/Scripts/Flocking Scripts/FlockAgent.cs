using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]

public class FlockAgent : MonoBehaviour
{
    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } }


    //this will allow us to access the collider without ever having to assign it.

    void Start()
    {
        agentCollider = GetComponent<Collider>();
    }

    public void Move(Vector3 velocity)
    {
        transform.forward = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
        //allows for constant movement regardless of framerate.
    }
    
}
