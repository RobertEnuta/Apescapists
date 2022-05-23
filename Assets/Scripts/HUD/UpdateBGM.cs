using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateBGM : MonoBehaviour
{
    [SerializeField] AudioClip main;
    [SerializeField] AudioClip loop1;
    [SerializeField] AudioClip loop2;
    [SerializeField] AudioClip loop3;
    [SerializeField] AudioClip loop4;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip win;
    [SerializeField] GameObject bonobo;
    [SerializeField] GameObject gorilla;
    private AudioSource source;

    private int changed = 0;
    private int lastChanged = -1;
    private float hp = 100;
    // Start is called before the first frame update
    void Start()
    {
        source =  gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {       
        ChangeSong(SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            bonobo = GameObject.Find("Bonobo");
            gorilla = GameObject.Find("Gorilla");
            hp = bonobo.GetComponent<PlayerScript>().bonoboHealth + gorilla.GetComponent<GorillaScript>().gorillaHealth;
        }

    }
    public float time, timer;
    public void ChangeSong(int scene)
    {
        if(scene == 0)
        {
            source.clip = main;
        }
        else
        {

            if(hp >= 75)
            {
                // source.clip = loop1;
                // StartCoroutine(WaitToChange(1));
                // changed = 1;
                WaitToChange(1);
            }
            else if (hp <= 75 && hp > 50)
            {
                WaitToChange(2);
                // source.clip = loop2;
                // StartCoroutine(WaitToChange(2));
                // changed = 2;
            } 
            else if (hp <= 50 && hp > 25)
            {
                WaitToChange(3);
                // source.clip = loop3;
                // StartCoroutine(WaitToChange(3));
                // changed = 3;
            }
            else if ( hp <= 25 && hp > 0)
            {
                WaitToChange(4);
                // source.clip = loop4;
                // StartCoroutine(WaitToChange(4));
                // changed = 4;
            }
            else if (hp <= 0 || bonobo.GetComponent<PlayerScript>().bonoboHealth <= 0)
            {
                source.clip = death;
                changed=5;
            }           
        }

        if(changed != lastChanged)
        {
            PlayMusic();
            lastChanged = changed;
        }
    }

    public void PlayMusic()
    {
        source.Play();
    }

    public void WaitToChange(int change)
    {
        // yield return new WaitForSeconds(source.clip.length + 0.01f - source.time);
        time += Time.deltaTime;
        timer = source.clip.length + 0.01f - source.time;
        if(time >= timer )
        {
            time = 0;
            changed = change;   
            switch (change)
            {
                case 1:
                    source.clip = loop1;
                    break;
                case 2:
                    source.clip = loop2;
                    break;
                case 3:
                    source.clip = loop3;
                    break;
                case 4:
                    source.clip = loop4;
                    break;
            }   
        } 
    }
}
