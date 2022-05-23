using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingDoor : MonoBehaviour
{
    List<ClosingSquare> squares = new List<ClosingSquare>();
    bool close = false;
    bool started = false;
    private void Start() 
    {

        foreach (ClosingSquare cs in gameObject.transform.GetComponentsInChildren<ClosingSquare>())
        {
            squares.Add(cs);
        }    
    }
    private void FixedUpdate() 
    {
        foreach (ClosingSquare cs in squares)
        {
            if (cs.PlayerInRoom() == true)
            {
                close = true;
                break;
            }
        }
        if (close == true)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponent<Collider2D>().enabled = true;
            started = true;
        }    
    }
    public bool GetStarter()
    {
        return started;
    }
    Object[] enemyPrefabs;
    int enemyDif = 0;
    private void SpawnEnemies()
    {
        //get room difficulty
        int dif = transform.parent.GetComponent<DifficultyRating>().difficulty;
        //load al lof the enemies in an array
        enemyPrefabs = Resources.LoadAll("Prefabs/Enemies");
        enemyDif = 0;
        int newEnemyDif = 0;
        //while new enemies dont go over difficulty limit, spawn new enemy
        while (enemyDif < dif)
        {
            //select an enemy from array
            GameObject randEnemy = (GameObject)enemyPrefabs[Random.Range(0,enemyPrefabs.Length)];
            newEnemyDif = randEnemy.GetComponent<DifficultyRating>().difficulty;
            //check the max difficulty and select new enemy
            while(enemyDif + newEnemyDif > dif)
            {
                //random a new enemy
                randEnemy = (GameObject)enemyPrefabs[Random.Range(0,enemyPrefabs.Length)];
                newEnemyDif = randEnemy.GetComponent<DifficultyRating>().difficulty;   
                //stop at first enemy found
                if (enemyDif + newEnemyDif <= dif)
                {
                    break;
                }                
            }
            
        }
    }
}
