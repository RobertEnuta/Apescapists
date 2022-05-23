using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Victory : MonoBehaviour
{
    [SerializeField] GameObject win;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    private void Update() {
        if(win.GetComponent<WinScript>().WinR())
        {
            Win();
        }
    }

    private void Win() 
    {
        gameObject.GetComponent<Canvas>().enabled = true;    
    }
}
