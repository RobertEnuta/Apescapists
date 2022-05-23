using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaScript : MonoBehaviour
{

    private BoxCollider2D hurtCollider;
    private Rigidbody2D rb;
    [SerializeField] public float gorillaHealth = 75;
    [SerializeField] public float gorillaMoveSpeed = 15;
    [NonSerialized] public bool apeAttached;
    [NonSerialized] public Vector2 move;
    [NonSerialized] public Vector2 moveDirection;
    [SerializeField] public Animator anim;
    //stops movement for casting abilities
    private bool haltMovement;
    private FollowAI fAI;
    [Space(15)]

    //audio variables
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] gorillaFootstepSounds;
    private float walkPitch;
    private float footStepTimer = 0.3f;
    private float currentFootStepTimer;

    //checks if gorilla is moving or not
    Vector3 previousPosition;
    float positionTimer = 1f;
    float currentPositionTimer;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        hurtCollider = gameObject.GetComponent<BoxCollider2D>();
        fAI = GetComponent<FollowAI>();
        currentPositionTimer = positionTimer;
    }
    bool dead = false;
    void Update()
    {
        ProcessInputs();
        ProcessHealth();
        CheckMovement();
        if(gorillaHealth < 0)
        {
            gorillaHealth = 0;
        }
    }

    private void CheckMovement()
    {
        currentPositionTimer -= Time.deltaTime;
        if (currentPositionTimer <= 0)
        {
            currentPositionTimer = positionTimer;
            previousPosition = transform.position;
        }
    }

    private void ProcessHealth()
    {
        if (gorillaHealth <= 0)
        {
            dead = true;

            gameObject.GetComponent<FollowAI>().enabled = false;
            gameObject.GetComponent<GorillaCharge>().enabled = false;
            gameObject.GetComponent<RecallToBonobo>().enabled = false;
        }
        if (dead = true && gorillaHealth > 0)
        {
            dead = false;
            gameObject.GetComponent<FollowAI>().enabled = true;
            gameObject.GetComponent<GorillaCharge>().enabled = true;
            gameObject.GetComponent<RecallToBonobo>().enabled = true;
        }
    }
    private void ProcessInputs()
    {
        if (apeAttached == true)
        {
            move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            moveDirection = new Vector2(
                Mathf.Clamp(move.x, -Mathf.Abs(move.normalized.x), Mathf.Abs(move.normalized.x)),
                Mathf.Clamp(move.y, -Mathf.Abs(move.normalized.y), Mathf.Abs(move.normalized.y)));
            fAI.enabled = false;//disables chase script
            anim.SetFloat("Horizontal", move.x);
            anim.SetFloat("Vertical", move.y);
            anim.SetFloat("Speed", move.sqrMagnitude);
        }
    }

    void FixedUpdate()
    {
        Move();
        PlayFootStepAudio();
    }

    private void Move()
    {
        if (haltMovement) return;
        rb.MovePosition(rb.position + moveDirection * gorillaMoveSpeed * Time.fixedDeltaTime);
    }

    //halts the movement of the gorilla while an ability is cast
    public IEnumerator HaltMovement(float haltTime)
    {
        haltMovement = true;
        yield return new WaitForSeconds(haltTime);
        haltMovement = false;
        fAI.enabled = true;
    }

    //probably needs to be revised along with playerscript takedamage
    public void TakeDamage(int damage)
    {
        gorillaHealth -= damage;
    }

    public void Heal(int hp)
    {
        if (gorillaHealth + hp <= 100)
        {
            gorillaHealth += hp;
        }
    }

    private void PlayFootStepAudio()
    {
        if (currentFootStepTimer > 0)
        {
            currentFootStepTimer -= Time.deltaTime;
            return;
        }

        if (transform.position == previousPosition) return;

        int n = UnityEngine.Random.Range(1, gorillaFootstepSounds.Length);
        audioSource.clip = gorillaFootstepSounds[n];
        audioSource.pitch = walkPitch;
        audioSource.PlayOneShot(audioSource.clip);
        walkPitch = UnityEngine.Random.Range(0.5f, 1.5f);
        gorillaFootstepSounds[n] = gorillaFootstepSounds[0];
        gorillaFootstepSounds[0] = audioSource.clip;
        currentFootStepTimer = footStepTimer;
    }

}
