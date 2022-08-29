using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnClickScene : MonoBehaviour
{

    public void OnClickPlayBtn()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnClickStartBtn()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void OnClickBackBtn()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickSettingBtn()
    {
        SceneManager.LoadScene("Setting");
    }

    public void OnClickGalleryBtn()
    {
        SceneManager.LoadScene("Gallery");
    }

    public void OnClickCameraBtn()
    {
        SceneManager.LoadScene("Camera");
    }

    public void OnClickSpaceAddBtn()
    {
        SceneManager.LoadScene("SpaceScene");
    }

    public void OnClickTutoBtn()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OnClickTutoExitBtn()
    {
        SceneManager.LoadScene("PlayScene");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
