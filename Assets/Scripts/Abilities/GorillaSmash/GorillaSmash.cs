using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaSmash : MonoBehaviour
{

    [SerializeField] private GameObject smashArea;
    [SerializeField] private Transform pivotPoint;
    [Space(15)]

    [SerializeField] private float duration = 0.2f;
    [SerializeField] private float cooldown = 0.3f;
    [SerializeField] public int damage = 20;
    [Space(15)]

    [SerializeField] AudioClip[] sounds;
    [NonSerialized] AudioSource source;

    [NonSerialized] private bool smashAvailable = true;

    [NonSerialized] private Vector2 lookDirection;
    [NonSerialized] private float lookAngle;
    [NonSerialized] CircleCollider2D smashCollider;
    [NonSerialized] GorillaScript gorillaScript;
    [NonSerialized] Renderer smashRenderer;

    void Start()
    {
        gorillaScript = GetComponent<GorillaScript>();
        smashCollider = smashArea.GetComponent<CircleCollider2D>();
        smashRenderer = smashArea.GetComponent<Renderer>();
        smashCollider.enabled = false;
        smashRenderer.enabled = false;
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        RotateSmashArea();
        if (Input.GetKeyDown(KeyCode.Mouse1) && gorillaScript.apeAttached)
        {
            Smash();
        }
    }

    private void RotateSmashArea()
    {
        if (!smashAvailable) return;
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        pivotPoint.rotation = Quaternion.Euler(0f, 0f, lookAngle);
    }

    private void RotateSmashArea(Vector3 target)
    {
        if (!smashAvailable) return;
        lookDirection = target - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        pivotPoint.rotation = Quaternion.Euler(0f, 0f, lookAngle);
    }

    private void Smash()
    {
        if (!smashAvailable) return;
        PlaySmashSFX();
        StartCoroutine(gorillaScript.HaltMovement(duration));
        StartCoroutine(StartSmashCooldown());
        StartCoroutine(EnableSmashArea());
    }

    private void Smash(Vector3 target)
    {
        RotateSmashArea(target);
        if (!smashAvailable) return;
        PlaySmashSFX();
        StartCoroutine(gorillaScript.HaltMovement(duration));
        StartCoroutine(StartSmashCooldown());
        StartCoroutine(EnableSmashArea());
    }

    public IEnumerator StartSmashCooldown()
    {
        smashAvailable = false;
        yield return new WaitForSeconds(cooldown);
        smashAvailable = true;
    }
    public IEnumerator EnableSmashArea()
    {
        smashCollider.enabled = true;
        smashRenderer.enabled = true;
        yield return new WaitForSeconds(duration);
        smashCollider.enabled = false;
        smashRenderer.enabled = false;
    }

    public void AISmash(Vector3 target)
    {
        Smash(target);
    }

    private void PlaySmashSFX()
    {
        source.clip = sounds[UnityEngine.Random.Range(0, sounds.Length)];
        source.Play();
    }
}
