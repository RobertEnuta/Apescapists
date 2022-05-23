using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] GameObject gorilla;
    [SerializeField] public Animator anim;
    Rigidbody2D rb;
    BoxCollider2D bonoboBoxCollider;
    GorillaScript gorillaScript;
    [Space(15)]

    //Properties for Bonobo 
    [SerializeField] public float bonoboHealth = 25;
    [SerializeField] public float bMoveSpeed = 20;
    [NonSerialized] public float activeMoveSpeed;
    [Space(15)]

    //checks if the duo is together
    [SerializeField] public bool isOnGorilla;

    //while rolling bonobo can't take damage
    [NonSerialized] public bool isInvincible = false;

    //stops movement for casting abilities
    private bool haltMovement;

    [NonSerialized] public Vector2 move;
    private Vector2 moveDirection;
    private Vector2 lookDirection;

    //audio variables
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] bonoboFootstepSounds;
    private float walkPitch;
    private float footStepTimer = 0.3f;
    private float currentFootStepTimer;

    void Start()
    {
        currentFootStepTimer = footStepTimer;
        gorillaScript = gorilla.GetComponent<GorillaScript>();
        rb = GetComponent<Rigidbody2D>();
        bonoboBoxCollider = GetComponent<BoxCollider2D>();
        activeMoveSpeed = bMoveSpeed;
        //audioSource = GetComponent<AudioSource>();
        walkPitch = UnityEngine.Random.Range(0.5f, 1.5f);
    }

    void Update()
    {
        ProcessInputs();
        GorillaCheck();
    }
    private void ProcessInputs()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveDirection = new Vector2(
            Mathf.Clamp(move.x, -Mathf.Abs(move.normalized.x), Mathf.Abs(move.normalized.x)),
            Mathf.Clamp(move.y, -Mathf.Abs(move.normalized.y), Mathf.Abs(move.normalized.y))
        );

        anim.SetFloat("Horizontal", move.x);
        anim.SetFloat("Vertical", move.y);
        anim.SetFloat("Speed", move.sqrMagnitude);
    }

    void FixedUpdate()
    {
        PlayFootStepAudio();
        Move();
    }

    private void Move()
    {
        if (haltMovement) return;
        if (isOnGorilla) return;
        rb.MovePosition(transform.position + (Vector3)moveDirection * activeMoveSpeed * Time.deltaTime);
    }

    private void GorillaCheck()
    {
        if (isOnGorilla)
        {
            bonoboBoxCollider.isTrigger = true;
            gorilla.GetComponent<BoxCollider2D>().isTrigger = false;
            // isInvincible = true;
            this.transform.position = gorilla.gameObject.transform.position;
            gorillaScript.apeAttached = true;
        }
        else
        {
            bonoboBoxCollider.isTrigger = false;
            gorilla.GetComponent<BoxCollider2D>().isTrigger = true;
            // isInvincible = false;
            gorillaScript.apeAttached = false;
        }
    }

    //take damage methods need to be revised in both playerscript and gorillascript
    public void TakeDamage(int damage)
    {
        if (isInvincible) return;
        if (isOnGorilla)
        {
            gorillaScript.TakeDamage(damage);
        }
        else
        {
            this.bonoboHealth -= damage;
        }
        if (bonoboHealth <= 0)
        {
            //Debug.Log("Game Over");
        }
    }

    public void AttachDuo()
    {
        this.isOnGorilla = true;
    }
    internal void DetachDuo()
    {
        gorilla.GetComponent<GorillaScript>().anim.SetFloat("Horizontal", 0);
        gorilla.GetComponent<GorillaScript>().anim.SetFloat("Vertical", 0);
        gorilla.GetComponent<GorillaScript>().anim.SetFloat("Speed", 0);
        this.isOnGorilla = false;
    }

    //halts the movement of the bonobo while an ability is cast
    public IEnumerator HaltMovement(float haltTime)
    {
        haltMovement = true;
        yield return new WaitForSeconds(haltTime);
        haltMovement = false;
    }

    public void Heal(int hp)
    {
        if(bonoboHealth + hp <= 45)
        {
            bonoboHealth += hp;
        }
    }
    private void PlayFootStepAudio()
    {
        if (currentFootStepTimer > 0)
        {
            currentFootStepTimer -= Time.deltaTime;
            return;
        }

        if (isOnGorilla) return;
        if (move.x <= 0.05 && move.y <= 0.05) return;
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = UnityEngine.Random.Range(1, bonoboFootstepSounds.Length);
        audioSource.clip = bonoboFootstepSounds[n];
        //add walkPitch value here
        audioSource.pitch = walkPitch;
        audioSource.PlayOneShot(audioSource.clip);
        walkPitch = UnityEngine.Random.Range(0.5f, 1.5f);
        // move picked sound to index 0 so it's not picked next time
        bonoboFootstepSounds[n] = bonoboFootstepSounds[0];
        bonoboFootstepSounds[0] = audioSource.clip;
        currentFootStepTimer = footStepTimer;

    }
}