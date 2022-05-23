using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAI : MonoBehaviour
{
    [SerializeField] float minDistance = 2;
    [SerializeField] float maxDistance = 15;
    [SerializeField] float moveSpeed = 10;
    //what tag to follow
    [SerializeField] string tag = "Player";
    [SerializeField] Animator anim;

    private GameObject[] target;
    //keeps count if the target has been in the vision range recently
    private bool targetSeen = false;

    AttackAI aAI;

    private void Start()
    {
        aAI = gameObject.GetComponent<AttackAI>();
    }

    private void Update()
    {
        if (gameObject.tag == "Enemy")
        {
            if (gameObject.GetComponent<HealthScript>().GetHealth() > 0)
            {
                FollowTarget();
            }
        } 
        else 
        {
            FollowTarget();
        }
    }

    private int targetNr = -1;
    private int prevLenght;
    private void FollowTarget()
    {
        FindTag(tag);
        if (target.Length > 0)
        {
            if (targetNr < 0 || target.Length < prevLenght)
            {
                SelectTarget();
                prevLenght = target.Length;
            }
            if (targetNr <= target.Length && targetNr >= 0)
            {
                if (target[targetNr] != null)
                {
                    //if gorrila is targeted but duo is attached select a different target
                    if (target[targetNr].name == "Gorilla" && (DuoAttached() || target[targetNr].GetComponent<GorillaScript>().gorillaHealth <= 0))
                    {
                        SelectTarget();
                    }
                    //if target is in range stop searching for enemies
                    foreach (GameObject item in target)
                    {
                        if (Vector2.Distance(transform.position, target[targetNr].transform.position) < maxDistance)
                        {
                            break;
                        }
                        targetNr = Random.Range(0, target.Length);
                    }
                    //make sure the target is in view
                    if (Vector2.Distance(transform.position, target[targetNr].transform.position) > minDistance && Vector2.Distance(transform.position, target[targetNr].transform.position) < maxDistance)
                    {

                        AnimateMoving();
                        transform.position = Vector2.MoveTowards(transform.position, target[targetNr].transform.position, moveSpeed * Time.deltaTime);
                        if (targetSeen == false)
                        {
                            targetSeen = true;
                        }
                    }
                    else
                    {
                        StartCoroutine(LoseTarget());
                        if (targetSeen && Vector2.Distance(transform.position, target[targetNr].transform.position) > minDistance)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, target[targetNr].transform.position, moveSpeed * Time.deltaTime);
                        }
                        if (Vector2.Distance(transform.position, target[targetNr].transform.position) <= minDistance)
                        {
                            if (target[targetNr].tag != "Player")
                            {
                                gameObject.GetComponent<GorillaSmash>().AISmash(target[targetNr].transform.position);
                            }
                            else
                            {
                                if(aAI != null)
                                {
                                    aAI.Attack(target[targetNr]);
                                }
                            }
                        }

                    }
                }
            }
        }

    }

    private void AnimateMoving()
    {
        if (target[targetNr] != null)
        {
            Vector3 targetDirection = (target[targetNr].transform.position - transform.position).normalized;
            anim.SetFloat("Horizontal", targetDirection.x);
            anim.SetFloat("Vertical", targetDirection.y);
            anim.SetFloat("Speed", 0.2f);
        }
        if (target[targetNr] == null)
        {
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Vertical", 0);
            anim.SetFloat("Speed", 0);
        }
    }

    private void FindTag(string tag)
    {
        target = GameObject.FindGameObjectsWithTag(tag);
    }
    //stop chasing after 3 seconds
    public IEnumerator LoseTarget()
    {
        yield return new WaitForSeconds(3f);
        targetSeen = false;
    }
    private void SelectTarget()
    {
        //get a random rn from the list of targets
        targetNr = Random.Range(0, target.Length);
    }
    private bool DuoAttached()
    {
        foreach (GameObject ape in target)
        {
            if (ape.name == "Bonobo")
            {
                if (ape.GetComponent<PlayerScript>().isOnGorilla)
                {
                    return true;
                }

            }
        }
        return false;
    }

    public GameObject[] GetTargets()
    {
        return target;
    }
}
