using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // List<bool> enemyInSquare = new List<bool>();
    List<RoomSquares> squares = new List<RoomSquares>();
    // GameObject square;
    bool clear; 
    bool started;
    private void Start() {
        //put all child squares in the list
        foreach (RoomSquares square in gameObject.transform.GetComponentsInChildren<RoomSquares>())
        {
            squares.Add(square); 
        }
    }

    // if there are enemies => room is not empty
    public bool IsRoomEmpty()
    {
        //suppose the squares are clear
        clear = true;
        //verify all of the child squares are empty
        foreach (RoomSquares square in squares)
        {
            //if one square if not clear means room is not empty
            if (square.IsClear() == false)
            {
                clear = false;
                break;
            }
        }

        //get count of all children objects that form the room
        int nrChildren = gameObject.transform.childCount;
        //if the room has no squares the door should open
        if (nrChildren == 0)
        {
            Debug.Log(transform.parent.name + " has no squares! \n The door is removed during gameplay." );
            return true;
        }
        return clear;
    }

}
