using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class BulletCount : MonoBehaviour
{
    List<Image> bullets = new List<Image>();
    // Start is called before the first frame update
    void Start()
    {
        //save all of the bullets
        foreach (Image bullet in gameObject.transform.GetComponentsInChildren<Image>())
        {
            bullets.Add(bullet);
        }
    }

    //when current count changes update UI
    public void UIUpdateBullets(int count)
    {
        if (count < 6)
        {
            for (int i = 0; i < 6 - count; i++)
            {
                bullets[i].GetComponent<Image>().enabled = false;
            }
        }
    }
    
    // show all the bullets again
    public void UIReloadBullets()
    {
        foreach (Image sprite in bullets)
        {
           sprite.GetComponent<Image>().enabled = true;
        }
    }
}
