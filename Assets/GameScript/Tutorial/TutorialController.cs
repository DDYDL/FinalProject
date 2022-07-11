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

    public bool isAttaking = false;

    // Start is called before the first frame update
    void Start()
    {
       
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



    // Update is called once per frame
    void Update()
    {
        
    }
}
