using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    //what bullet is being used and where its spawned
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform gunPivot;
    [SerializeField] private ParticleSystem muzzleParticle;
    [SerializeField] private Camera mainCamera;
    [NonSerialized] private SpriteRenderer renderer;
    [Space(15)]

    //bool for if you can hold down lmb and shoot, or it has to be pressed each time
    [SerializeField] private bool automatic = true;
    [Space(15)]

    //cooldown variables for how often the gun can shoot and reload cooldown
    [SerializeField] private float shotCooldown = 0.3f;
    [SerializeField] private float reloadTime = 2f;
    [NonSerialized] private float shotCooldownTracker;
    [NonSerialized] private float reloadTimer = 0f;
    [NonSerialized] private bool reloading = false;
    [NonSerialized] private bool canShoot = true;
    [Space(15)]

    //magazine related attributes
    [SerializeField] private int magazineSize = 6;
    [SerializeField] private int currentBulletCount = 6;
    [Space(15)]

    //number of bullets and their spread
    [SerializeField] private int bulletsPerShot = 1;
    [SerializeField] private float spreadAngle = 1f;
    [Space(15)]

    //velocity and range
    [SerializeField] private float bulletVelocity = 20f;
    [SerializeField] private float range = 2f;

    //camera shake duration on magnitude
    [SerializeField] private float cameraShakeDuration = 0.3f;
    [SerializeField] private float cameraShakeMagnitude = 0.7f;

    //constant for directional shooting
    [NonSerialized] private Vector2 lookDirection;
    [NonSerialized] private float lookAngle;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        RotateWeapon();

        //takes care of cooldown between individiual shots
        if (shotCooldownTracker >= 0)
        {
            shotCooldownTracker -= Time.deltaTime;
            canShoot = false;
        }
        else
        {
            canShoot = true;
        }

        //checks if the gun is still reloading, if it is- it can't shoot
        if (reloading == true)
        {
            reloadTimer -= Time.deltaTime;
            canShoot = false;

            if (reloadTimer <= 0)
            {
                canShoot = true;
                reloading = false;
            }
        }

        //reloads on pressing "R"
        if (Input.GetKey("r"))
        {
            Reload();
        }

        //reloads when gun runs out of bullets
        if (currentBulletCount == 0)
        {
            Reload();
        }

        //shooting on hold lmb or on clicking only
        if (automatic)
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    private void RotateWeapon()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        gunPivot.rotation = Quaternion.Euler(0f, 0f, lookAngle);

        if (lookDirection.normalized.x < 0)
        {
            renderer.flipY = true;
        }
        else renderer.flipY = false;
    }

    void Shoot()
    {
        if (!canShoot) return;
        shotCooldownTracker = shotCooldown;
        this.currentBulletCount -= 1;

        //muzzle particles
        muzzleParticle.Stop();
        muzzleParticle.Play();

        //camera shaking
        mainCamera.GetComponent<ShakeBehaviour>().TriggerShake(cameraShakeDuration, cameraShakeMagnitude);

        //shoot x bullets at a time 
        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.transform.rotation = Quaternion.RotateTowards(bullet.transform.rotation, Random.rotation, spreadAngle); //makes bullets spread
            bullet.GetComponent<Rigidbody2D>().velocity = (bullet.transform.right * bulletVelocity); //applies velocity to bullets
            Destroy(bullet, range); //destroys the bullet if it gets out of the range of the weapon
        }
    }


    //resets bullet to max in mag, and triggers the reload timer
    void Reload()
    {
        reloading = true;
        reloadTimer = reloadTime;
        currentBulletCount = magazineSize;
    }

    public int CurrentBullets()
    {
        return currentBulletCount;
    }

    public float UIReloadExposeTime()
    {
        return reloadTime;
    }
    public float UIReloadExposeTimer()
    {
        return reloadTimer;
    }
    public bool UIReloadExposeReloading()
    {
        return reloading;
    }
    public int MaxBullets()
    {
        return magazineSize;
    }
}
