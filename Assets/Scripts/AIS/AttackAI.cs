using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackAI : MonoBehaviour
{
    //damage done per each attack
    [SerializeField] int attackDamage = 10;
    //attacks per second
    [SerializeField] float attackSpeed = 2;
    //distance between attacker and target
    [SerializeField] float attackRange = 3;

    [NonSerialized] private float pauseTime = 1f;
    [NonSerialized] private bool attacked = false;
    
    [SerializeField] Animator anim;

    private void Start()
    {
        pauseTime = pauseTime / attackSpeed;
        anim = gameObject.GetComponent<Animator>();
    }
    float aTimer;
    private void Update()
    {
        if (aTimer >= 0)
        {
            aTimer -= Time.deltaTime;
        }
        else
        {
            attacked = false;          
        }
    }
    public void Attack(GameObject target)
    {
        if (target.tag == "Player")
        {
            if (attacked == false)
            {
                attacked = true;
                anim.SetTrigger("Attack");
                gameObject.GetComponent<SoundRandomizer>().PlaySound(); 

                if (target.name == "Gorilla")
                {
                    target.GetComponent<GorillaScript>().TakeDamage(attackDamage);
                }
                else if ((target.name == "Bonobo" && target.GetComponent<BoxCollider2D>().enabled == true) || (target.name == "Bonobo" && target.GetComponent<PlayerScript>().isOnGorilla == true))
                {
                    target.GetComponent<PlayerScript>().TakeDamage(attackDamage);
                }
                aTimer = pauseTime;
            }
        }
    }

    public IEnumerator PauseAttack()
    {
        attacked = true;
        yield return new WaitForSeconds(pauseTime);
        attacked = false;
    }

}
