using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class Abilities : MonoBehaviour
{
    [SerializeField] GameObject bonobo;
    [SerializeField] GameObject duoAbilities;

    private List<Image> images = new List<Image>();

    bool changed = false;
    private void Start() {
        changed = !bonobo.GetComponent<PlayerScript>().isOnGorilla;
    }
    //Hide the duo ability icons when theyre not available
    void Update()
    {
        if (bonobo.GetComponent<PlayerScript>().isOnGorilla != changed)
        {
            if (bonobo.GetComponent<PlayerScript>().isOnGorilla != true)
            {
                foreach (Image item in duoAbilities.transform.GetComponentsInChildren<Image>())
                {
                    item.enabled = false;
                }
            } 
            else 
            {
                foreach (Image item in duoAbilities.transform.GetComponentsInChildren<Image>())
                {
                    if (item.name == "ApeThrow" || item.name == "GorillaCharge" || item.name == "Shockwave" || item.name == "SpinToWin")
                    {
                        item.enabled = true;
                    }
                }
            }
            changed = bonobo.GetComponent<PlayerScript>().isOnGorilla;
        }
    }
}
