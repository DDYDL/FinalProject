using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideClickEvent : MonoBehaviour
{
    public GameObject InvenScrollView;

    public void ButtonClick()
    {
        InvenScrollView.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
