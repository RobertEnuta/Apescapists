using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSquares : MonoBehaviour
{
    bool noEnemy = false;
    bool lastEnemy = false;
    List<Collider2D> enemies;
    private void Start()
    {
        enemies = new List<Collider2D>();
    }
    private void Update()
    {
        if(enemies.Count == 0)
        {
            noEnemy = true;
        }
        else 
        {
            noEnemy = false;
        }
    }

    public bool IsClear()
    {
        return noEnemy;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Add(other);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Remove(other);
        }
    }
}
