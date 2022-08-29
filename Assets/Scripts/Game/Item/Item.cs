using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
// filename �� ������ �����ϰ� �Ǹ� �⺻������ ������ �̸�
// menuName ����Ƽ ����-��Ŭ-Create �ϰ� �Ǹ� �޴��� ���̰� �� �̸�
// order �޴��� ���� ����

public class Item : ScriptableObject // ���ӿ�����Ʈ�� ���� �ʿ����.
{
    public enum ItemType //������ ����
    {
        Equipment,
        Used,
        Letter,
        ETC,
    }
    public string itemName;
    public ItemType itemType;
    public Sprite itemImage;    //�������� �̹���
    public GameObject itemPrefab;   //�������� ������ (������, ���������� ����)

    public string weaponType;

    public bool Use()
    {
        return false;
    }

}
