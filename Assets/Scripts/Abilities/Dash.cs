using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dash : MonoBehaviour
{

    //Properties for dash
    [SerializeField] float length = 0.5f;
    [SerializeField] float cooldown = 1;
    [SerializeField] float speed = 40;
    [Space(15)]

    [SerializeField] private AudioClip dashAudioClip;
    [NonSerialized] private AudioSource audioSource;

    [NonSerialized] float dashCounter;
    [NonSerialized] Vector2 move;
    [NonSerialized] PlayerScript bonoboScript;
    [NonSerialized] Animator bonoboAnimator;
    [NonSerialized] bool isOnCooldown = false;
    [NonSerialized] bool cantDash = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bonoboScript = GetComponent<PlayerScript>();
        bonoboAnimator = bonoboScript.anim;
    }

    void Update()
    {
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Mouse1) && !bonoboScript.isOnGorilla)
        {
            if (move.x == 0 && move.y == 0) return;
            if (cantDash || isOnCooldown) return;
            bonoboAnimator.SetBool("Spin", true);
            bonoboScript.activeMoveSpeed = speed;
            dashCounter = length;
            bonoboScript.isInvincible = true;
            audioSource.PlayOneShot(dashAudioClip);
            StartCoroutine(DashCooldown());
            StartCoroutine(DashDuration());
        }

        if (!cantDash)
        {
            bonoboScript.activeMoveSpeed = bonoboScript.bMoveSpeed;
        }
    }
    public IEnumerator DashDuration()
    {
        cantDash = true;
        yield return new WaitForSeconds(length);
        cantDash = false;
        bonoboScript.isInvincible = false;
        bonoboAnimator.SetBool("Spin", false);
    }
    public IEnumerator DashCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }
}