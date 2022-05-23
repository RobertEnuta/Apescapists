using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingSquare : MonoBehaviour
{
    bool closeDoor = false;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")    
        {
            closeDoor = true;
        }
    }

    public bool PlayerInRoom()
    {
        return closeDoor;
    }
}
