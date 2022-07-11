using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpaceSelete : MonoBehaviour
{
    string buttonName;
    public void InputClick() => SeleteSpace(buttonName);
    public void InputClick1() => SeleteSpace(buttonName);
    public void InputClick2() => SeleteSpace(buttonName);

    void Start()
    {
        
    }

    
    void Update()
    {
        buttonName = EventSystem.current.currentSelectedGameObject.name;
    }

    void SeleteSpace(string buttonName)
    {
        Debug.Log("buttonName = " + buttonName); //SpaceSlot
        string index = buttonName.Substring(buttonName.Length - 2, 1);
        int x = int.Parse(index);
        Debug.Log("placeNumber = " + x);

        switch(x)
        {
            case 1:
                Initialization.placeCode = 1;
                Debug.Log("placeCode = " + Initialization.placeCode);
                break;
            case 2:
                Initialization.placeCode = 2;
                Debug.Log("placeCode = " + Initialization.placeCode);
                break;
            case 3:
                Initialization.placeCode = 3;
                Debug.Log("placeCode = " + Initialization.placeCode);
                break;
            default:
                break;
        }
    }
}
