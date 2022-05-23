using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject bonobo;
    [SerializeField] GameObject gorilla;
    [SerializeField] Slider sliderB;
    [SerializeField] Slider sliderG;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sliderB.value = bonobo.GetComponent<PlayerScript>().bonoboHealth;
        sliderG.value = gorilla.GetComponent<GorillaScript>().gorillaHealth;
    }
}
