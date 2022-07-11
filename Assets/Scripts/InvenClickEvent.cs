using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenClickEvent : MonoBehaviour
{
    public GameObject InvenScrollView;

    public void ButtonClick()
    {
        InvenScrollView.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
