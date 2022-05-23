using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    [SerializeField] GameObject impactParticle;
    [SerializeField] float bulletKnockbackForce = 100;
    HealthScript enemy;

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Enemy" || hit.tag == "Destructable" || hit.tag == "Obstacle")
        {
            enemy = hit.GetComponent<HealthScript>();
            ProcessKnockback(hit);
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            GameObject particleInstance = Instantiate(impactParticle, hit.transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
    }

    private void ProcessKnockback(Collider2D hit)
    {
        Vector2 difference = (hit.transform.position - transform.position).normalized;
        Vector2 force = difference * bulletKnockbackForce;
        hit.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }
}
