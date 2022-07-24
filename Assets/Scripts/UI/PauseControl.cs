using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class PauseControl : MonoBehaviour
{
    bool pauseActive = false;
    public GameObject PopupCanvas;
    AROcclusionManager occlusionManager;

    public void pauseBtn()
    {
        //occlusionManager = GetComponent<AROcclusionManager>();
        
        Time.timeScale = 0;
        pauseActive = true;
        //occlusionManager.enabled = false;

        
    }

    public void GoButtonClick()
    {
        Time.timeScale = 1;
        pauseActive = false;

        //occlusionManager.enabled = true;
    }

    public void ExitButtonClick()
    {
        SceneManager.LoadScene("Main");
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
