using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class ApeStrikeAreaDamage : MonoBehaviour
{
    [SerializeField] private GameObject bonobo;

    [SerializeField] private AudioClip hitAudioClip;
    [SerializeField] private AudioSource audioSource;

    List<Collider2D> enemiesHit=new List<Collider2D>();
    bool allowNextAttack = true;
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Enemy" || hit.tag == "Destructable" || hit.tag == "Obstacle")
        {
            enemiesHit.Add(hit);
        }
    }
    private void OnDisable()
    {
        HitEachWithDelay();
    }

    private async void HitEachWithDelay()
    {
        foreach (Collider2D enemyCollider in enemiesHit)
        {
            //code ready to call take damage function on the enemy
            HealthScript enemy = enemyCollider.GetComponent<HealthScript>();
            if (enemy != null)
            {
                audioSource.PlayOneShot(hitAudioClip);
                enemy.TakeDamage(bonobo.GetComponent<ApeStrike>().damage);
                bonobo.transform.position = enemyCollider.transform.position;
                await Task.Delay((int)(bonobo.GetComponent<ApeStrike>().timeInBetweenHits * 1000));
            }
        }
        enemiesHit.Clear();
    }

}
