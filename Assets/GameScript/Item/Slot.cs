using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private ItemDataBase theItemDatabase;
    public Item item;
    public int itemCount;
    public Image itemImage;
    public GameObject click2;
    //public GameObject ghost;

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;

    }



    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if (item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            Debug.Log("인벤토리 입력 성공");
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }

        SetColor(1);
    }


    public void UsedItem()
    {
        if(item.itemName == "HealthPotion")
            click2.gameObject.SetActive(false);

        if (item != null)
        {

            if (item.itemName == "Bomb") 
            {
                ghost.ghostNpc.ghostDead();
                Destroy(ghost.npc);
                Debug.Log("악령 퇴치 성공");

                if (item.itemType == Item.ItemType.Used)
                    SetSlotCount(-1);
                return;
            }

            theItemDatabase.UseItem(item);
            

            if (item.itemType == Item.ItemType.Used)
                SetSlotCount(-1);
        }
    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);    // 이미지의 투명도를 조절한다.

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }



    // Start is called before the first frame update
    void Start()
    {
        theItemDatabase = FindObjectOfType<ItemDataBase>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
