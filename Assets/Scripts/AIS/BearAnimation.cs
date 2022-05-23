using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAnimation : MonoBehaviour
{
    public void ShowAnimation()
    {
        gameObject.GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<SoundRandomizer>().PlaySound();
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Animator>().SetTrigger("Attack");
    }

}
