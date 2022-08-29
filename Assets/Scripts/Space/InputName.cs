using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputName : MonoBehaviour
{
    public InputField spaceNameInput;
    public static string spaceName = null;

    private void Awake()
    {
        spaceName = spaceNameInput.GetComponent<InputField>().text;
    }

    void Start()
    {

    }

    void Update()
    {
        //키보드
        if (spaceName.Length > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            InputSpaceName();
        }
    }

    public void InputSpaceName()
    {
        spaceName = spaceNameInput.text;
        PlayerPrefs.SetString("CurrentSpaceName", spaceName);
        AccountManager.SetCoordi();
    }
}
