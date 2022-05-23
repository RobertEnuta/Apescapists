using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatStrike : MonoBehaviour
{
    [SerializeField] private GameObject bat;
    [SerializeField] private Transform batPivot;
    [SerializeField] private GameObject gun;
    [Space(15)]
    
    [SerializeField] private float duration = 0.2f;
    [SerializeField] public float cooldown = 2f;
    [SerializeField] public int damage = 20;
    [Space(15)]

    [SerializeField] private AudioClip audioClip;
    [NonSerialized] private AudioSource audioSource;

    [NonSerialized] Animator bonoboAnimator;
    [NonSerialized] private bool batAvailable = true;

    [NonSerialized] PlayerScript playerScript;
    [NonSerialized] BoxCollider2D boxCollider;
    [NonSerialized] private float lookAngle;
    [NonSerialized] private Vector2 lookDirection;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bat.GetComponent<Renderer>().enabled = false;
        playerScript = this.GetComponent<PlayerScript>();
        boxCollider = bat.GetComponent<BoxCollider2D>();
        bonoboAnimator = playerScript.anim;
    }

    // Update is called once per frame
    void Update()
    {
        RotateBat();
        //bonobo can strike whenever not on gorilla
        if (Input.GetKeyDown(KeyCode.Alpha4) && !playerScript.isOnGorilla)
        {
            if (!batAvailable) return;

            audioSource.PlayOneShot(audioClip);
            UseBatStrike();
        }
    }

    private void RotateBat()
    {
        if (!batAvailable) return;

        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        batPivot.rotation = Quaternion.Euler(0f, 0f, lookAngle);
    }

    void UseBatStrike()
    {
        StartCoroutine(playerScript.HaltMovement(duration));
        StartCoroutine(StartBatCooldown());
        StartCoroutine(EnableBat());
        bonoboAnimator.SetTrigger("BatStrike");
    }

    public IEnumerator StartBatCooldown()
    {
        batAvailable = false;
        yield return new WaitForSeconds(cooldown);
        batAvailable = true;
    }
    public IEnumerator EnableBat()
    {
        //disables gun for duration of bat usage
        gun.GetComponent<Renderer>().enabled = false;
        gun.GetComponent<Weapon>().enabled = false;

        boxCollider.enabled = true;
        bat.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(duration);
        boxCollider.enabled = false;
        bat.GetComponent<Renderer>().enabled = false;

        //enables gun again
        gun.GetComponent<Renderer>().enabled = true;
        gun.GetComponent<Weapon>().enabled = true;
    }
}
