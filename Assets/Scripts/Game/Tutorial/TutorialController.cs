using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Text noticeText;
    public Animator npcAni;
    public GameObject particle;
    public GameObject click2;
    public Slot slot;

    public bool isAttaking = false;

    public void Start()
    {
        slot.IsTutorial = true;
    }

    public void Step2() // Inventory ��ũ��Ʈ���� slot(1)�� �������� ������ �����.
    {
        noticeText.text = "ȸ�� �������� ����ϼ���";  // slot(1)�� ����Ű�� �ִϸ��̼� ����.
        click2.gameObject.SetActive(true);
    }

    /*public void Step3() //IsNpc ��ũ��Ʈ���� ��� �Ƿɰ� �������� ��,
    {
        noticeText.text = "�Ƿɿ��� �������� ����ϼ���";
        Debug.Log("Npc�� ����");
        isAttaking = true;
    }*/

    public void attackNpc() //ItemDataBase���� Bomb �������� ���Ǹ� ����� �Լ�
    {
        npcAni.SetBool("isAttacked", true);   //Npc�� �״� �ִϸ��̼� ����.
        Destroy(GetComponent<ghost>());
        Debug.Log("�Ƿ� ��ġ ����");
    }

    public void ActiveClick2(bool tf)
    {
        click2.gameObject.SetActive(tf);
    }


}
