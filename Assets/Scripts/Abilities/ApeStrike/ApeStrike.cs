using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApeStrike : MonoBehaviour
{
    [SerializeField] private GameObject apeStrikeArea;
    [SerializeField] private GameObject gun;
    [Space(15)]

    [SerializeField] float initialDashSpeed = 20f;
    [SerializeField] float initialDashRange = 0.5f;
    [Space(15)]

    [SerializeField] float apeStrikeDuration = 0.2f;
    [SerializeField] public float cooldown = 5f;
    [SerializeField] public int damage = 20;
    [SerializeField] public float timeInBetweenHits = 0.04f;
    [Space(15)]

    [SerializeField] private AudioClip initialDashSound;
    [NonSerialized] private AudioSource audioSource;

    [NonSerialized] Animator anim;
    [NonSerialized] public bool isUsingApeStrike = false;
    [NonSerialized] public bool stopInitialDash = false;
    [NonSerialized] public bool apeStrikeAvailable = true;

    [NonSerialized] BoxCollider2D bonoboCollider;
    [NonSerialized] CircleCollider2D apeStrikeAreaCollider;
    [NonSerialized] Renderer bonoboRenderer;
    [NonSerialized] Renderer apeStrikeAreaRenderer;
    [NonSerialized] PlayerScript bonoboScript;
    [NonSerialized] Vector3 targetPos;

    void Start()
    {
        bonoboCollider = GetComponent<BoxCollider2D>();
        bonoboRenderer = GetComponent<Renderer>();
        apeStrikeAreaCollider = apeStrikeArea.GetComponent<CircleCollider2D>();
        apeStrikeAreaRenderer= apeStrikeArea.GetComponent<Renderer>();
        bonoboScript = gameObject.GetComponent<PlayerScript>();
        apeStrikeAreaCollider.enabled = false;
        apeStrikeAreaRenderer.enabled = false;
        anim = bonoboScript.anim;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && !bonoboScript.isOnGorilla)
        {
            if (!apeStrikeAvailable) return;
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos = transform.position + (targetPos - transform.position).normalized * 1000.0f;
            isUsingApeStrike = true;
            audioSource.PlayOneShot(initialDashSound);
            StartCoroutine(InitialDashRange());
            StartCoroutine(StartApeStrikeCooldown());
        }
        if (isUsingApeStrike)
        {
            bonoboScript.move = Vector2.zero;
            targetPos.z = transform.position.z;
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPos, initialDashSpeed * Time.deltaTime);
            if (stopInitialDash)
            {
                StartCoroutine(bonoboScript.HaltMovement(apeStrikeDuration));
                StartCoroutine(StartApeStrike());
                isUsingApeStrike = false;
            }
        }

    }
    public IEnumerator InitialDashRange()
    {
        anim.SetBool("Spin", true);
        bonoboScript.isInvincible = true;
        gun.GetComponent<Renderer>().enabled = false;
        gun.GetComponent<Weapon>().enabled = false;
        stopInitialDash = false;
        yield return new WaitForSeconds(initialDashRange);
        bonoboScript.isInvincible = false;
        stopInitialDash = true;
        anim.SetBool("Spin", false);
    }

    private IEnumerator StartApeStrike()
    {
        anim.SetBool("ApeStrike", true);
        bonoboCollider.enabled = false;
        bonoboRenderer.enabled = false;
        apeStrikeAreaCollider.enabled = true;
        apeStrikeAreaRenderer.enabled = true;
        apeStrikeArea.GetComponent<ApeStrikeAreaDamage>().enabled = true;
        yield return new WaitForSeconds(apeStrikeDuration);
        apeStrikeArea.GetComponent<ApeStrikeAreaDamage>().enabled = false;
        bonoboCollider.enabled = true;
        bonoboRenderer.enabled = true;
        apeStrikeAreaCollider.enabled = false;
        apeStrikeAreaRenderer.enabled = false;
        gun.GetComponent<Renderer>().enabled = true;
        gun.GetComponent<Weapon>().enabled = true;
        anim.SetBool("ApeStrike", false);
        StartCoroutine(RunAway());
    }


    public IEnumerator StartApeStrikeCooldown()
    {
        apeStrikeAvailable = false;
        yield return new WaitForSeconds(cooldown);
        apeStrikeAvailable = true;
    }

    public IEnumerator RunAway()
    {
        bonoboScript.isInvincible = true;
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        spr.color = new Color(1f,1f,1f,0.5f);
        yield return new WaitForSeconds(1.5f);
        bonoboScript.isInvincible = false;
        spr.color = new Color(1f,1f,1f,1f);
        
        

    }
}
