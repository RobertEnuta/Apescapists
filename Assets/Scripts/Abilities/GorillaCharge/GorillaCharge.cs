using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaCharge : MonoBehaviour
{
    [SerializeField] GameObject bonobo;
    [SerializeField] private GameObject landingRange;
    [Space(15)]

    [SerializeField] float speed = 10;
    [SerializeField] int chargeDamage = 20;
    [SerializeField] public int landingDamage = 30;
    [SerializeField] public float cooldown = 5f;
    [Space(15)]

    [SerializeField] AudioClip sound;
    [NonSerialized] AudioSource source;

    [NonSerialized] public bool isCharging = false;
    [NonSerialized] bool gorillaChargeAvailable = true;
    [NonSerialized] PlayerScript bonoboScript;
    [NonSerialized] GorillaScript gorillaScript;
    [NonSerialized] Animator gorillaAnimator;
    [NonSerialized] FollowAI followAI;
    [NonSerialized] Vector3 targetPos;

    void Start()
    {
        gorillaAnimator = GetComponent<Animator>();
        gorillaScript = GetComponent<GorillaScript>();
        bonoboScript = bonobo.GetComponent<PlayerScript>();
        landingRange.GetComponent<Renderer>().enabled = false;
        landingRange.GetComponent<CircleCollider2D>().enabled = false;
        followAI = GetComponent<FollowAI>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        //upon pressing 2 and having the duo be together, gorilla rushes towards the cursor
        if (Input.GetKeyDown(KeyCode.Alpha2) && bonoboScript.isOnGorilla)
        {
            if (!gorillaChargeAvailable) return;
            StartCoroutine(StartCooldown());

            isCharging = true;

            //sets the position to which the gorilla has to charge
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bonoboScript.DetachDuo();
        }

        if (isCharging == true)
        {
            Vector3 targetDirection = (targetPos - transform.position).normalized;
            gorillaAnimator.SetFloat("Horizontal", targetDirection.x);
            gorillaAnimator.SetFloat("Vertical", targetDirection.y);
            gorillaAnimator.SetFloat("Speed", 0.2f);
            //detaches gorilla and bonobo and sends gorila to the cursor's position from when 2 was pressed

            //sets move to zero so that it doesn't just flow away after landing is done
            gorillaScript.moveDirection = Vector2.zero;
            gorillaScript.move = Vector2.zero;
            targetPos.z = transform.position.z;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if (transform.position == targetPos)
            {
                StartCoroutine(ProcessLanding());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (!isCharging) return;
        if (hit.tag == "Wall") { StartCoroutine(ProcessLanding()); }
        if (hit.tag == "Enemy" || hit.tag == "Destructable" || hit.tag == "Obstacle")
        {
            HealthScript enemy = hit.GetComponent<HealthScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(chargeDamage);
            }
        }
    }

    public IEnumerator ProcessLanding()
    {
        gorillaAnimator.SetFloat("Horizontal", 0);
        gorillaAnimator.SetFloat("Vertical", 0);
        gorillaAnimator.SetFloat("Speed", 0);
        landingRange.GetComponent<CircleCollider2D>().enabled = true;
        landingRange.GetComponent<Renderer>().enabled = true;
        source.PlayOneShot(sound);
        StartCoroutine(gorillaScript.HaltMovement(0.5f));
        isCharging = false;
        yield return new WaitForSeconds(0.5f);
        landingRange.GetComponent<Renderer>().enabled = false;
        landingRange.GetComponent<CircleCollider2D>().enabled = false;
        followAI.enabled = true;
    }

    public IEnumerator StartCooldown()
    {
        gorillaChargeAvailable = false;
        yield return new WaitForSeconds(cooldown);
        gorillaChargeAvailable = true;
    }
}
