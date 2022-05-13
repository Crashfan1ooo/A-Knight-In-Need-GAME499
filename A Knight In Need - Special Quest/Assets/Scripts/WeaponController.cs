using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Sword;
    public bool canAttack = true;
    public float attackCoolDown = 1.0f;
    public AudioClip swordAttackSound;

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if(canAttack)
            {
                SwordAttack();
            }
        }

    }

    public void SwordAttack()
    {
        canAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(swordAttackSound);

        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }
}
