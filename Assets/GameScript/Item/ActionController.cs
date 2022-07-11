using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ActionController : MonoBehaviour
{

    public bool IsButtonPutted = false;
    public static bool IsItemTrigger = false;
    private bool pickupActivated = false; // ������ ���� �����ҽ� True

    private static GameObject _item; //�浹ü ������ ����.

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Text actionText;

    public Text noticeText;

    [SerializeField]
    private Inventory theInventory;     // Inventory.cs

    public static void GetCollider(GameObject other)
    {
        _item = other;
        IsItemTrigger = true;
    }

    public void TryAction() // Button�� ������ ������ �����ϵ��� �Ѵ�.
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
                //Debug.Log("�޼��� ��� ����");
            }
        }
        else
            ItemInfoDisappear();
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;     // �������� ȹ���� �� �ִ� ���°� �ȴ�.
        actionText.gameObject.SetActive(true);
        actionText.text = _item.transform.GetComponent<ItemPickUp>().item.itemName + "  " + "<color=blue>" + "Grab ��ư Ŭ��" + "</color>";
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (_item.transform != null)
            {
                theInventory.AcquireItem(_item.transform.GetComponent<ItemPickUp>().item);
                Destroy(_item.transform.gameObject);
                ItemInfoDisappear();
                IsItemTrigger = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckItem();
    }
}
