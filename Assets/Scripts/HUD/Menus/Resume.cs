using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Resume : MonoBehaviour
{
    [SerializeField] Image bg;
    public bool paused = false;
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex > 0)
        {
            ResumeGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex > 0)
        {
            if(Input.GetKeyDown("p"))
            {
                paused = !paused;
            }
            if(!paused)
            {
                ResumeGame();
            } 
            else
            {            
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void PauseGame()
    {
        bg.GetComponent<Image>().color = new Color(1f,1f,1f,0.1f);
        gameObject.GetComponent<Canvas>().enabled = true;
    }
}
