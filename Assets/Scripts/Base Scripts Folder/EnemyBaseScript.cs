using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseScript : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;

    [SerializeField] float health = 100;

    //animation booleans
    private bool isAttacking;
    private bool isRunning;

    private void Update() 
    {
            
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        TakeDamage(10f);    
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
