using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
// filename 이 에셋을 생성하게 되면 기본적으로 지어질 이름
// menuName 유니티 에셋-우클-Create 하게 되면 메뉴에 보이게 될 이름
// order 메뉴에 보일 순서

public class Item : ScriptableObject // 게임오브젝트에 붙일 필요없음.
{
    public enum ItemType //아이템 유형
    {
        Equipment,
        Used,
        Letter,
        ETC,
    }
    public string itemName;
    public ItemType itemType;
    public Sprite itemImage;    //아이템의 이미지
    public GameObject itemPrefab;   //아이템의 프리팹 (생성시, 프리팹으로 생성)

    public string weaponType;

    public bool Use()
    {
        return false;
    }

}
