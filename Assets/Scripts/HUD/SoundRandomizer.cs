using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRandomizer : MonoBehaviour
{    
    [SerializeField] AudioClip[] sounds;
    [Range(0.1f,0.5f)]
    [SerializeField] float volChange = 0.15f;
    [Range(0.1f,0.5f)]
    [SerializeField] float pitch = 0.2f;
    private AudioSource source;
    private float vol;
    private float pit;
    private void Start() {
        source = gameObject.GetComponent<AudioSource>();
        vol = source.volume;
        pit = source.pitch;
    }
    public void PlaySound()
    {
        source.clip = sounds[Random.Range(0, sounds.Length)];
        source.volume = Random.Range(vol - volChange, vol);
        source.pitch = Random.Range(pit - pitch, pit + pitch);
        source.Play();
    }    

    // public void PlayOneSound()
    // {
    //     source.clip = sounds[Random.Range(0, sounds.Length)];
    //     source.volume = Random.Range(1 - volChange, 1);
    //     source.pitch = Random.Range(1 - pitch, 1 + pitch);
    //     source.PlayOneShot(source.clip,source.volume);
    // }
}
