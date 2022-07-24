using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ghost : MonoBehaviour
{
    public Animator ghostAnimator;
    private Vector3 target;
    public  Animator npcAni;
    public Text noticeText;
    public  AudioSource audio;
    public  AudioClip sound; //ȿ����

    public  bool isAttack = false;

    public static GameObject npc;
    public static ghost ghostNpc;

    // Start is called before the first frame update
    void Start()
    {
        npcAni = GetComponent<Animator>();
        npc = gameObject;
        ghostNpc = this.GetComponent<ghost>();
        Debug.Log("start ����");
    }

    public void OnTriggerEnter(Collider other)
    {
        target = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
        transform.LookAt(target);   //�÷��̾ �ٶ󺸵��� ����.
        Debug.Log("Ʈ���� ����");
        //noticeText.text = "�Ƿɿ��� �������� ����ϼ���";
        Debug.Log("Npc�� ����"); //�Ƿɿ��� �������� ����϶�� text�� �������� �Ѵ�.
        isAttack = true;
    }

    public  void ghostDead()
    {
        Debug.Log("Dead ����");
        if (isAttack) 
        { 
            npcAni.SetBool("isAttacked", true);   //Npc�� �״� �ִϸ��̼� ����.
            audio.clip = sound;
        }
    }

}
