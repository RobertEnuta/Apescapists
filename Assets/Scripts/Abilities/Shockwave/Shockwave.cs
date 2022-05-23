using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    [SerializeField] GameObject bonobo;
    [SerializeField] GameObject shockwave;
    [SerializeField] float speed = 10;
    [SerializeField] float damage = 20;
    [SerializeField] float range = 1;
    [SerializeField] public float cooldown = 5;
    [NonSerialized] PlayerScript bonoboScript;
    [NonSerialized] bool shockwaveUsed = false;

    void Start()
    {
        bonoboScript = bonobo.GetComponent<PlayerScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && bonoboScript.isOnGorilla)
        {
            if (shockwaveUsed) return;
            Wave();
        }
    }

    void Wave()
    {
        Vector2 lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        GameObject shockwaveInstance = Instantiate(shockwave, bonobo.transform.position, Quaternion.Euler(0f, 0f, lookAngle));
        shockwaveInstance.GetComponent<Rigidbody2D>().velocity = (shockwaveInstance.transform.right * speed); //applies velocity to wave
        StartCoroutine(WaitToDestroy(shockwaveInstance));
        StartCoroutine(ShockwaveCooldown());
    }

    public IEnumerator ShockwaveCooldown()
    {
        shockwaveUsed = true;
        yield return new WaitForSeconds(cooldown);
        shockwaveUsed = false;
    }

    public IEnumerator WaitToDestroy(GameObject shockwave)
    {
        float wait = shockwave.GetComponent<StunEnemy>().duration;
        yield return new WaitForSeconds(wait + 0.01f);
        Destroy(shockwave, range); //destroys the shockwave after some seconds
    }
}
