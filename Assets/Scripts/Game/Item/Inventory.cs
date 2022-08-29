using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;
    [SerializeField]
    private GameObject go_mSlotsParent;

    public TutorialController tuto;
    private Slot[] slots;
    private MissionSlot[] missionSlot;
    public MissionPage m_Page;

    public void AcquireItem(Item _item, int _count = 1) //Grab버튼을 눌렀을 때, 실제로 실행되는 함수
    {
        if (Item.ItemType.Letter != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)      //아이템칸에 아무것도 없을 경우
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }


            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)
                {
                    slots[i].AddItem(_item);

                    //slot(1)에 아이템이 들어가게 되면 회복 아이템을 사용하라는 메세지가 나오도록 지정.
                    if (i == 1 && slots[i].IsTutorial)
                    {
                        tuto.Step2();
                    }
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < missionSlot.Length; i++)
            {
                if (missionSlot[i].item == null) //
                {
                    missionSlot[i].item = _item;
                    missionSlot[i].ActiveMissionpage(i);
                    return;
                }
            }
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        missionSlot = go_mSlotsParent.GetComponentsInChildren<MissionSlot>();
    }

}
