using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public GameObject BgSound;
    AudioSource backmusic;

    void Awake() 
    {
        BgSound = GameObject.Find("BgSound");
        backmusic = BgSound.GetComponent<AudioSource>(); //배경음악 저장해둠

        backmusic.Play();
        DontDestroyOnLoad(BgSound); //배경음악 계속 재생하게
    }

    public void PlayMusic()
    {
        if(backmusic.isPlaying) return;
        backmusic.Play();
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
