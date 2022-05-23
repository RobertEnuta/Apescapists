using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayers : MonoBehaviour
{
    
    [SerializeField] GameObject bonobo;
    [SerializeField] GameObject gorilla;

    private void FixedUpdate() 
    {
        if(gameObject.GetComponentInChildren<Door>() == null)
        {
            bonobo.GetComponent<PlayerScript>().Heal(5);
            //if gorrila is death revive with 30 hp
            if(gorilla.GetComponent<GorillaScript>().gorillaHealth <= 0)
            {
                gorilla.GetComponent<GorillaScript>().gorillaHealth = 20;
            } else 
            {
                gorilla.GetComponent<GorillaScript>().Heal(10);
            }
            gameObject.GetComponent<HealPlayers>().enabled = false;
        }    

    }
}
