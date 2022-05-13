using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController weaponC;


    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && weaponC.isAttacking)
        {
            
            
        }
    }
}
