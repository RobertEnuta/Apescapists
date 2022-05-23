using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRandomizer : MonoBehaviour
{ 
    //GUN OBJECT
    [SerializeField] GameObject gun;
    Weapon gunScript;
    int currentBullets;
    //Sound values
    [Range(0.1f,0.5f)]
    [SerializeField] float volChange = 0.15f;
    [Range(0.1f,0.5f)]
    [SerializeField] float pitch = 0.2f;
    private AudioSource source;
    //sound clips array
    [SerializeField] AudioClip[] sounds;
    [SerializeField] AudioClip reloadSound;
    // Start is called before the first frame update
    void Start()
    {
        gunScript = gun.GetComponent<Weapon>();
        source = GetComponent<AudioSource>();
        currentBullets = gunScript.CurrentBullets();
    }
    private bool reloaded = false;
    // Update is called once per frame
    void Update()
    {
        if(gunScript.CurrentBullets() < currentBullets)
        {   
            currentBullets = gunScript.CurrentBullets();
            source.clip = sounds[Random.Range(0, sounds.Length)];
            source.volume = Random.Range(1 - volChange, 1);
            source.pitch = Random.Range(1 - pitch, 1 + pitch);
            source.Play();
        }

        if (Input.GetKey("r") ||  gunScript.CurrentBullets() == 0)
        {
            currentBullets = gunScript.MaxBullets();
            source.clip = reloadSound;
            source.volume = Random.Range(1 - volChange, 1);
            source.pitch = Random.Range(1 - pitch, 1 + pitch);
            source.Play();
            // reloaded = true;
        }
    }
}
