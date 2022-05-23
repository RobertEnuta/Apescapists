using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject room;
    [SerializeField] GameObject starterDoor;
    private void FixedUpdate()
    {
        if (starterDoor.GetComponent<ClosingDoor>().GetStarter() == true && starterDoor != null)
        {
            if (room.GetComponent<Room>().IsRoomEmpty() == true)
            { 
                Destroy(gameObject);
            }
        } else if(starterDoor == null)
        {
            Destroy(gameObject);
        }

    }

    public IEnumerator DestroyRoom()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
