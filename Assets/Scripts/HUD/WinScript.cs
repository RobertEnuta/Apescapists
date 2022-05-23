using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    bool state = false;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")    
        {
            state = true;
        }
    }

    public bool WinR()
    {
        return state;
    }
}
