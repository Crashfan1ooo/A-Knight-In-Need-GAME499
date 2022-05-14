using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    public GameObject player;
    private Transform targetTransform;

    public int bossHitPoints = 30;  //This is for when the sword collides, damage is done in increments of 3
    public WeaponController playerSword;  //This is for a reference to the sword
    public Player thePlayer; //Reference to the player itself
    public bool coolingDown = false;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        targetTransform = player.transform;

        navMeshAgent.destination = targetTransform.position;

        if (bossHitPoints <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Boss dead af");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword" && playerSword.isAttacking == true)
        {
            bossHitPoints--;
            Debug.Log("Boss taking damage...");
        }

        if (other.tag == "Player")
        {
            if (coolingDown == false)
            {
                thePlayer.Health =  thePlayer.Health - 1;
                coolingDown = true;
                Debug.Log("Player taking damage...");
                StartCoroutine(AttackCoolDown(3.0f));


            }
        }
    }

    private IEnumerator AttackCoolDown(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        coolingDown = false;
    }
}
