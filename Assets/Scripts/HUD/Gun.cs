using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bonobo;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject bullets;
    Weapon gunScript;
    private List<Image> bulletImages = new List<Image>();
    Animator animator;
    //reload time
    private float reloadTime = 2f;
    private float reloadTimer = 0f;
    private bool reloading = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = gun.GetComponent<Animator>();
        gunScript = bonobo.GetComponentInChildren<Weapon>();
        foreach (Image bull in bullets.GetComponentsInChildren<Image>())
        {
            bulletImages.Add(bull);
        }
        TimerValues();
    }

    // Update is called once per frame
    void Update()
    {
        //fire animation
        if (Input.GetButton("Fire1"))
        {
            bullets.GetComponent<BulletCount>().UIUpdateBullets(gunScript.CurrentBullets());
            if (gunScript.CurrentBullets() > 0)
            {
                animator.SetTrigger("FireTrigger");
            }
        }
        //reloads on pressing "R"
        if (Input.GetKey("r") || gunScript.CurrentBullets() == 0)
        {
            UIReload();
            animator.SetBool("ReloadBool", true);
        }

        //checks if the gun is still reloading, if it is- it can't shoot
        if (reloading == true)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            {
                reloading = false;
                bullets.GetComponent<BulletCount>().UIReloadBullets();
                animator.SetBool("ReloadBool", false);
            }
        }

    }

    //resets bullet to max in mag, and triggers the reload timer
    void UIReload()
    {
        reloading = true;
        reloadTimer = reloadTime;
        animator.SetBool("ReloadBool", true);
    }

    //update the values for the timer of the reload
    void TimerValues()
    {
        reloadTime = gunScript.UIReloadExposeTime();
        reloadTimer = gunScript.UIReloadExposeTimer();
        reloading = gunScript.UIReloadExposeReloading();
    }
}
