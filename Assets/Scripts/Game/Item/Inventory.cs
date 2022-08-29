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

    public void AcquireItem(Item _item, int _count = 1) //Grab��ư�� ������ ��, ������ ����Ǵ� �Լ�
    {
        if (Item.ItemType.Letter != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)      //������ĭ�� �ƹ��͵� ���� ���
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

                    //slot(1)�� �������� ���� �Ǹ� ȸ�� �������� ����϶�� �޼����� �������� ����.
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
