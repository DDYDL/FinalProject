using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ActionController : MonoBehaviour
{

    public bool IsButtonPutted = false;
    public static bool IsItemTrigger = false;
    private bool pickupActivated = false; // 아이템 습득 가능할시 True

    private static GameObject _item; //충돌체 정보를 저장.

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Text actionText;

    [SerializeField]
    private Inventory theInventory;     // Inventory.cs

    public static void GetCollider(GameObject other)
    {
        _item = other;
        IsItemTrigger = true;
    }

    public void TryAction() // GrabButton을 누르면 실행이 가능하도록 한다.
    {
        CanPickUp();
    }

    private void CheckItem()
    {
        if (IsItemTrigger)
        {
            if (_item.transform.tag == "Item")
            {
                ItemInfoAppear();
                //Debug.Log("메세지 출력 성공");
            }
        }
        else
            ItemInfoDisappear();
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;     // 아이템을 획득할 수 있는 상태가 된다.
        actionText.gameObject.SetActive(true);
        actionText.text = _item.transform.GetComponent<ItemPickUp>().item.itemName + "  " + "<color=blue>" + "Grab 버튼 클릭" + "</color>";
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

    private void CanPickUp()    // 아이템을 찾고, Grab버튼을 누르면 실행되는 코드 , 
    {
        if (pickupActivated)
        {
            if (_item.transform != null)
            {
                theInventory.AcquireItem(_item.transform.GetComponent<ItemPickUp>().item);
                Destroy(_item.transform.gameObject);    //아이템(3D오브젝트)가 사라지고
                ItemInfoDisappear();    //아이템에 관한 설명이 사라진다.
                IsItemTrigger = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckItem();
    }
}
