using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public GameObject bossEnemy;

    public GameObject[] minionEnemies;
    private int minionCount;

    private bool enemiesAreDead;
 
    void Start()
    {
        

        enemiesAreDead = false;
    }


    void Update()
    {
        minionEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        //this will continously find gameobjects that are tagged with Enemy within the scene and update the array. 
        minionCount = minionEnemies.Length;
        //this will return the amount of enemies within the array to a useable integer value that we use for conditional statements
        if (minionCount <= 1)
        {
            bossEnemy.SetActive(true);
        }
    }
}
