using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogAttackAI : MonoBehaviour
{
    [SerializeField] GameObject quill;
    [SerializeField] private float quillVelocity = 10f;
    [SerializeField] private float shotCooldown = 1.5f;

    private bool onCooldown = false;
    private bool canShoot = true;
    Vector3 previousPosition;
    float positionTimer = 1f; //how often should the check if the hedgehog is moving is made
    float currentTimer;
    void Start()
    {
        currentTimer = positionTimer;
    }

    void Update()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer<=0)
        {
            currentTimer = positionTimer;
            previousPosition = transform.position;
        }
    }
    private void FixedUpdate()
    {

        //checks if the hedgehog is moving, if it isn it doesn't shoot
        if (transform.position == previousPosition)
        {
            ShootQuills();
        }
    }

    void ShootQuills()
    {
        if (!canShoot || onCooldown) return;
        Quaternion rotation = gameObject.transform.rotation;

        for (int i = 0; i < 8; i++)
        {
            rotation *= Quaternion.Euler(0, 0, 45);
            GameObject quillInstance = Instantiate(quill, gameObject.transform.position, rotation);
            quillInstance.GetComponent<Rigidbody2D>().velocity = (quillInstance.transform.right * quillVelocity);
        }
        StartCoroutine(QuillCooldown());
    }

    private IEnumerator QuillCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(shotCooldown);
        onCooldown = false;
    }
}
