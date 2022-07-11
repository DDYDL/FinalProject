using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public string itemName;
    [Tooltip("Hp, AT 만 가능합니다.")]
    public string[] part;   //효과 포션 하나당 미치는 효과가 여러개일 수 있어 배열.
    //public int[] num;
}


public class ItemDataBase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;

    //public TutorialController tuto;
    //public ghost npc;
    public AudioSource audio;
    public AudioClip sound; //효과음
    //public ghost ghostNpc;

    private const string HP = "HP", AT = "ATTACK";

    /*[SerializeField]
    private StatusController thePlayerStatus;
    [SerializeField]
    private WeaponManager theWeaponManager;*/

    public void UseItem(Item _item)
    {
        if (_item.itemType == Item.ItemType.Used)   //소비 아이템일 때,
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
                                Debug.Log("악령 퇴치 성공");*/
                                break;
                            default:
                                Debug.Log("Status 오류 발생");
                                break;
                        }

                        Debug.Log(_item.itemName + "을 사용했습니다.");
                    }
                    return;
                }
            }

            Debug.Log("itemEffectDatabase에 일치하는 itemName이 없습니다.");

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
