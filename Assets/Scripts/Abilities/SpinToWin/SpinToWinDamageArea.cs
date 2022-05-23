using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinToWinDamageArea : MonoBehaviour
{
    [SerializeField] private GameObject gorilla;
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Enemy" || hit.tag == "Destructable" || hit.tag == "Obstacle")
        {
            //code ready to call take damage function on the enemy
            HealthScript enemy = hit.GetComponent<HealthScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(gorilla.GetComponent<SpinToWin>().damagePerRotation);
            }
        }
    }
}
