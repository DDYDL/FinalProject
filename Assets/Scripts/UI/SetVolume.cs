using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    float sound;

    void Awake()
    {
 
    }

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Music", sound);
    }

    public void SoundSlider()
    {
        float sound = slider.value;

        if (sound == -40f) mixer.SetFloat("Music", -80);
        else mixer.SetFloat("Music", sound);

        PlayerPrefs.SetFloat("Music", sound);
    }

    // Update is called once per frame
    void Update()
    {
        SoundSlider();
    }
}
