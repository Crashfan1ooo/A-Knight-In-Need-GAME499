using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    public GameObject player;
    private Transform targetTransform;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        targetTransform = player.transform;

        navMeshAgent.destination = targetTransform.position;


    }
}
