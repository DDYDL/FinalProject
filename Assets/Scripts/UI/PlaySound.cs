using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public GameObject BgSound;

    void Awake()
    {
        BgSound = GameObject.Find("BgSound");

        Destroy(BgSound); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
