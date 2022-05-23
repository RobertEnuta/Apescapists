using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallToBonobo : MonoBehaviour
{
    [SerializeField] GameObject bonobo;
    [SerializeField] float speed = 10;
    [SerializeField] int damage = 30;
    [SerializeField] float cooldown = 6;

    [NonSerialized] bool recallAvailable = true;
    [NonSerialized] bool runTowardsBonobo = false;
    [NonSerialized] PlayerScript bonoboScript;
    [NonSerialized] GorillaScript gorillaScript;
    [NonSerialized] BoxCollider2D hurtCollider;
    [NonSerialized] Rigidbody2D rb;
    [NonSerialized] BonoboThrow bonoboThrow;
    [NonSerialized] GorillaCharge gorillaCharge;
    [NonSerialized] Animator gorillaAnim;
    [NonSerialized] private FollowAI fAI;

    void Start()
    {
        gorillaAnim = GetComponent<Animator>();
        bonoboThrow = bonobo.GetComponent<BonoboThrow>();
        gorillaCharge = GetComponent<GorillaCharge>();
        gorillaScript = GetComponent<GorillaScript>();
        hurtCollider = GetComponent<BoxCollider2D>();
        bonoboScript = bonobo.GetComponent<PlayerScript>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        fAI = GetComponent<FollowAI>();
    }

    void Update()
    {
        //upon pressing 1 or 2 and having the duo be seperated, gorilla starts running towards bonobo
        if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2)) && !bonoboScript.isOnGorilla)
        {
            //makes sure that if the gorilla is charging it doesnt instantly snap them back together
            if (gorillaCharge.isCharging) return;
            if (bonoboThrow.isThrown) return;
            if (!recallAvailable) return;

            fAI.enabled = false;
            runTowardsBonobo = true;
        }

        //when they are on top of one another
        //(and also if the recall has been cast, so that they dont accidentally stick together when they come close)
        if (runTowardsBonobo && Vector2.Distance(gameObject.transform.position, bonobo.transform.position) <= 0.7)
        {
            fAI.enabled = false;
            bonoboScript.AttachDuo();
            runTowardsBonobo = false;
        }
    }

    void FixedUpdate()
    {
        //starts moving only when called
        if (!runTowardsBonobo) return;
        Vector3 targetDirection = (bonobo.transform.position - transform.position).normalized;
        gorillaAnim.SetFloat("Horizontal", targetDirection.x);
        gorillaAnim.SetFloat("Vertical", targetDirection.y);
        gorillaAnim.SetFloat("Speed", 0.2f);
        gameObject.transform.position = Vector2.MoveTowards(rb.position, bonobo.transform.position, speed * Time.deltaTime);
    }

    //damage enemies along the path of the gorilla rushing towards the bonobo
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (!runTowardsBonobo) return;
        if (hit.tag == "Enemy" || hit.tag == "Destructable" || hit.tag == "Obstacle")
        {
            HealthScript enemy = hit.GetComponent<HealthScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    public IEnumerator StartCooldown()
    {
        recallAvailable = false;
        yield return new WaitForSeconds(cooldown);
        recallAvailable = true;
    }
}
