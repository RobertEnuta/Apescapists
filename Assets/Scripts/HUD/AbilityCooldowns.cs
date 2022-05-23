using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class AbilityCooldowns : MonoBehaviour
{
    [SerializeField] GameObject playerOwner;
    private BatStrike batScript;
    private ApeStrike apeScript;
    [SerializeField] Image cdApe;
    [SerializeField] Image cdBat;
    [SerializeField] Image bgApe;
    [SerializeField] Image bgBat;
    private void Start()
    {
        playerOwner = GameObject.Find("Bonobo");
        batScript = playerOwner.GetComponent<BatStrike>();
        apeScript = playerOwner.GetComponent<ApeStrike>();
        timeApe = apeScript.cooldown;
        timeBat = batScript.cooldown;
            bgBat.GetComponent<Image>().enabled = false;
            cdBat.GetComponent<Image>().enabled = false;
            bgApe.GetComponent<Image>().enabled = false;
            cdApe.GetComponent<Image>().enabled = false;
    }
    private float timeApe;
    private float timeBat;
    private float timerApe;
    private float timerBat;
    private string numberName;
    private string numberName2;
    bool apeCool = false;
    bool batCool = false;
    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Alpha3) && playerOwner.GetComponent<PlayerScript>().isOnGorilla == false && apeCool == false)
        {
            apeCool = true;
            timeApe = apeScript.cooldown;
        }
        if (apeCool)
        {
            bgApe.GetComponent<Image>().enabled = true;
            cdApe.GetComponent<Image>().enabled = true;
            timeApe -= Time.deltaTime;
            if(timeApe <= 0)
            {
                apeCool = false;
            } 
            numberName = ((int)Mathf.Round(timeApe)).ToString();
            cdApe.sprite = Resources.Load<Sprite>("Numbers/" + numberName); 
        }
        if(timeApe <= 0)
        {  
            bgApe.GetComponent<Image>().enabled = false;
            cdApe.GetComponent<Image>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && playerOwner.GetComponent<PlayerScript>().isOnGorilla == false && batCool == false)
        {
            batCool = true;
            timeBat = batScript.cooldown;
        }
        if (batCool)
        {
            bgBat.GetComponent<Image>().enabled = true;
            cdBat.GetComponent<Image>().enabled = true;
            timeBat -= Time.deltaTime;
            if(timeBat <= 0)
            {
                batCool = false;
            } 
            numberName2 = ((int)Mathf.Round(timeBat)).ToString();
            cdBat.sprite = Resources.Load<Sprite>("Numbers/" + numberName2); 
        }
        if(timeBat <= 0)
        {  
            bgBat.GetComponent<Image>().enabled = false;
            cdBat.GetComponent<Image>().enabled = false;
        }
    }



}
