using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void PlayGame()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            //load scene 1, samplelevel 
            SceneManager.LoadScene(1);
        }
        
    }
}
