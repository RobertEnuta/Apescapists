using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    [SerializeField] private int damage = 20;
    [SerializeField] float knockbackForce = 1;
    [SerializeField] float rangeinSeconds = 2f;
    PlayerScript bonobo;
    GorillaScript gorilla;
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Player")
        {
            bonobo = hit.GetComponent<PlayerScript>();
            gorilla = hit.GetComponent<GorillaScript>();
            //ProcessKnockback(hit);
            if (bonobo != null)
            {
                bonobo.TakeDamage(damage);
            }

            if (gorilla != null)
            {
                gorilla.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
    private void Update()
    {
        Destroy(gameObject, rangeinSeconds);
    }
    private void ProcessKnockback(Collider2D hit)
    {
        Vector2 difference = (hit.transform.position - transform.position).normalized;
        Vector2 force = difference * knockbackForce;
        hit.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }
}
