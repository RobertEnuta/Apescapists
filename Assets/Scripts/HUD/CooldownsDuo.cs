using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class CooldownsDuo : MonoBehaviour
{
    [SerializeField] GameObject bonobo;
    [SerializeField] GameObject gorilla;
    private GorillaCharge charScript;
    private BonoboThrow apeScript;
    private Shockwave waveScript;
    private SpinToWin spinScript;

    [SerializeField] Image cdApe;
    [SerializeField] Image cdChar;
    [SerializeField] Image bgApe;
    [SerializeField] Image bgChar;
    [SerializeField] Image cdWave;
    [SerializeField] Image cdSpin;
    [SerializeField] Image bgSpin;
    [SerializeField] Image bgWave;
    private void Start()
    {
        bonobo = GameObject.Find("Bonobo");
        gorilla = GameObject.Find("Gorilla");

        apeScript = bonobo.GetComponent<BonoboThrow>();
        timeApe = apeScript.cooldown;      

        charScript = gorilla.GetComponent<GorillaCharge>();
        timeC = charScript.cooldown;

        spinScript = gorilla.GetComponent<SpinToWin>();
        timeS = charScript.cooldown;

        waveScript = gorilla.GetComponent<Shockwave>();
        timeW = charScript.cooldown;
        // images
            bgChar.GetComponent<Image>().enabled = false;
            cdChar.GetComponent<Image>().enabled = false;

            bgApe.GetComponent<Image>().enabled = false;
            cdApe.GetComponent<Image>().enabled = false;

            bgSpin.GetComponent<Image>().enabled = false;
            cdSpin.GetComponent<Image>().enabled = false;

            bgWave.GetComponent<Image>().enabled = false;
            cdWave.GetComponent<Image>().enabled = false;
    }
    private float timeApe =0f;
    private float timeC=0f;
    private float timeS=0f;
    private float timeW=0f;
    private string numberName;
    private string numberName2;
    private string numberName3;
    private string numberName4;
    bool apeCool = false;
    bool cCool = false;
    bool sCool = false;
    bool wCool = false;
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && bonobo.GetComponent<PlayerScript>().isOnGorilla == true && apeCool == false)
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
        if(timeApe <= 0 || bonobo.GetComponent<PlayerScript>().isOnGorilla != true)
        {  
            bgApe.GetComponent<Image>().enabled = false;
            cdApe.GetComponent<Image>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && bonobo.GetComponent<PlayerScript>().isOnGorilla == true && cCool == false)
        {
            cCool = true;
            timeC = charScript.cooldown;
        }
        if (cCool)
        {
            bgChar.GetComponent<Image>().enabled = true;
            cdChar.GetComponent<Image>().enabled = true;
            timeC -= Time.deltaTime;
            if(timeC <= 0)
            {
                cCool = false;
            } 
            numberName2 = ((int)Mathf.Round(timeC)).ToString();
            cdChar.sprite = Resources.Load<Sprite>("Numbers/" + numberName2); 
        }
        if(timeC <= 0 || bonobo.GetComponent<PlayerScript>().isOnGorilla != true) 
        {  
            bgChar.GetComponent<Image>().enabled = false;
            cdChar.GetComponent<Image>().enabled = false;
        }
        if(timeS <= 0 || bonobo.GetComponent<PlayerScript>().isOnGorilla != true)
        {  
            bgChar.GetComponent<Image>().enabled = false;
            cdChar.GetComponent<Image>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && bonobo.GetComponent<PlayerScript>().isOnGorilla == true && sCool == false)
        {
            sCool = true;
            timeS = spinScript.cooldown;
        }
        if (sCool)
        {
            bgSpin.GetComponent<Image>().enabled = true;
            cdSpin.GetComponent<Image>().enabled = true;
            timeS -= Time.deltaTime;
            if(timeS <= 0)
            {
                sCool = false;
            } 
            numberName3 = ((int)Mathf.Round(timeS)).ToString();
            cdSpin.sprite = Resources.Load<Sprite>("Numbers/" + numberName3); 
        }
        if(timeS <= 0  || bonobo.GetComponent<PlayerScript>().isOnGorilla != true)
        {  
            bgSpin.GetComponent<Image>().enabled = false;
            cdSpin.GetComponent<Image>().enabled = false;
        }
         if (Input.GetKeyDown(KeyCode.Alpha4) && bonobo.GetComponent<PlayerScript>().isOnGorilla == true && wCool == false)
        {
            wCool = true;
            timeW = waveScript.cooldown;
        }
        if (wCool)
        {
            bgWave.GetComponent<Image>().enabled = true;
            cdWave.GetComponent<Image>().enabled = true;
            timeW -= Time.deltaTime;
            if(timeW <= 0)
            {
                wCool = false;
            } 
            numberName4 = ((int)Mathf.Round(timeW)).ToString();
            cdWave.sprite = Resources.Load<Sprite>("Numbers/" + numberName4); 
        }
        if(timeW <= 0 || bonobo.GetComponent<PlayerScript>().isOnGorilla != true )
        {  
            bgWave.GetComponent<Image>().enabled = false;
            cdWave.GetComponent<Image>().enabled = false;
        }
    }
}
