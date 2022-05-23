using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEnemy : MonoBehaviour
{
    [SerializeField] public float duration = 2;
    [SerializeField] string enemyTag = "Enemy";

    [NonSerialized] float durationCounter;
    [NonSerialized] private List<FollowAI> enemiesHit;
    [NonSerialized] private FollowAI enemyAI;
    [NonSerialized] private bool stunned = false;
    [NonSerialized] private bool reset = false;
    private void Start()
    {
        enemiesHit = new List<FollowAI>();
    }

    private void Update()
    {
        if (durationCounter >= 0f)
        {
            durationCounter -= Time.deltaTime;
        }
        else
        {
            foreach (FollowAI enemy in enemiesHit)
            {
                if (enemy.GetComponentInParent<HealthScript>().GetHealth() >= 0)
                {
                    enemy.enabled = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == enemyTag)
        {
            enemyAI = other.GetComponent<FollowAI>();
            if (enemyAI != null)
            {
                enemiesHit.Add(enemyAI);
                if (stunned == false)
                {
                    enemyAI.enabled = false;
                    durationCounter = duration;
                    // StartCoroutine(StunDuration());
                }
            }
        }
    }

    // public IEnumerator StunDuration()
    // {
    //     Debug.Log("stunned");
    //     yield return new WaitForSeconds(duration);
    //     Debug.Log("stunned2222");
    //     stunned = false;
    //     reset = false;
    //     foreach(FollowAI enemy in enemiesHit)
    //     {
    //         enemy.enabled = true;
    //     }
    // }

}
