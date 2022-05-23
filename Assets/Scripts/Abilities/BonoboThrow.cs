using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BonoboThrow : MonoBehaviour
{
    [SerializeField] GameObject gorilla;
    [SerializeField] float speed = 20f;
    [SerializeField] int damage = 30;
    [SerializeField] float range = 1;
    [SerializeField] float haltTimeOnLand = 0.2f;
    [SerializeField] public float cooldown = 4;
    [Space(15)]

    [SerializeField] private AudioClip dashSound;
    [NonSerialized] private AudioSource audioSource;

    [NonSerialized] public bool isThrown = false;
    [NonSerialized] private Animator bonoboAnimator;
    [NonSerialized] PlayerScript bonoboScript;
    [NonSerialized] GorillaScript gorillaScript;
    [NonSerialized] FollowAI gorillaFollowAI;
    [NonSerialized] bool bonoboThrowAvailable;
    [NonSerialized] Vector3 targetPos;
    [NonSerialized] bool stopThrow;

    void Start()
    {
        bonoboThrowAvailable = true;
        gorillaScript = gorilla.GetComponent<GorillaScript>();
        gorillaFollowAI = gorilla.GetComponent<FollowAI>();
        bonoboScript = gameObject.GetComponent<PlayerScript>();
        bonoboAnimator = bonoboScript.anim;
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Alpha1) && bonoboScript.isOnGorilla) || (bonoboScript.isOnGorilla && gorillaScript.gorillaHealth <= 0))
        {
            if (!bonoboThrowAvailable) return;
            StartCoroutine(StartCooldown());
            
            bonoboScript.isInvincible = true;
            if (gorillaScript.gorillaHealth > 0)
            {
                gorillaFollowAI.enabled = true;
            }
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos = transform.position + (targetPos - transform.position).normalized * 1000.0f;
            isThrown = true;
            bonoboScript.DetachDuo();
            bonoboAnimator.SetBool("Spin", true);
            audioSource.PlayOneShot(dashSound);
            StartCoroutine(ThrowRange());
        }

        if (isThrown)
        {
            gorillaScript.moveDirection = Vector2.zero;
            gorillaScript.move = Vector2.zero;
            targetPos.z = transform.position.z;
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPos, speed * Time.deltaTime);
            if (stopThrow)
            {
                StartCoroutine(bonoboScript.HaltMovement(haltTimeOnLand));
                isThrown = false;
                bonoboScript.isInvincible = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (!isThrown) return;
        if (hit.tag == "Enemy" || hit.tag == "Destructable" || hit.tag == "Obstacle")
        {
            HealthScript enemy = hit.GetComponent<HealthScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            bonoboAnimator.SetBool("Spin", false);
            StartCoroutine(bonoboScript.HaltMovement(haltTimeOnLand));
            isThrown = false;
            bonoboScript.isInvincible = false;
        }
    }

    IEnumerator ThrowRange()
    {
        stopThrow = false;
        yield return new WaitForSeconds(range);
        stopThrow = true;
        bonoboAnimator.SetBool("Spin", false);
    }

    public IEnumerator StartCooldown()
    {
        bonoboThrowAvailable = false;
        yield return new WaitForSeconds(cooldown);
        bonoboThrowAvailable = true;
    }
}
