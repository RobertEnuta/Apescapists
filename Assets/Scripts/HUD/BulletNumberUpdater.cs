using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.


public class BulletNumberUpdater : MonoBehaviour
{
    [SerializeField] GameObject gun;
    Weapon gunScript;
    [SerializeField] Image currentTens;
    [SerializeField] Image currentOnes;
    [SerializeField] Image maxTens;
    [SerializeField] Image maxOnes;

    int currentBullets;
    int maxBullets;
    string numberName;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentBullets = gun.GetComponent<Weapon>().CurrentBullets();
        maxBullets = gun.GetComponent<Weapon>().MaxBullets();
        if (currentBullets < 10)
        {
            currentTens.enabled = false;
        }
        else 
        {
            currentTens.enabled = true;
        }
        if (maxBullets < 10)
        {
            maxTens.enabled = false;
        }
        else 
        {
            maxTens.enabled = true;
        }
        numberName = (currentBullets / 10).ToString();
        currentTens.sprite = Resources.Load<Sprite>("Numbers/" + numberName); 
        numberName = (currentBullets % 10).ToString();
        currentOnes.sprite = Resources.Load<Sprite>("Numbers/" + numberName); 
        numberName = (maxBullets / 10).ToString();
        maxTens.sprite = Resources.Load<Sprite>("Numbers/" + numberName); 
        numberName = (maxBullets % 10).ToString();
        maxOnes.sprite = Resources.Load<Sprite>("Numbers/" + numberName); 
    }
}
