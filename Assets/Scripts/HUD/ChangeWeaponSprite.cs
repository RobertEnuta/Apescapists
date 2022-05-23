using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class ChangeWeaponSprite : MonoBehaviour
{
    private string weaponLocation = "Weapons/";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //change when a new gun is picked up
        if(Input.GetKeyDown("k"))
        {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Weapons/Monk");
        } 
        if(Input.GetKeyDown("l"))
        {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Weapons/Magnum");
        }
        
    }
}
