using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public string itemName;
    [Tooltip("Hp, AT �� �����մϴ�.")]
    public string[] part;   //ȿ�� ���� �ϳ��� ��ġ�� ȿ���� �������� �� �־� �迭.
    //public int[] num;
}


public class ItemDataBase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;

    //public TutorialController tuto;
    //public ghost npc;
    public AudioSource audio;
    public AudioClip sound; //ȿ����
    //public ghost ghostNpc;

    private const string HP = "HP", AT = "ATTACK";

    /*[SerializeField]
    private StatusController thePlayerStatus;
    [SerializeField]
    private WeaponManager theWeaponManager;*/

    public void UseItem(Item _item)
    {
        if (_item.itemType == Item.ItemType.Used)   //�Һ� �������� ��,
        {
            for (int i = 0; i < itemEffects.Length; i++)
            {
                if (itemEffects[i].itemName == _item.itemName)
                {
                    for (int j = 0; j < itemEffects[i].part.Length; j++)
                    {
                        switch (itemEffects[i].part[j])
                        {
                            case HP:
                                HealthBarHandler.SetHealthBarValue(HealthBarHandler.GetHealthBarValue() + 0.2f);
                                audio.clip = sound;
                                break;
                            case AT:
                                /*ghost.ghostNpc.ghostDead();
                                Destroy(ghost.npc);
                                Debug.Log("�Ƿ� ��ġ ����");*/
                                break;
                            default:
                                Debug.Log("Status ���� �߻�");
                                break;
                        }

                        Debug.Log(_item.itemName + "�� ����߽��ϴ�.");
                    }
                    return;
                }
            }

            Debug.Log("itemEffectDatabase�� ��ġ�ϴ� itemName�� �����ϴ�.");

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //tuto = GetComponent<TutorialController>();
        //ghostNpc = GenerateNpc.npc.GetComponent<ghost>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
