using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject bonobo;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
        bonobo = GameObject.Find("Bonobo");
    }

    // Update is called once per frame
    void Update()
    {
        if(bonobo.GetComponent<PlayerScript>().bonoboHealth <= 0)
        {
            gameObject.GetComponent<Canvas>().enabled = true;
            GameObject.Find("HUD").GetComponent<Canvas>().enabled = false;
            GameObject.Find("HUD").GetComponentInChildren<AudioSource>().enabled = false;
            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy.GetComponent<FollowAI>().enabled = false;
                enemy.GetComponent<AttackAI>().enabled = false;
            }
            foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if( player.GetComponent<FollowAI>() != null)
                {
                    player.GetComponent<FollowAI>().enabled = false;
                    player.GetComponent<AttackAI>().enabled = false;
                }
            }
            GameObject.Find("MenuCanvas").GetComponent<Resume>().enabled = false;
            GameObject.Find("MenuCanvas").GetComponent<Canvas>().enabled = false;
        }
    }
}
