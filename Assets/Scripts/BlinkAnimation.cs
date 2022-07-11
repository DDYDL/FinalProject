using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkAnimation : MonoBehaviour
{
    float time;

    // Update is called once per frame
    void Update()
    {
        if(time < 1f)
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 1 - time);
        }
        else
        {
            GetComponent<Image>().color = new Color(1, 1, 1, time);
           

            
        }

        time += Time.deltaTime;
        time = 0;
    }
}
