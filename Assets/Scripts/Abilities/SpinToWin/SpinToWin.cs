using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinToWin : MonoBehaviour
{
    [SerializeField] private GameObject spinArea;
    [SerializeField] private GameObject bonobo;
    [SerializeField] private Transform pivotPoint;
    [Space(15)]

    [SerializeField] public int damagePerRotation = 10;
    [SerializeField] public float duration = 2f;
    [SerializeField] public int rotations = 5;
    [SerializeField] public float cooldown = 5f;
    [Space(15)]

    [SerializeField] private AudioClip sound;
    [NonSerialized] private AudioSource audioSource;

    [NonSerialized] private bool isSpinning = false;
    [NonSerialized] private bool spinAvailable = true;
    [NonSerialized] GorillaScript gorillaScript;
    [NonSerialized] BoxCollider2D spinCollider;
    [NonSerialized] Renderer spinRenderer;
    [NonSerialized] Renderer bonoboRenderer;
    [NonSerialized] Animator gorillaAnimator;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bonoboRenderer = bonobo.GetComponent<Renderer>();
        gorillaScript = GetComponent<GorillaScript>();
        spinCollider = spinArea.GetComponent<BoxCollider2D>();
        spinRenderer = spinArea.GetComponent<Renderer>();
        gorillaAnimator = gorillaScript.anim;
        spinCollider.enabled = false;
        spinRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && gorillaScript.apeAttached)
        {
            if (!spinAvailable) return;
            audioSource.PlayOneShot(sound);
            StartCoroutine(StartSpinToWin());
            StartCoroutine(StartSpinCooldown());
        }

        if (isSpinning)
        {
            spinArea.transform.Rotate(Vector3.forward * (90f * rotations) * Time.deltaTime, Space.Self);
        }
    }

    public IEnumerator StartSpinToWin()
    {
        gorillaAnimator.SetBool("SpinToWin", true);
        bonoboRenderer.enabled = false;
        isSpinning = true;
        spinCollider.enabled = true;
        spinRenderer.enabled = true;
        yield return new WaitForSeconds(duration);
        spinCollider.enabled = false;
        spinRenderer.enabled = false;
        isSpinning = false;
        bonoboRenderer.enabled = true;
        gorillaAnimator.SetBool("SpinToWin", false);
    }
    public IEnumerator StartSpinCooldown()
    {
        spinAvailable = false;
        yield return new WaitForSeconds(cooldown);
        spinAvailable = true;
    }
}
