using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Volume : MonoBehaviour
{
    // public float volume = 0.4f;
    AudioSource BGM;
    [SerializeField] Slider volume;

    // Start is called before the first frame update
    void Start()
    {
        GameObject source = GameObject.Find("BGM");
        BGM = source.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        BGM.volume = volume.value;
    }

    // void ChangeVolume(float value)
    // {
    //     volume = value;
    // }
}
